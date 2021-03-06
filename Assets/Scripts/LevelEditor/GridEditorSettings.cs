﻿using System.Linq;
using Assets.Scripts.Extensions;
using Assets.Scripts.Models;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.LevelEditor
{
    [ExecuteInEditMode]
    public class GridEditorSettings : UnityBehavior, IGridEditorSettings
    {
        public string LevelName;
        public int LevelNumber;

        public LevelSummary GetExportData()
        {
            var ballData = gameObject.GetChildren()
                .Where(c => c.GetComponent<EditableBall>() != null)
                .Select(c => c.GetComponent<EditableBall>().Export())
                .ToList();

            var levelSummary = new LevelSummary(LevelNumber, LevelName)
            {
                BallData = ballData
            };
            return levelSummary;
        }

        public void SetLevelData(LevelSummary levelSummary)
        {
            Debug.Log("Clearing children...");
            ClearAllChildren();

            var simpleObjectPool = GetComponent<SimpleObjectPool>();
            LevelNumber = levelSummary.OrdinalNumber;
            LevelName = levelSummary.LevelNumber;
            Debug.Log("Loading level : " + LevelName + " (" + levelSummary.BallData.Count + " balls)");
            foreach (var ballLevelData in levelSummary.BallData)
            {
                var newBall = simpleObjectPool.GetObjectFromPool();
                var editableBall = newBall.GetComponent<EditableBall>();
                editableBall.SetData(ballLevelData);
                newBall.transform.SetParent(transform);
            }
        }

        private void ClearAllChildren()
        {
            var simpleObjectPool = GetComponent<SimpleObjectPool>();
            var children = gameObject.GetChildren();
            foreach (var child in children)
            {
                var pooledObject = child.GetComponent<PooledObject>();
                if (pooledObject != null)
                {
                    simpleObjectPool.ReturnObjectToPool(child);
                }
                else
                {
                    GameObject.DestroyImmediate(child);
                }
            }
        }


        public void AlignAllChildrenToGrid()
        {
            var children = gameObject.GetChildren();
            foreach (var child in children)
            {
                var position = child.transform.position;
                var newPosition = new Vector3(Mathf.Round(position.x), Mathf.Round(position.y));
                child.transform.position = newPosition;
            }
        }
    }
}
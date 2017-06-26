using System;
using System.Collections.Generic;
using Assets.Scripts.Core.Events;
using Assets.Scripts.Models;
using UnityEngine;

namespace Assets.Scripts.Balls
{
    public interface IBallController
    {
        bool Active { get; set; }
        bool IsProjectile { get; set; }
        BallModel Model { get; set; }

        GameObject gameObject { get; }
        void Launch(Vector3 position, Quaternion rotation, Vector3 trajectory, float projectileSpeed);
        bool IsAtGrid(int gridX, int gridY);
        void ResetBall();

        Vector3 Position { get; set; }
        Sprite CurrentBallSprite { get; }
        Quaternion Rotation { get; set; }
        float Hitpoints { get; set; }

        List<IBallController> North { get; }
        List<IBallController> South { get; }
        List<IBallController> East { get; }
        List<IBallController> West { get; }
        List<IBallController> AllNeighbors { get; }
    }
}
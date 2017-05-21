using Assets.Scripts.Models;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(SimpleObjectPool))]
    public class LevelController : MonoBehaviour
    {

        public GameObject Container;

        public int GridSize = 10;
        public Vector2 Offset = new Vector2(0, 0);
        public float Spacing = 1;
        private SimpleObjectPool _simpleObjectPool;


        void Start()
        {
            _simpleObjectPool = GetComponent<SimpleObjectPool>();
            GenerateLevel();
        }

        private void GenerateLevel()
        {
            float x = Offset.x;
            float y = Offset.y;
            for (int gridX = 0; gridX < GridSize; gridX++)
            {
                for (int gridY = 0; gridY < GridSize; gridY++)
                {
                    var newBall = _simpleObjectPool.GetObjectFromPool();
                    newBall.transform.SetParent(Container.transform);
                    newBall.transform.position = new Vector2(x, y);
                    var ballModel = new BallModel { IconName = "PlanetIcons/planet_004" };
                    newBall.GetComponent<BallController>().Model = ballModel;

                    y += Spacing;
                }
                y = Offset.y;
                x += Spacing;
            }
        }

        void Update()
        {

        }
    }
}

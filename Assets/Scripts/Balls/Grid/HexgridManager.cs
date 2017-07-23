using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Balls.Grid
{
    public class HexgridManager : UnityBehavior
    {
        private HexgridController _hexgridController;

        protected override void Start()
        {
            _hexgridController = new HexgridController();
        }
    }

    public class HexgridController
    {
        public HexgridController()
        {

        }
    }
}

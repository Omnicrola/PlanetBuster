using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Balls
{
    public class BallNeighborLocator
    {
        public void SetNeighbors(int gridX, int gridY, IBallController center, List<IBallController> activeBalls)
        {
            var north = activeBalls.FirstOrDefault(b => b.IsAtGrid(gridX, gridY + 1));
            var south = activeBalls.FirstOrDefault(b => b.IsAtGrid(gridX, gridY - 1));
            var east = activeBalls.FirstOrDefault(b => b.IsAtGrid(gridX + 1, gridY));
            var west = activeBalls.FirstOrDefault(b => b.IsAtGrid(gridX - 1, gridY));


            if (east != null)
            {
                east.West.Add(center);
                center.East.Add(east);
            }
            if (west != null)
            {
                west.East.Add(center);
                center.West.Add(west);
            }
            if (north != null)
            {
                north.South.Add(center);
                center.North.Add(north);
            }
            if (south != null)
            {
                south.North.Add(center);
                center.South.Add(south);
            }
        }
    }
}
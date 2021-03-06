﻿using System;
using Assets.Scripts.Balls;
using UnityEngine;

namespace Assets.Scripts.Core.Events
{
    public class BallOutOfBoundsEventArgs : IGameEvent
    {
        public GameObject Ball { get; set; }

        public BallOutOfBoundsEventArgs(GameObject ball)
        {
            Ball = ball;
        }
    }
}
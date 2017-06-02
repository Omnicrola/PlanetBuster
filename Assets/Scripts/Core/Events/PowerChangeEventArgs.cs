using System;

namespace Assets.Scripts.Core.Events
{
    public class PowerChangeEventArgs : EventArgs
    {
        public PowerChangeEventArgs(float newPowerLevel)
        {
            NewPowerLevel = newPowerLevel;
        }

        public float NewPowerLevel { get; private set; }
    }
}
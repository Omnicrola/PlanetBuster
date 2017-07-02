using System;

namespace Assets.Scripts.Models
{
    [Serializable]
    public class BallLevelData
    {
        public int XPos { get; set; }
        public int YPos { get; set; }
        public BallType BallType { get; set; }
        public bool HasPowerGem { get; set; }
        public BallMagnitude Magnitude { get; set; }

        public override string ToString()
        {
            return "BallLevelData[(" + XPos + "," + YPos + ") type:" + BallType + " mag:" + Magnitude + "]";
        }

        protected bool Equals(BallLevelData other)
        {
            return XPos == other.XPos && YPos == other.YPos && BallType == other.BallType && HasPowerGem == other.HasPowerGem && Magnitude == other.Magnitude;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((BallLevelData)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = XPos;
                hashCode = (hashCode * 397) ^ YPos;
                hashCode = (hashCode * 397) ^ (int)BallType;
                hashCode = (hashCode * 397) ^ HasPowerGem.GetHashCode();
                hashCode = (hashCode * 397) ^ (int)Magnitude;
                return hashCode;
            }
        }
    }
}
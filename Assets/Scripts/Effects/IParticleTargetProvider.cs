using UnityEngine;

namespace Assets.Scripts.Effects
{
    public interface IParticleTargetProvider
    {
        Vector3 TargetPosition { get; }
    }
}
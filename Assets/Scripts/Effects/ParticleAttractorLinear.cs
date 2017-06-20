using UnityEngine;

namespace Assets.Scripts.Effects
{
    [RequireComponent(typeof(ParticleSystem))]
    public class ParticleAttractorLinear : MonoBehaviour
    {

        public Vector3 target;
        public float speed = 5f;

        private ParticleSystem ps;
        private ParticleSystem.Particle[] m_Particles;
        private int numParticlesAlive;

        void Start()
        {
            ps = GetComponent<ParticleSystem>();
            if (!GetComponent<Transform>())
            {
                GetComponent<Transform>();
            }
        }

        void Update()
        {
            m_Particles = new ParticleSystem.Particle[ps.main.maxParticles];
            numParticlesAlive = ps.GetParticles(m_Particles);
            float step = speed * Time.deltaTime;
            for (int i = 0; i < numParticlesAlive; i++)
            {
                m_Particles[i].position = Vector3.Lerp(m_Particles[i].position, target, step);
            }
            ps.SetParticles(m_Particles, numParticlesAlive);
        }
    }
}
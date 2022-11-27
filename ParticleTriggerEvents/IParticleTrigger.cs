namespace ParticleTriggerEvents
{
    // Implement this for collision detection.
    public interface IParticleTrigger
    {
        UnityEngine.ParticleSystem.Particle CollidedParticle { get; set; }
        void OnParticleTriggerEnter2D();
    }
}

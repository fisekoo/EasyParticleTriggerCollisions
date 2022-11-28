namespace Fisekoo.ParticleTriggerEvents
{
    // Implement this for collision detection.
    public interface IParticleTrigger
    {
        /// <summary>
        /// Particle collided with object.
        /// </summary>
        UnityEngine.ParticleSystem.Particle CollidedParticle { get; set; }
        void OnParticleTriggerEnter2D();
    }
}

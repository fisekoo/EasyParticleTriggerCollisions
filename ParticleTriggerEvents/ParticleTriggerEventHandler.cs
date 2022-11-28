using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Fisekoo
{
    [RequireComponent(typeof(ParticleSystem))]
    public class ParticleTriggerEventHandler : MonoBehaviour
    {
        #region PROPERTIES
        /// <summary>
        /// Layers which particles will interact.
        /// </summary>
        public LayerMask CollidesWith
        {
            get => _collidesWith;
            set => _collidesWith = value;
        }
        #endregion
        #region EXPOSED_VARIABLES
        /// <summary>
        /// Layers which particles will interact.
        /// </summary>
        [SerializeField] private LayerMask _collidesWith;
        #endregion
        #region PRIVATE_VARIABLES
        /// <summary>
        /// Particle system.
        /// </summary>
        private ParticleSystem _ps;
        /// <summary>
        /// List of every particle that is in bounds of any added colliders.
        /// </summary>
        private List<ParticleSystem.Particle> _enterParticles = new List<ParticleSystem.Particle>();
        #endregion
        #region PRIVATE_STATIC_VARIABLES
        /// <summary>
        /// GameObject array that contains GameObjects with selected layers.
        /// </summary>
        private static GameObject[] _objectsWithLayer;
        #endregion
        #region SETUP
        // Setting up variables...
        private void Awake()
        {
            _ps = GetComponent<ParticleSystem>();
            SetupParticleSystem(_ps);
        }
        private void Start()
        {
            _objectsWithLayer = FindObjectsWithLayer(_collidesWith);
        }
        #endregion

        /// <summary>
        /// Runs every frame while script is active.
        /// Main logic of trigger.
        /// Do not modify unless you understand.
        /// </summary>
        private void OnParticleTrigger()
        {
            int numEnter = _ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, _enterParticles, out var enterColliderData);
            for (int i = 0; i < numEnter; i++)
            {
                // Getting the first collider that particle interacted with.
                var interactedCollider = enterColliderData.GetCollider(i, 0);

                // Getting IPartlicleTriggerInterface
                var tryGetIpt = interactedCollider.TryGetComponent<IParticleTrigger>(out var iPt);

                // Do not do anything if it is not IParticleTrigger object.
                if (tryGetIpt == false) return;

                // Setting collided particle.
                iPt.CollidedParticle = _enterParticles[i];

                // Invoking OnParticleTriggerEnter.
                iPt.OnParticleTriggerEnter2D();

                // Applying particle.
                _enterParticles[i] = iPt.CollidedParticle;
            }

            // Applying particle.
            _ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, _enterParticles);
        }

        /// <summary>
        /// Finds and returns GameObjects with in layerMask.
        /// </summary>
        /// <param name="layerMask">Layers to filter.</param>
        /// <returns>GameObject array with selected layers.</returns>
        private GameObject[] FindObjectsWithLayer(LayerMask layerMask)
        {
            var gameObjsInLayer = MonoBehaviour.FindObjectsOfType<GameObject>().Where(g => ((layerMask.value & (1 << g.layer)) > 0)).ToArray();

            // Adding colliders to Trigger system for interaction.
            AddColliders(gameObjsInLayer);

            return gameObjsInLayer;
        }
        /// <summary>
        /// Adds colliders to Trigger system for interaction.
        /// </summary>
        /// <param name="objects">Objects to add.</param>
        private void AddColliders(GameObject[] objects)
        {
            for (int i = 0; i < objects.Length; i++)
            {
                if (objects[i].TryGetComponent<Collider2D>(out var col)) _ps.trigger.AddCollider(col);
            }
        }
        /// <summary>
        /// Sets up Particle System for trigger collisions.
        /// </summary>
        /// <param name="system">Particle System to setup.</param>
        private void SetupParticleSystem(ParticleSystem system)
        {
            var trigger = system.trigger;
            trigger.enabled = true;
            trigger.enter = ParticleSystemOverlapAction.Callback;
            trigger.exit = ParticleSystemOverlapAction.Callback;
            trigger.inside = ParticleSystemOverlapAction.Ignore;
            trigger.outside = ParticleSystemOverlapAction.Ignore;
            trigger.colliderQueryMode = ParticleSystemColliderQueryMode.One;
        }
    }
}

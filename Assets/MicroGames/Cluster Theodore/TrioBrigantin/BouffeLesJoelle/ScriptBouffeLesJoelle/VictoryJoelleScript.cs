using System.Collections;
using UnityEngine;

namespace Brigantin
{
    namespace BouffeLesJoelle
    {
        /// <summary>
        /// Ludovic Eugenot
        /// </summary>
        public class VictoryJoelleScript : MonoBehaviour
        {
            #region Initiatlization
            [SerializeField] ParticleSystem myParticleSystem;
            #endregion


            void Start()
            {
                myParticleSystem = myParticleSystem ? myParticleSystem : GetComponentInChildren<ParticleSystem>(true);
            }

            public void StartParticleEmitter()
            {
                myParticleSystem.gameObject.SetActive(true);
            }
        }
    }
}

using System.Collections;
using UnityEngine;

namespace Brigantin
{
    namespace BouffeLesJoelle
    {
        public class Music : MonoBehaviour
        {
            #region Initialization
            [SerializeField] AudioSource source;
            [SerializeField] AudioClip musicSlow;
            [SerializeField] AudioClip musicMedium;
            [SerializeField] AudioClip musicFast;
            [SerializeField] AudioClip musicSuperFast;

            bool initDone=false;
            #endregion

            void Start()
            {
                source = source ? source : GetComponent<AudioSource>();
                if (!musicSlow) Debug.LogWarning("Pas de musique Slow chargée");
                if (!musicMedium) Debug.LogWarning("Pas de musique Medium chargée");
                if (!musicFast) Debug.LogWarning("Pas de musique Fast chargée");
                if (!musicSuperFast) Debug.LogWarning("Pas de musique SuperFast chargée");
            }

            private void Update()
            {
                if (!initDone)
                {
                    switch (LUE_Manager.Instance.bpm)
                    {
                        case 60:
                            source.clip = musicSlow;
                            break;
                        case 90:
                            source.clip = musicMedium;
                            break;
                        case 120:
                            source.clip = musicFast;
                            break;
                        case 140:
                            source.clip = musicSuperFast;
                            break;
                    }

                    source.Play();
                    initDone = true;
                }
            }
        }
    }
}

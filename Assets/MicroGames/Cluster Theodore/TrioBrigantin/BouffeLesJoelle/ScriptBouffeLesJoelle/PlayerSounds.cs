using System.Collections;
using UnityEngine;

namespace Brigantin
{
    namespace BouffeLesJoelle
    {
        /// <summary>
        /// Ludovic Eugenot
        /// </summary>
        public class PlayerSounds : MonoBehaviour
        {
            #region Initiatlization
            [Range(0f,50f)] public float startFadeSpeed = 10f;
            [Range(0f,50f)] public float endFadeSpeed = 20f;

            [SerializeField] AudioClip quickSplashClip;
            [SerializeField] AudioClip slowSplashClip;
            [SerializeField] AudioClip dashClip;
            [SerializeField] AudioClip crunchClip;

            [SerializeField] AudioSource quickSplashSource;
            [SerializeField] AudioSource slowSplashSource;
            public AudioSource fxSource;

            [SerializeField] Player playerScript;
            #endregion

            void Start()
            {
                AudioSource[] sources = GetComponents<AudioSource>();
                quickSplashSource = quickSplashSource ? quickSplashSource : sources[0];
                slowSplashSource = slowSplashSource ? slowSplashSource : sources[1];
                fxSource = fxSource ? fxSource : sources[2];
                playerScript = playerScript ? playerScript : transform.parent.GetComponent<Player>();

                quickSplashSource.loop = true;
                slowSplashSource.loop = true;
                fxSource.loop = false;

                quickSplashSource.clip = quickSplashClip;
                slowSplashSource.clip = slowSplashClip;
            }

            public void PlayJoelleCrunch()
            {
                fxSource.PlayOneShot(crunchClip);
            }

            public void PlayJoelleDash()
            {
                fxSource.PlayOneShot(dashClip);
            }

            private void Update()
            {
                FadeSpeedVolumes();
            }
            public void FadeSpeedVolumes()
            {
                quickSplashSource.volume = Mathf.InverseLerp(startFadeSpeed, endFadeSpeed, playerScript.currentSpeed);
                slowSplashSource.volume = playerScript.currentSpeed < startFadeSpeed ?
                    Mathf.InverseLerp(0, startFadeSpeed, playerScript.currentSpeed) :
                    Mathf.InverseLerp(endFadeSpeed, startFadeSpeed, playerScript.currentSpeed);
            }

            public IEnumerator FadeOut(AudioSource audioSource, float duration)
            {
                float currentTime = 0;
                float start = audioSource.volume;

                while (currentTime < duration)
                {
                    currentTime += Time.deltaTime;
                    audioSource.volume = Mathf.Lerp(start, 0f, currentTime / duration);
                    yield return null;
                }
                audioSource.Stop();
                audioSource.volume = start;
                yield break;
            }
            public IEnumerator FadeSources(AudioSource exitingSource, AudioSource enteringSource, float duration) // fade dynamically and not 100% to one or the other
            {
                float currentTime = 0;
                float normalVolume = exitingSource.volume;

                while (currentTime < duration)
                {
                    currentTime += Time.deltaTime;
                    exitingSource.volume = Mathf.Lerp(normalVolume, 0f, currentTime / duration);
                    enteringSource.volume = Mathf.Lerp(0f, normalVolume, currentTime / duration);
                    yield return null;
                }
                exitingSource.Stop();
                exitingSource.volume = normalVolume;
                yield break;
            }
        }
    }
}

using System.Collections;
using Testing;
using UnityEngine;
using UnityEngine.Audio;

namespace Brigantin
{
    namespace FantomeSousLesProjecteurs
    {
        public class SoundManager : MonoBehaviour
        {
            #region Variables

            public AudioClip slowBackgroundMusic;
            public AudioClip mediumBackgroundMusic;
            public AudioClip fastBackgroundMusic;
            public AudioClip veryFastBackgroundMusic;
            public AudioClip ghostSound;
            public AudioClip lightSound;
            public AudioClip victorySound;
            public AudioClip defeatSound;

            private AudioSource source;

            #endregion

            #region Unity methods

            private void Awake()
            {
                
            }

            private void Start()
            {
                source = GetComponent<AudioSource>();

                switch (Manager.Instance.bpm)
                {
                    case BPM.Slow:
                        source.clip = slowBackgroundMusic;
                        break;

                    case BPM.Medium:
                        source.clip = mediumBackgroundMusic;
                        break;

                    case BPM.Fast:
                        source.clip = fastBackgroundMusic;
                        break;

                    case BPM.SuperFast:
                        source.clip = veryFastBackgroundMusic;
                        break;
                }
            }

            private void Update()
            {

            }

            #endregion

            #region Methods

            public void PlayMusic()
            {
                source.Play();
            }

            public void StopMusic()
            {
                if (source.isPlaying)
                {
                    source.Stop();
                }
            }

            public void PlayVictory()
            {
                if (!source.isPlaying)
                {
                    source.clip = victorySound;
                    source.Play();
                }
            }

            public void PlayDefeat()
            {
                if (!source.isPlaying)
                {
                    source.clip = defeatSound;
                    source.Play();
                }
            }

            public void PlayLight()
            {
                if (!source.isPlaying)
                {
                    source.clip = lightSound;
                    source.Play();
                }
            }

            public void PlayGhost()
            {
                if (!source.isPlaying)
                {
                    source.clip = ghostSound;
                    source.Play();
                }
            }

            #endregion
        }
    }
}
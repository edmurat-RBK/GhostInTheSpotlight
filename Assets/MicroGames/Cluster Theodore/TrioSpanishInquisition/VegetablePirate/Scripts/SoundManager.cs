using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpanishInquisition
{
    namespace VegetablePirate
    {
        public class SoundManager : MonoBehaviour
        {

            private static SoundManager _instance;
            public static SoundManager instance
            {
                get
                {
                    if (_instance == null)
                    {
                        _instance = FindObjectOfType<SoundManager>();
                    }
                    return _instance;
                }
            }

            private AudioSource[] gameSounds;

            [SerializeField]
            private AudioSource goodButton;
            [SerializeField]
            private AudioSource wrongButton;
            [SerializeField]
            private AudioSource victorySound;
            [SerializeField]
            private AudioSource defeatSound;
            [SerializeField]
            private AudioSource katanaCut;
            [SerializeField]
            private AudioSource objectThrown;

            [SerializeField]
            private AudioSource musicSlow;
            [SerializeField]
            private AudioSource musicMedium;
            [SerializeField]
            private AudioSource musicFast;
            [SerializeField]
            private AudioSource musicSuperFast;

            [SerializeField]
            private AudioSource bombNoise;

            // Start is called before the first frame update
            void Start()
            {
                gameSounds = GetComponents<AudioSource>();

                goodButton = gameSounds[0];
                wrongButton = gameSounds[1];
                victorySound = gameSounds[2];
                defeatSound = gameSounds[3];
                katanaCut = gameSounds[4];            
                objectThrown = gameSounds[5];
                musicSlow = gameSounds[6];
                musicMedium = gameSounds[7];
                musicFast = gameSounds[8];
                musicSuperFast = gameSounds[9];
                bombNoise = gameSounds[10];
            }

            public void PlayGoodButton()
            {
                goodButton.Play();
                Debug.Log("Goodbutton play.");
            }

            public void PlayWrongButton()
            {
                wrongButton.Play();
                Debug.Log("WrongButton play");
            }

            public void PlayKatana()
            {
                katanaCut.Play();
                Debug.Log("Son Katana");
            }

            public void PlayVictory()
            {
                victorySound.Play();
                Debug.Log("Son Victoire");
            }

            public void PlayDefeat()
            {
                defeatSound.Play();
                Debug.Log("Son défaite");
            }

            public void PlayObjectThrown()
            {
                objectThrown.Play();
                Debug.Log("Son objet lancé");
            }

            public void PlayFlagMusicSlow()
            {
                musicSlow.Play();
            }

            public void PlayFlagMusicMedium()
            {
                musicMedium.Play();
            }

            public void PlayFlagMusicFast()
            {
                musicFast.Play();
            }

            public void PlayFlagMusicSuperFast()
            {
                musicSuperFast.Play();
            }

            public void PlayBombNoise()
            {
                bombNoise.Play();
                Debug.Log("Son bombe");
            }
        }
    }
}

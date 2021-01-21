using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpanishInquisition
{
    namespace HisserLeDrapeau
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
            private AudioSource rFlag1;
            [SerializeField]
            private AudioSource buttonApparition;

            [SerializeField]
            private AudioSource musicSlow;
            [SerializeField]
            private AudioSource musicMedium;
            [SerializeField]
            private AudioSource musicFast;
            [SerializeField]
            private AudioSource musicSuperFast;

            // Start is called before the first frame update
            void Start()
            {
                gameSounds = GetComponents<AudioSource>();

                goodButton = gameSounds[0];
                wrongButton = gameSounds[1];
                rFlag1 = gameSounds[2];
                victorySound = gameSounds[3];
                defeatSound = gameSounds[4];
                buttonApparition = gameSounds[5];
                musicSlow = gameSounds[6];
                musicMedium = gameSounds[7];
                musicFast = gameSounds[8];
                musicSuperFast = gameSounds[9];
            }

            public void PlayGoodButton()
            {
                goodButton.Play();
                Debug.Log("Goodbutton play.");
            }

            public void PlayWrongButton()
            {
                wrongButton.Play();
                Debug.Log("WronButton play");
            }

            public void PlayFlagFirst()
            {
                rFlag1.Play();
                Debug.Log("Drapeau satde 1");
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

            public void PlayButtonApparition()
            {
                buttonApparition.Play();
                Debug.Log("Son Apparition");
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
        }
    }
}

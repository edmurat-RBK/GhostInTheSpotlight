using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Testing;

namespace LeRafiot
{
    namespace UnDeuxTroisRequin
    {
        /// <summary>
        /// Antoine LEROUX
        /// This script is use to tell the end game conditions
        /// </summary>
        
        public class LoseConditions : TimedBehaviour
        {
            #region Variables
            private float spawnCooldown;

            [Header("UI")]
            public GameObject panel;
            public TextMeshProUGUI resultText;
            public TextMeshProUGUI bpmText;
            public Slider timerUI;
            public TextMeshProUGUI tickNumber;
            public Animator buttonAnimator;
            #endregion

            public override void Start()
            {
                base.Start(); 
                bpmText.text = "bpm: " + bpm.ToString();
                spawnCooldown = 60 / bpm;

                buttonAnimator.gameObject.SetActive(true);
            }

            //FixedUpdate is called on a fixed time.
            public override void FixedUpdate()
            {
                base.FixedUpdate(); 
                timerUI.value = (float)timer / spawnCooldown;
            }

            //TimedUpdate is called once every tick.
            public override void TimedUpdate()
            {
                if (Tick == 8 && !Manager.Instance.panel.activeSelf)                //Lose if at the end of the game, the player don't pull the chest to the boat 
                {
                    Manager.Instance.Result(false);
                    SoundManager123Requin.Instance.sfxSound[1].Play();
                    buttonAnimator.gameObject.SetActive(false);
                }

                if (Tick <= 8)
                {
                    tickNumber.text = Tick.ToString();
                }

                if (Tick < 8 && !SharkManager.Instance.sharkIsHere)
                {
                    buttonAnimator.SetTrigger("Press");
                }
            }

            private void Update()
            {              
                if (!Manager.Instance.panel.activeSelf)                             //Lose if the player pulling up the chest when a shark is here
                {
                    if (SharkManager.Instance.sharkIsHere)              
                    {
                        buttonAnimator.SetBool("DontPress", true);

                        if (Input.GetButtonDown("A_Button") || Input.GetKeyDown(KeyCode.Space))
                        {
                            Manager.Instance.Result(false);
                            SoundManager123Requin.Instance.sfxSound[1].Play();
                            buttonAnimator.gameObject.SetActive(false);
                        }
                    }
                    else
                    {
                        buttonAnimator.SetBool("DontPress", false);
                    }
                }
            }
        }
    }
}
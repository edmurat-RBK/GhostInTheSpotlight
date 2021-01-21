using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Testing;

namespace LeRafiot
{
    namespace UnDeuxTroisRequin
    {
        /// <summary>
        /// Antoine LEROUX
        /// This script is use to pull up by x height the rope of y height
        /// </summary>

        public class RopeController : TimedBehaviour
        {
            #region Variables
            private LineRenderer rope;
            private LoseConditions loseScript;
            private bool win;

            [Header("Object attached")]
            public GameObject attachedTo;
            public Animator playerAnimator;
            public Animator pirateAnimator;
            private int spriteNumber;

            [Header("Cam")]
            public GameObject camPlayer;
            public GameObject camBoat;

            //Rope settings
            [HideInInspector] public int ropeSize;
            [HideInInspector] public int pullingUpRopeSize;

            //Level 3 parameters
            [HideInInspector] public bool level3;
            [HideInInspector] public int pullingDownRopeSize = 1;

            private int countAnim;

            #endregion

            public override void Start()
            {
                base.Start(); //Do not erase this line!
                rope = GetComponent<LineRenderer>();
                loseScript = GetComponent<LoseConditions>();
                spriteNumber = 1;
                countAnim = 1;
                
                playerAnimator.gameObject.SetActive(true);
                camPlayer.SetActive(true);
                camBoat.SetActive(false);
            }

            //FixedUpdate is called on a fixed time.
            public override void FixedUpdate()
            {
                base.FixedUpdate(); //Do not erase this line!

            }

            private void Update()
            {
                rope.SetPosition(1, attachedTo.transform.localPosition);                                //The rope is always attach to the chest

                if (rope.GetPosition(1).y > 0.5)
                {
                    if ((Input.GetButtonDown("A_Button") || Input.GetKeyDown(KeyCode.Space)) && !Manager.Instance.panel.activeSelf)
                    {
                        attachedTo.transform.position -= new Vector3(0, -pullingUpRopeSize);            //Pulling up the chest
                        SoundManager123Requin.Instance.sfxSound[2].Play();

                        playerAnimator.SetTrigger("PullUp");
                    }
                }
                else
                {
                    if (!win && !SharkManager.Instance.sharkIsHere && !Manager.Instance.panel.activeSelf)
                    {
                        win = true;
                        rope.SetPosition(1, new Vector3(0, 0));
                        Manager.Instance.Result(true);
                        playerAnimator.gameObject.SetActive(false);
                        camPlayer.SetActive(false);
                        camBoat.SetActive(true);
                        loseScript.buttonAnimator.gameObject.SetActive(false);
                        SoundManager123Requin.Instance.sfxSound[0].Play();
                    }
                }
            }

            //TimedUpdate is called once every tick.
            public override void TimedUpdate()
            {
                if (win)                                                                        //animation update with ticks
                {
                    countAnim++;

                    if (countAnim == 2)
                    {
                        pirateAnimator.SetTrigger("IdleWin");
                    }
                    else if (countAnim == 3)
                    {
                        pirateAnimator.SetTrigger("Win");

                        countAnim = 1;
                    }

                }

                if (!playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("PlayerPullUp") && !Manager.Instance.panel.activeSelf)
                {
                    spriteNumber++;

                    if (spriteNumber == 1)
                    {
                        playerAnimator.SetTrigger("Face");
                    }
                    else if (spriteNumber == 2)
                    {
                        playerAnimator.SetTrigger("Left");
                    }
                    else if (spriteNumber == 3)
                    {
                        playerAnimator.SetTrigger("Right");

                        spriteNumber = 0;
                    }
                }
                
                if (rope.GetPosition(1).y > 0)
                {
                    if (level3 && !Manager.Instance.panel.activeSelf)
                    {
                        attachedTo.transform.position -= new Vector3(0, pullingDownRopeSize);   //Automatically pulling down the chest in rythm with tick
                    }
                }
            }
        }
    }
}
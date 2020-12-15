using System.Collections;
using System.Collections.Generic;
using Testing;
using UnityEditor;
using UnityEngine;

namespace Brigantin
{
    namespace FantomeSousLesProjecteurs
    {
        public class MicroManager : TimedBehaviour
        {
            [Range(1,8)]
            public int _blackScreenDelay;

            public int blackScreenDelay { get; set; }
            public float maxGameTime { get; set; }
            private GameObject ghost { get; set; }
            private GhostMovement ghostMovement { get; set; }
            private GameObject blackScreen { get; set; }

            private void Awake()
            {
                blackScreenDelay = _blackScreenDelay;
            }

            public override void Start()
            {
                // DO NOT REMOVE -----
                base.Start();
                // -------------------

                ghost = GameObject.Find("Ghost");
                ghostMovement = ghost.GetComponent<GhostMovement>();
                blackScreen = GameObject.Find("Black Screen");

                blackScreen.SetActive(false);
            }

            public override void FixedUpdate()
            {
                // DO NOT REMOVE -----
                base.FixedUpdate();
                // -------------------
            }

            private void Update()
            {
                if (Input.GetButtonDown("A_Button"))
                {
                    OnAButton();
                }
            }

            public override void TimedUpdate()
            {
                // DO NOT REMOVE -----
                base.TimedUpdate();
                // -------------------

                if(Tick == 1)
                {
                    ghost.SetActive(true);
                }
                else if(Tick == blackScreenDelay)
                {
                    blackScreen.SetActive(true);
                }
                else if(Tick == 8)
                {
                    Manager.Instance.Result(ghostMovement.inArea);
                }
            }

            private void OnAButton()
            {
                ghost.GetComponent<GhostMovement>().canMove = false;
                blackScreen.SetActive(false);
            }
        }
    }
}

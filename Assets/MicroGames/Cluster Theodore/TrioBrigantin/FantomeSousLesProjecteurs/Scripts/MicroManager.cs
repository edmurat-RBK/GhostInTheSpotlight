﻿using System.Collections;
using System.Collections.Generic;
using Testing;
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
            private GameObject goGhost;
            private GameObject goBlackScreen;

            private void Awake()
            {
                blackScreenDelay = _blackScreenDelay;
            }

            public override void Start()
            {
                // DO NOT REMOVE -----
                base.Start();
                // -------------------

                goGhost = GameObject.Find("Ghost");
                goBlackScreen = GameObject.Find("Black Screen");

                goBlackScreen.SetActive(false);
            }

            public override void FixedUpdate()
            {
                // DO NOT REMOVE -----
                base.FixedUpdate();
                // -------------------
            }

            public override void TimedUpdate()
            {
                // DO NOT REMOVE -----
                base.TimedUpdate();
                // -------------------

                if(Tick == 1)
                {
                    goGhost.SetActive(true);
                }
                else if(Tick == blackScreenDelay)
                {
                    goBlackScreen.SetActive(true);
                }
                else if(Tick == 8)
                {
                    Manager.Instance.Result(true);
                }
            }
        }
    }
}
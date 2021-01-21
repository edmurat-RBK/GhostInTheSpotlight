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
        /// This script is the mini game sound manager
        /// </summary>

        public class SoundManager123Requin : TimedBehaviour
        {
            public static SoundManager123Requin Instance;

            public AudioSource[] globalMusic;

            [Space] public AudioSource[] sfxSound;

            public override void Start()
            {
                base.Start();
                ManagerInit();

                switch (bpm)
                {
                    case (float)BPM.Slow:
                        globalMusic[0].Play();
                        break;
                    case (float)BPM.Medium:
                        //globalMusic[1].Play();
                        break;
                    case (float)BPM.Fast:
                        //globalMusic[2].Play();
                        break;
                    case (float)BPM.SuperFast:
                        globalMusic[3].Play();
                        break;
                    default:
                        break;
                }

            }

            void ManagerInit()
            {
                if (Instance == null)
                {
                    Instance = this;
                }
                else
                {
                    Destroy(gameObject);
                }
            }

            //Update call on a fixed time
            public override void FixedUpdate()
            {
                base.FixedUpdate();
            }

            //Update call every tics
            public override void TimedUpdate()
            {
                base.TimedUpdate();
            }
        }
    }
}
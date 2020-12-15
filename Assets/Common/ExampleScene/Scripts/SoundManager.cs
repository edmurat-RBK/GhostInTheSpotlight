using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Testing;

namespace ExampleScene
{
    public class SoundManager : TimedBehaviour
    {

        public AudioSource[] globalMusic;
        public override void Start()
        {
            base.Start();
            switch (bpm)
            {
                case (float)BPM.Slow:
                    globalMusic[0].Play();
                    break;
                case (float)BPM.SuperFast:
                    globalMusic[1].Play();
                    break;
                default:
                    break;
            }

        }

        //Update called on a fixed time
        public override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        //Update call every ticks
        public override void TimedUpdate()
        {
            base.TimedUpdate();
        }

    }

}




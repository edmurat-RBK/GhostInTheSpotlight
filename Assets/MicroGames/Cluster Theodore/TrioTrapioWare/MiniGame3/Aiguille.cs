using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrioTrapioWare
{
    namespace Swipe
    {
        public class Aiguille : TimedBehaviour
        {
            float rotationAiguille = 45f + 15.07f;

            public GameObject managerSwipe;
            private Initialisation initialisation;

            public override void Start()
            {
                base.Start(); //Do not erase this line!
                initialisation = managerSwipe.GetComponent<Initialisation>();
            }

            //FixedUpdate is called on a fixed time.
            public override void FixedUpdate()
            {
                base.FixedUpdate(); //Do not erase this line!

            }

            //TimedUpdate is called once every tick.
            public override void TimedUpdate()
            {
                ChangeTheRotation();
                StartCoroutine(Clock());
            }
            void ChangeTheRotation()
            {
                if (Tick <= 8)
                {
                    rotationAiguille = rotationAiguille - 45f;
                    transform.eulerAngles = new Vector3(0, 0, rotationAiguille);
                }
            }
            IEnumerator Clock()
            {
                if (Tick < 5)
                {
                    initialisation.source.PlayOneShot(initialisation.clock);
                }
                if (Tick >= 5 && Tick < 8)
                {
                    initialisation.source.PlayOneShot(initialisation.clock);
                    for (int i = 0; i < 3; i++)
                    {
                        yield return new WaitForSeconds((0.25f * 60) / bpm);
                        initialisation.source.PlayOneShot(initialisation.clockFast);
                    }
                }
            }
        }
    }
}
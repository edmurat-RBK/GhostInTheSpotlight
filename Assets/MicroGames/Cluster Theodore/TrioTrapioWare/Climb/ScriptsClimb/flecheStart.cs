using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrapioWare
{
    namespace Climb
    {
        public class flecheStart : MonoBehaviour
        {
            // Start is called before the first frame update
            void Start()
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                StartCoroutine(StartFade());
            }

           IEnumerator StartFade()
            {
                for (int i = 0; i < 4; i++)
                {
                    gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    yield return new WaitForSeconds(0.5f);
                    gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    yield return new WaitForSeconds(0.5f);
                }
               
            }
        }
    }
}




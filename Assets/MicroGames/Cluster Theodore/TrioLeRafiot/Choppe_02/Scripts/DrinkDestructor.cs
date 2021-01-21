using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Testing;

namespace LeRafiot
{
    namespace Choppe
    {
        public class DrinkDestructor : MonoBehaviour
        {
            private void OnTriggerEnter2D(Collider2D collision)
            {
                if (collision.gameObject.CompareTag("Enemy2"))
                {
                    DrinkManager.Instance.canSpawn = false;
                    Destroy(collision.gameObject);
                    Manager.Instance.Result(false);
                    SoundManagerChoppe.Instance.sfxSound[1].Play();
                }
                else
                {
                    Destroy(collision.gameObject);
                }
            }

        }
    }   
}

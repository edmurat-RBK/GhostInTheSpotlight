using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Testing;


namespace LeRafiot
{
    namespace Planche
    {
        /// <summary>
        /// Guillaume Rogé
        /// This script trigger the detection of player
        /// </summary>

        public class CollisionDetection : MonoBehaviour
        {
            public bool enemyInZone;
            public bool playerInZone;

            private void Start()
            {
                enemyInZone = false;
                playerInZone = false;

            }

            private void Update()
            {
                if(playerInZone && enemyInZone && !Manager.Instance.panel.activeSelf)
                {
                    PlayerController.Instance.canMove = false;
                    Manager.Instance.Result(false);
                    SoundManagerPlanche.Instance.sfxSound[1].Play();
                }
            }

            private void OnTriggerEnter2D(Collider2D collision)
            {
                if (collision.gameObject.tag == ("Ennemy1"))
                {
                    enemyInZone = true;
                }

                if (collision.gameObject.tag == ("Player"))
                {
                    playerInZone = true;
                }
            }

            private void OnTriggerExit2D(Collider2D collision)
            {
                if (collision.gameObject.tag == ("Ennemy1"))
                {
                    enemyInZone = false;
                }

                if (collision.gameObject.tag == ("Player"))
                {
                    playerInZone = false;
                }
            }
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrapioWare
{
    namespace Climb
    {
        public class BirdSpawner : MonoBehaviour
        {
            // Start is called before the first frame update

            private enum Level { Un, Deux, Trois };

            [SerializeField] private bool isRight = false;

            private Level myLevel;

            private float fireRateLevel;

            public bool selected = false;

            [SerializeField] private GameObject bird;

            void Update()
            {
                if (selected)
                {
                    selected = false;
                    StartCoroutine(SpawnBirds());
                }
            }

            IEnumerator SpawnBirds()
            {
                fireRateLevel = Random.Range(2f, 4f);

                GameObject storedBird = Instantiate(bird, transform.position, Quaternion.identity);
                storedBird.transform.parent = ClimbGameManager.Instance.transform;
                if (!isRight)
                {
                    storedBird.GetComponent<BirdBehaviour>().goLeft = false;
                    storedBird.GetComponent<BirdBehaviour>().goRight = true;
                    storedBird.GetComponent<SpriteRenderer>().flipX = true;
                }
                yield return new WaitForSeconds(fireRateLevel);

                if (ClimbGameManager.Instance.needToStop == false)
                {
                    StartCoroutine(SpawnBirds());
                }

               
            }

        }
    }
}


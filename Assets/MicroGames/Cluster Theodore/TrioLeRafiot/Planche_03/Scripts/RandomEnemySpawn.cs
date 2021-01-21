using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Testing;

namespace LeRafiot
{
    namespace Planche
    {
        /// <summary>
        /// Guillaume Rogé
        /// Script who choice x random target and activate it
        /// </summary>

        public class RandomEnemySpawn : TimedBehaviour
        {
            public static RandomEnemySpawn Instance;

            #region Variable
            [Header("Number of target per Tic")]
            public int numberRandomSpawn;

            [Header("Target list")]
            public List<GameObject> target = new List<GameObject>();

            [Header("Random Number")]
            public List<int> numberToChose = new List<int>(new int[]{0,1,2,3,4});
            #endregion


            public override void Start()
            {
                base.Start(); //Do not erase this line!

                ManagerInit();
            }

            //FixedUpdate is called on a fixed time.
            public override void FixedUpdate()
            {
                base.FixedUpdate(); //Do not erase this line!
            }

            //TimedUpdate is called once every tick.
            public override void TimedUpdate()
            {
                if (!Manager.Instance.panel.activeSelf)
                {
                    if (numberToChose.Count >= numberRandomSpawn)
                    {
                        PickRandomNumber(0);
                    }
                    else if (numberToChose.Count >= numberRandomSpawn - 1)
                    {
                        PickRandomNumber(1);
                    }
                    else if (numberToChose.Count >= numberRandomSpawn - 2)
                    {
                        PickRandomNumber(2);
                    }
                    else if (numberToChose.Count >= numberRandomSpawn - 3)
                    {
                        PickRandomNumber(3);
                    }
                    else
                    {
                        //Do nothing
                    }
                }
            }

            void PickRandomNumber(int deleteRange)
            {
                for (int i = 0; i < numberRandomSpawn - deleteRange; i++)
                {
                    int random = numberToChose[Random.Range(0, (numberToChose.Count))];

                    target[random].GetComponent<Target>().numberPicked = random;
                    target[random].GetComponent<Target>().activate = true;

                    numberToChose.Remove(random);
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
        }
    }
}
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
        /// This script is the behavior of target
        /// </summary>

        public class Target : TimedBehaviour
        {
            public enum wichEnemy
            {
                seagull,
                canonBall,
                shark,
            }

            #region Variable
            [Header("Enemy")]
            public GameObject enemy;
            public int tickEnemyToTravel;

            private GameObject actualEnemy;

            [Header("Enemy Position")]
            public Transform way;

            [Header("Alert")]
            public int tickToIncrase;
            public Vector2 startScale;
            public Vector2 finalScale;

            [Header ("Start bool")]
            public bool activate;

            public wichEnemy enemyChosen;

            private bool coolDown;

            [HideInInspector] public int numberPicked;
            #endregion

            public override void Start()
            {
                base.Start(); //Do not erase this line!

                transform.localScale = startScale;
            }

            public override void FixedUpdate()
            {
                base.FixedUpdate(); //Do not erase this line!    

                if (activate == true)
                {
                    if (coolDown == false)
                    {
                        coolDown = true;
                        StartCoroutine(IncreaseScale(startScale, finalScale, (tickToIncrase * (60 / bpm))));
                    }
                }
            }

            public override void TimedUpdate()
            {
  
            }

            public IEnumerator IncreaseScale(Vector2 scale, Vector2 endScale, float timeToFade)
            {
                Vector2 currentScale = scale;
                float t = 0;
                while (t < 1)
                {
                    t += Time.deltaTime / timeToFade;
                    transform.localScale = Vector3.Lerp(currentScale, endScale, t);
                    yield return null;
                }
                activate = false;
                coolDown = false;
                transform.localScale = startScale;

                RandomEnemySpawn.Instance.numberToChose.Add(numberPicked);

                if (enemy != null)
                {
                    actualEnemy = Instantiate(enemy, way.GetChild(0).transform.position, way.GetChild(0).transform.rotation);
                    StartCoroutine(MoveToPositioninCurve(actualEnemy.transform, (tickEnemyToTravel * (60 / bpm))));

                    switch (enemyChosen)
                    {
                        case wichEnemy.seagull:
                            SoundManagerPlanche.Instance.sfxSound[2].Play();
                            break;
                        case wichEnemy.canonBall:
                            SoundManagerPlanche.Instance.sfxSound[3].Play();
                            break;
                        case wichEnemy.shark:
                            SoundManagerPlanche.Instance.sfxSound[4].Play();
                            break;
                        default:
                            break;
                    }
                }

            }

            public IEnumerator MoveToPositioninCurve(Transform transform, float timeToMove)
            {
                Vector2 p0 = way.GetChild(0).position;
                Vector2 p1 = way.GetChild(1).position;
                Vector2 p2 = way.GetChild(2).position;
                Vector2 p3 = way.GetChild(3).position;

                float t = 0;
                while (t < 1)
                {
                    t += Time.deltaTime / timeToMove;

                    transform.position = Mathf.Pow(1 - t, 3) * p0 +
                        3 * Mathf.Pow(1 - t, 2) * t * p1 +
                        3 * (1 - t) * Mathf.Pow(t, 2) * p2 +
                        Mathf.Pow(t, 3) * p3;

                    //transform.rotation = Quaternion.Lerp(way.GetChild(0).transform.rotation, way.GetChild(3).transform.rotation, t);

                    yield return null;
                }
            }
        }

    }
}

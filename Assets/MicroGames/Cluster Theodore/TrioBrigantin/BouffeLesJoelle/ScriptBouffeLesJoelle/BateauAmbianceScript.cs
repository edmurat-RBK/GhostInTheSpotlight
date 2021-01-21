using System.Collections;
using UnityEngine;

namespace Brigantin
{
    namespace BouffeLesJoelle
    {
        public class BateauAmbianceScript : MonoBehaviour
        {
            #region Initialization
            Vector3 startPosition;
            [SerializeField] [Range(0, 5)] float usualNoise = 1;
            [SerializeField] [Range(0, 5)] float celebratingNoise = 3;
            [SerializeField] [Range(0, 2)] float celebratingTimeInTick = 1;
            [SerializeField] [Range(0, 0.2f)] float timeBetweenNoise = 0.1f;
            float celebTimer = 0;
            float timer = 0;
            bool managerDependantInitDone = false;
            #endregion

            void Start()
            {
                startPosition = transform.position;
            }

            void AddEvent()
            {
                LUE_Manager.Instance.OnEnemyKilled += CelebrateJoelle;
            }

            private void OnDisable()
            {
                if (LUE_Manager.Instance)
                {
                    LUE_Manager.Instance.OnEnemyKilled -= CelebrateJoelle;
                }
            }


            void Update()
            {
                if (!managerDependantInitDone)
                {
                    if (LUE_Manager.Instance)
                    {
                        AddEvent();
                        managerDependantInitDone = true;
                    }
                }
                if (timer <= 0)
                {
                    if (celebTimer <= 0)
                    {
                        transform.position = new Vector3(
                            startPosition.x + Random.Range(-usualNoise, usualNoise),
                            startPosition.y + Random.Range(-usualNoise, usualNoise));
                    }
                    else
                    {
                        transform.position = new Vector3(
                            startPosition.x + Random.Range(-celebratingNoise, celebratingNoise),
                            startPosition.y + Random.Range(-celebratingNoise, celebratingNoise));
                    }
                    timer = celebTimer <= 0 ? timeBetweenNoise * 0.5f : timeBetweenNoise;
                }
                celebTimer -= Time.deltaTime;
                timer -= Time.deltaTime;
            }

            public void CelebrateJoelle()
            {
                celebTimer = TtT(celebratingTimeInTick);
                timer = 0f;
            }

            float TtT(float tickNumber)
            {
                return tickNumber * 60 / LUE_Manager.Instance.SceneBPM;
            }
        }
    }
}

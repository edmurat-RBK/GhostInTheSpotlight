using System;
using System.Collections;
using UnityEngine;
using Testing;

namespace Brigantin
{
    namespace BouffeLesJoelle
    {
        /// <summary>
        /// Ludovic Eugenot
        /// </summary>
        public class LUE_Manager : TimedBehaviour
        {
            #region Initiatlization
            public static LUE_Manager Instance;
            [Header("Check if on private scene")]
            public bool isOnMyPrivateScene = true;
            public bool isUsingMyOwnUI = true;
            [Header("Testing related")]
            public Difficulty notTestingSceneDifficulty;
            public BPM notTestingSceneBpm;
            public float SceneBPM { get { return _mySceneBPM; } }
            private float _mySceneBPM;
            public float mapBoundariesX = 8f;
            public float mapBoundariesY = 4f;

            [Header("Gameobjects to reference")]
            [SerializeField] Spawner spawner;
            [SerializeField] TMPro.TMP_Text victoryScreen;
            [SerializeField] TMPro.TMP_Text defeatScreen;
            [SerializeField] GameObject victoryScene;
            [SerializeField] GameObject defeatScene;
            [SerializeField] Camera levelCamera;
            [SerializeField] Camera victoryCamera;
            [SerializeField] Camera defeatCamera;

            #region Code related
            [HideInInspector] public bool gameHasStarted = false;
            [HideInInspector] public bool gameIsRunning = false;
            [HideInInspector] public bool oneEnemyMissed = false;
            private int numberOFEnemiesToEat;

            [HideInInspector] public AudioSource musicSource;
            [HideInInspector] public double startTime;

            public event Action OnEnemyKilled;
            private float resultWaitTime;
            private bool resultSeen = false;

            void OneEnemyKilled()
            {
                OnEnemyKilled?.Invoke();
            }
            #endregion
            #endregion

            void Start()
            {
                base.Start();

                if (Instance == null)
                {
                    Instance = this;
                }
                else
                {
                    Destroy(this);
                }

                gameHasStarted = true;
                spawner = spawner ? spawner : FindObjectOfType<Spawner>();
                AudioSource[] sources = transform.GetChild(0).GetComponents<AudioSource>();
                musicSource = sources[0];
                //sources[1] est l'ambiance

                #region Victory and defeat objects enabled to false
                if (!victoryScreen) Debug.LogError("You must reference the Victory Text");
                if (!defeatScreen) Debug.LogError("You must reference the Loss Text");
                if (!victoryScene) Debug.LogError("You must reference the Victory Scene");
                if (!defeatScene) Debug.LogError("You must reference the Loss Scene");
                if (!victoryCamera) Debug.LogError("You must reference the Victory Camera");
                if (!defeatCamera) Debug.LogError("You must reference the Loss Camera");
                victoryScreen.enabled = false;
                defeatScreen.enabled = false;
                victoryScreen.transform.root.gameObject.SetActive(false);
                victoryScene?.SetActive(false);
                defeatScene?.SetActive(false);
                victoryCamera.enabled = false;
                defeatCamera.enabled = false;
                #endregion

                if (isOnMyPrivateScene)
                {
                    _mySceneBPM = (int)notTestingSceneBpm;
                    currentDifficulty = notTestingSceneDifficulty;
                }
                else
                {
                    _mySceneBPM = bpm;
                }

                startTime = Time.time;
                SetSpawnerStats();

                numberOFEnemiesToEat = spawner.numberOfEnemiesToSpawn;
                resultWaitTime = TtT(0.5f);

                gameIsRunning = true;
            }
            public void Update()
            {
                CheckVictoryAndDefeatConditions();
            }

            private void CheckVictoryAndDefeatConditions()
            {
                if (!resultSeen)
                {
                    if (oneEnemyMissed || numberOFEnemiesToEat == 0)
                    {
                        TriggerEndResult();
                    }
                }
                else
                {
                    if (oneEnemyMissed)
                    {
                        Defeat();
                    }
                    if (numberOFEnemiesToEat == 0)
                    {
                        Victory();
                    }
                }
            }

            void TriggerEndResult()
            {
                resultWaitTime -= Time.deltaTime;
                if (resultWaitTime < 0)
                {
                    resultSeen = true;
                }
            }

            void Victory()
            {
                gameIsRunning = false;
                if (isUsingMyOwnUI)
                {
                    victoryScreen.transform.root.gameObject.SetActive(true);
                    victoryScreen.enabled = true;
                }

                victoryScene.SetActive(true);
                levelCamera.enabled = false;
                victoryCamera.enabled = true;
                if (!isOnMyPrivateScene)
                {
                    Manager.Instance.Result(true);
                }
            }

            void Defeat()
            {
                gameIsRunning = false;
                if (isUsingMyOwnUI)
                {
                    defeatScreen.transform.root.gameObject.SetActive(true);
                    defeatScreen.enabled = true;
                }

                defeatScene.SetActive(true);
                levelCamera.enabled = false;
                defeatCamera.enabled = true;
                if (!isOnMyPrivateScene)
                {
                    Manager.Instance.Result(false);
                }
            }

            private void SetSpawnerStats()
            {
                switch (currentDifficulty)
                {
                    case Difficulty.EASY:
                        spawner.minimumDistanceBetween2Spawns = 6f;
                        spawner.maximumDistanceBetween2Spawns= 12f;
                        spawner.minimumDistanceFromApparitionToSpawn = 4f;
                        spawner.maximumDistanceFromApparitionToSpawn = 9f;
                        switch (bpm)
                        {
                            case 120:
                                spawner.lastEnemyDeathTime = TtT(5);
                                spawner.numberOfEnemiesToSpawn = 1;
                                spawner.timeBetweenEnemyDeaths = TtT(3);
                                break;
                            case 140:
                                spawner.lastEnemyDeathTime = TtT(5);
                                spawner.numberOfEnemiesToSpawn = 1;
                                spawner.timeBetweenEnemyDeaths = TtT(3);
                                break;
                            default:
                                spawner.lastEnemyDeathTime = TtT(6);
                                spawner.numberOfEnemiesToSpawn = 2;
                                spawner.timeBetweenEnemyDeaths = TtT(2);
                                break;
                        }

                        spawner.numberOfCloudsToSpawn = 1;
                        spawner.cloudDistance = 3;
                        spawner.cloudAlpha = 0.5f;
                        spawner.cloudEndAlpha = 0.4f;
                        break;
                    case Difficulty.MEDIUM:
                        spawner.lastEnemyDeathTime = TtT(6);
                        spawner.minimumDistanceBetween2Spawns = 5f;
                        spawner.maximumDistanceBetween2Spawns = 9f;
                        spawner.minimumDistanceFromApparitionToSpawn = 6f;
                        spawner.maximumDistanceFromApparitionToSpawn = 12f;
                        switch (bpm)
                        {
                            case 120:
                                spawner.numberOfEnemiesToSpawn = 2;
                                spawner.timeBetweenEnemyDeaths = TtT(2);
                                break;
                            case 140:
                                spawner.numberOfEnemiesToSpawn = 2;
                                spawner.timeBetweenEnemyDeaths = TtT(2);
                                break;
                            default:
                                spawner.numberOfEnemiesToSpawn = 3;
                                spawner.timeBetweenEnemyDeaths = TtT(1);
                                break;
                        }

                        spawner.numberOfCloudsToSpawn = 3;
                        spawner.cloudDistance = 5;
                        spawner.cloudAlpha = 0.7f;
                        spawner.cloudEndAlpha = 0.5f;
                        break;
                    case Difficulty.HARD:
                        spawner.minimumDistanceBetween2Spawns = 7f;
                        spawner.lastEnemyDeathTime = TtT(6);
                        spawner.minimumDistanceFromApparitionToSpawn = 7f;
                        spawner.maximumDistanceFromApparitionToSpawn = 15f;
                        switch (bpm)
                        {
                            case 60:
                                spawner.maximumDistanceBetween2Spawns = 13f;
                                spawner.numberOfEnemiesToSpawn = 4;
                                spawner.timeBetweenEnemyDeaths = TtT(1);
                                break;
                            case 90:
                                spawner.maximumDistanceBetween2Spawns = 13f;
                                spawner.numberOfEnemiesToSpawn = 3;
                                spawner.timeBetweenEnemyDeaths = TtT(1);
                                break;
                            case 120:
                                spawner.maximumDistanceBetween2Spawns = 9f;
                                spawner.numberOfEnemiesToSpawn = 2;
                                spawner.timeBetweenEnemyDeaths = TtT(2);
                                break;
                            case 140:
                                spawner.maximumDistanceBetween2Spawns = 9f;
                                spawner.numberOfEnemiesToSpawn = 2;
                                spawner.timeBetweenEnemyDeaths = TtT(2);
                                break;
                            default:
                                break;
                        }

                        spawner.numberOfCloudsToSpawn = 40;
                        spawner.cloudDistance = 8;
                        spawner.cloudAlpha = 0.9f;
                        spawner.cloudEndAlpha = 0.4f;
                        break;
                }

            }

            public void DecrementEnemiesToEat()
            {
                numberOFEnemiesToEat--;
                OneEnemyKilled();
            }

            /// <summary>
            /// Converting ticks to time
            /// </summary>
            /// <param name="tickNumber">Number of ticks to convert to a time value.</param>
            /// <returns></returns>
            float TtT(float tickNumber)
            {
                return tickNumber * 60 / SceneBPM;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Testing;

namespace TrioBrigantin
{
    namespace CouteauxTires
    {
        public class ACouteauxTiré_Manager : TimedBehaviour
        {
            #region Variables
            static public ACouteauxTiré_Manager instance;
            public ACouteauTires_SoundManager soundManager;

            [Header("Enemy Setup Fields")]

            [SerializeField] int numberOfEnemies; //Serialize field when testing
            int ammo;
            [HideInInspector] public List<Enemy> enemiesAlive = new List<Enemy>();
            [HideInInspector] public List<Enemy> enemiesKilled = new List<Enemy>();
            bool resultSent = false;
            [SerializeField] AmmoCounter timeTick;

            public GameObject baseEnemy;
            public GameObject superEnemy;
            [SerializeField] GameObject[] spawnSets;
            public GameObject spawnSetAnchor;
            GameObject chosenSpawnSet;
            EnemySpawnerMag chosenSpawner;

            bool doSuperEnemySpawning;
            [Range(0, 100)]
            [SerializeField] int superEnemyChance = 4; //Probability of Super Enemy, 0 means will be sure to not spawn

            [Header("For End Screen")]
            [SerializeField] GameObject gameScene;
            [SerializeField] GameObject winScene;
            [SerializeField] GameObject loseScene;
            bool onEndScreen = false;
            bool winCon;
            #endregion

            private void Awake()
            {
                if (instance == null)
                    instance = this;
                else
                    Destroy(gameObject);
            }

            public override void Start()
            {
                base.Start(); //Do not erase this line!
                
                switch (bpm)
                {
                    case 60:
                        CrosshairController.instance.movementSpeed = 6.7f;
                        break;

                    case 90:
                        CrosshairController.instance.movementSpeed = 8;
                        break;

                    case 120:
                        CrosshairController.instance.movementSpeed = 9.7f;
                        break;

                    case 140:
                        CrosshairController.instance.movementSpeed = 12.2f;
                        break;

                    default:
                        break;
                }

                switch (currentDifficulty)
                {
                    case Difficulty.EASY:
                        numberOfEnemies = 3;
                        ammo = numberOfEnemies;
                        break;

                    case Difficulty.MEDIUM:
                        numberOfEnemies = 4;
                        doSuperEnemySpawning = DecideSuperEnemySpawn();

                        if (doSuperEnemySpawning)
                            ammo = numberOfEnemies + 1;
                        else
                            ammo = numberOfEnemies;

                        break;

                    case Difficulty.HARD:
                        numberOfEnemies = 5;
                        doSuperEnemySpawning = DecideSuperEnemySpawn();

                        if (doSuperEnemySpawning)
                            ammo = numberOfEnemies + 1;
                        else
                            ammo = numberOfEnemies;

                        break;
                }

                InstantiateSpawner(spawnSets[(int)currentDifficulty]);
                timeTick.InitAmmoCounter(8);
                Debug.Log("Ammo left: " + ammo);
                Debug.Log(Tick);
            }

            //FixedUpdate is called on a fixed time.
            public override void FixedUpdate()
            {
                base.FixedUpdate(); //Do not erase this line!
                //if (ammo == 0 && enemiesKilled.Count == numberOfEnemies)
                //{
                //    Manager.Instance.Result(true);
                //}
            }

            //TimedUpdate is called once every tick.
            public override void TimedUpdate()
            {
                Debug.Log("Ennemies locked: " + enemiesKilled.Count);
                Debug.Log(Tick);

                if (resultSent)
                    return;

                timeTick.DiscountKnife(8 - Tick);

                if(ammo == 0 && !onEndScreen)
                {
                    gameScene.SetActive(false);

                    if (enemiesKilled.Count < numberOfEnemies)
                    {
                        loseScene.SetActive(true);
                        if (doSuperEnemySpawning)
                            soundManager.Play("SuperEnemySnicker");
                        soundManager.Play("PistolHammer");

                        winCon = false;
                    }
                    else if (enemiesKilled.Count == numberOfEnemies)
                    {
                        winScene.SetActive(true);
                        soundManager.Play("KnifeHit");
                        if(doSuperEnemySpawning)
                            soundManager.Play("SuperEnemyDeath");
                        soundManager.Play("EnemyDeath");

                        winCon = true;
                    }

                    onEndScreen = true;
                }

                if (Tick == 8)
                {
                    if (!onEndScreen)
                        Manager.Instance.Result(false);
                    else
                        Manager.Instance.Result(winCon);
                    
                    resultSent = true;
                }
            }

            #region AmmoInteraction
            public void MinusAmmo()
            {
                ammo--;
                Debug.Log("Ammo left: " + ammo);
            }

            public bool GetAmmoZero()
            {
                if (ammo == 0)
                    return true;
                else
                    return false;
            }
            #endregion

            void InstantiateSpawner(GameObject _spawnSet)
            {
                chosenSpawnSet = Instantiate(_spawnSet, spawnSetAnchor.transform.position, Quaternion.identity);
                chosenSpawnSet.transform.SetParent(spawnSetAnchor.transform);
                chosenSpawner = chosenSpawnSet.GetComponent<EnemySpawnerMag>();
                chosenSpawner.GetEnemyNumber(numberOfEnemies, doSuperEnemySpawning);
                chosenSpawner.SpawnBehavior();
                Debug.Log(chosenSpawnSet.name + " was instatiated");
            }

            bool DecideSuperEnemySpawn()
            {
                int superSpawnChance = Random.Range(1, 101);
                if(superSpawnChance <= superEnemyChance)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
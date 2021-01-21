using UnityEngine;

namespace Brigantin
{
    namespace BouffeLesJoelle
    {
        /// <summary>
        /// Ludovic Eugenot
        /// </summary>
        public class Spawner : MonoBehaviour
        {
            #region Initiatlization
            public GameObject enemyToSpawn;
            [HideInInspector] public float lastEnemyDeathTime = 1f;
            [HideInInspector] public float timeBetweenEnemyDeaths = 1f;
            [HideInInspector] public int numberOfEnemiesToSpawn = 1;
            [HideInInspector] public float minimumDistanceBetween2Spawns = 3f;
            [HideInInspector] public float maximumDistanceBetween2Spawns = 10f;
            [HideInInspector] public float minimumDistanceFromApparitionToSpawn = 4f;
            [HideInInspector] public float maximumDistanceFromApparitionToSpawn = 9f;

            public GameObject cloudToSpawn;
            [HideInInspector] public int numberOfCloudsToSpawn = 1;
            [HideInInspector] public float cloudDistance = 2f;
            [HideInInspector] public float cloudAlpha = 2f;
            [HideInInspector] public float cloudEndAlpha = 2f;

            bool amActive = false;
            bool enemiesSpawned = false;
            bool lastEnemySpawned = false;
            private Vector2 previousSpawnPos;
            int enemyIndex = 0;
            #endregion

            void Start()
            {
                if (!enemyToSpawn)
                    Debug.LogError("Need Enemy Prefab to work");
                transform.position = Vector3.zero;
                transform.localScale = Vector3.one;
                previousSpawnPos = new Vector2(100, 100);
            }

            void Update()
            {
                if (!enemiesSpawned)
                {
                    for (int i = 0; i < numberOfEnemiesToSpawn; i++)
                    {
                        SpawnOneEnemy(lastEnemyDeathTime - timeBetweenEnemyDeaths * i);
                    }
                    enemiesSpawned = true;

                    for (int i = 0; i < numberOfCloudsToSpawn; i++)
                    {
                        SpawnOneCloud();
                    }
                }
            }

            void SpawnOneCloud()
            {
                Vector2 spawnBoundaries = new Vector2(LUE_Manager.Instance.mapBoundariesX, LUE_Manager.Instance.mapBoundariesY);
                Clouds cloud = Instantiate(cloudToSpawn, RandomPositionWithinBoundariesCloudsEdition(spawnBoundaries) + Vector3.up * 1.5f, Quaternion.identity, transform).GetComponent<Clouds>();
                cloud.GetComponent<SpriteRenderer>().flipX = Random.Range(0, 2) > 0 ? true : false;
                cloud.cloudDistance = cloud.transform.position.x < 0 ? cloudDistance : -cloudDistance;
                cloud.alpha = cloudAlpha;

            }

            void SpawnOneEnemy(float timeOfDeath)
            {
                Vector2 spawnBoundaries = new Vector2(LUE_Manager.Instance.mapBoundariesX - 1, LUE_Manager.Instance.mapBoundariesY - 1);
                Vector2 vulnerablePosition = RandomPositionWithinBoundaries(spawnBoundaries);
                Enemy enemy = Instantiate(enemyToSpawn, RandomPositionOverBoundaries(spawnBoundaries, vulnerablePosition), Quaternion.identity, transform).GetComponent<Enemy>();
                enemy.vulnerablePosition = vulnerablePosition;
                enemy.transform.name = "Enemy" + enemyIndex;
                enemyIndex++;
                enemy.worldTimeOfDeath = timeOfDeath;
            }

            Vector3 RandomPositionWithinBoundaries(Vector2 spawnBoundaries)
            {
                int iterations = 0;
                Vector3 position = new Vector3(Random.Range(-spawnBoundaries.x, spawnBoundaries.x), Random.Range(-spawnBoundaries.y, spawnBoundaries.y));

                if (lastEnemySpawned)
                {
                    while ((Vector2.Distance(position, previousSpawnPos) > maximumDistanceBetween2Spawns || Vector2.Distance(position, previousSpawnPos) < minimumDistanceBetween2Spawns) && iterations < 12)
                    {
                        iterations++;
                        position = new Vector3(Random.Range(-spawnBoundaries.x, spawnBoundaries.x), Random.Range(-spawnBoundaries.y, spawnBoundaries.y));
                        //MARCHE PAS

                    }
                }
                else
                {
                    position = new Vector3(Random.Range(0, 2) > 0 ? -spawnBoundaries.x * .5f : spawnBoundaries.x * .5f, Random.Range(0, 2) > 0 ? -spawnBoundaries.y * .5f : spawnBoundaries.y * .5f);
                    lastEnemySpawned = true;
                }

                if (iterations > 10)
                {
                    Debug.LogWarning("impossible de trouver un spawn, le dernier spawn était : " + previousSpawnPos);
                }
                previousSpawnPos = position;
                return position;
            }
            Vector3 RandomPositionWithinBoundariesCloudsEdition(Vector2 spawnBoundaries)
            {
                Vector3 position = new Vector3(Random.Range(-spawnBoundaries.x - 3, spawnBoundaries.x + 3), Random.Range(0, spawnBoundaries.y));
                return position;
            }
            Vector3 RandomPositionOverBoundaries(Vector2 spawnBoundaries, Vector2 spawnPosition)
            {
                int iterations = 0;
                Vector3 position = new Vector3(Random.Range(-spawnBoundaries.x - 4, spawnBoundaries.x + 4), spawnBoundaries.y + 3);

                if (lastEnemySpawned)
                {
                    while ((Vector2.Distance(position, spawnPosition) > maximumDistanceFromApparitionToSpawn || Vector2.Distance(position, spawnPosition) < minimumDistanceFromApparitionToSpawn) && iterations < 15)
                    {
                        iterations++;
                        position = new Vector3(Random.Range(-spawnBoundaries.x - 4, spawnBoundaries.x + 4), spawnBoundaries.y + 3);
                    }
                }
                else
                {
                    lastEnemySpawned = true;
                }
                return position;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Testing;

namespace LeRafiot
{
    namespace Planche
    {
        /// <summary>
        /// Antoine LEROUX
        /// This script is use to manage the difficulty depending to the level (1,2,3) 
        /// All gameplay values changing need to be refer in this script
        /// </summary>

        public class LevelDifficultyPlanche : TimedBehaviour
        {
            #region Variables
            [Header("Level EASY")]
            [Range(1, 5)] public int numberEnemyPerTickEasy;
            [Range(1, 7)] public int alertIncreaseInTickEasy;

            [Space]
            public int sharkTravelInTickEasy;
            public int canonBallTravelInTickEasy;
            public int seagullTravelInTickEasy;


            [Header("Level MEDIUM")]
            [Range(1, 5)] public int numberEnemyPerTickMedium;
            [Range(1, 7)] public int alertIncreaseInTickMedium;

            [Space]
            public int sharkTravelInTickMedium;
            public int canonBallTravelInTickMedium;
            public int seagullTravelInTickMedium;

            [Header("Level HARD")]
            [Range(1, 5)] public int numberEnemyPerTickHard;
            [Range(1, 7)] public int alertIncreaseInTickHard;

            [Space]
            public int sharkTravelInTickHard;
            public int canonBallTravelInTickHard;
            public int seagullTravelInTickHard;

            public List<GameObject> cible = new List<GameObject>();

            #endregion

            public override void Start()
            {
                base.Start();

                SetValues();
            }

            //FixedUpdate is called on a fixed time.
            public override void FixedUpdate()
            {
                base.FixedUpdate();

            }

            //TimedUpdate is called once every tick.
            public override void TimedUpdate()
            {

            }

            void SetValues()
            {
                if (Manager.Instance.currentDifficulty == Difficulty.EASY)
                {
                    RandomEnemySpawn.Instance.numberRandomSpawn = numberEnemyPerTickEasy;

                    for (int i = 0; i < cible.Count; i++)
                    {
                        cible[i].GetComponentInChildren<Target>().tickToIncrase = alertIncreaseInTickEasy;
                    }

                    //Shark
                    cible[0].GetComponentInChildren<Target>().tickEnemyToTravel = sharkTravelInTickEasy;
                    cible[4].GetComponentInChildren<Target>().tickEnemyToTravel = sharkTravelInTickEasy;

                    //CanonBall
                    cible[1].GetComponentInChildren<Target>().tickEnemyToTravel = canonBallTravelInTickEasy;
                    cible[3].GetComponentInChildren<Target>().tickEnemyToTravel = canonBallTravelInTickEasy;

                    //Seagull
                    cible[2].GetComponentInChildren<Target>().tickEnemyToTravel = seagullTravelInTickEasy;
                }
                else if (Manager.Instance.currentDifficulty == Difficulty.MEDIUM)
                {
                    RandomEnemySpawn.Instance.numberRandomSpawn = numberEnemyPerTickMedium;

                    for (int i = 0; i < cible.Count; i++)
                    {
                        cible[i].GetComponentInChildren<Target>().tickToIncrase = alertIncreaseInTickMedium;
                    }

                    //Shark
                    cible[0].GetComponentInChildren<Target>().tickEnemyToTravel = sharkTravelInTickMedium;
                    cible[4].GetComponentInChildren<Target>().tickEnemyToTravel = sharkTravelInTickMedium;

                    //CanonBall
                    cible[1].GetComponentInChildren<Target>().tickEnemyToTravel = canonBallTravelInTickMedium;
                    cible[3].GetComponentInChildren<Target>().tickEnemyToTravel = canonBallTravelInTickMedium;

                    //Seagull
                    cible[2].GetComponentInChildren<Target>().tickEnemyToTravel = seagullTravelInTickMedium;
                }
                else if (Manager.Instance.currentDifficulty == Difficulty.HARD)
                {
                    RandomEnemySpawn.Instance.numberRandomSpawn = numberEnemyPerTickHard;

                    for (int i = 0; i < cible.Count; i++)
                    {
                        cible[i].GetComponentInChildren<Target>().tickToIncrase = alertIncreaseInTickHard;
                    }

                    //Shark
                    cible[0].GetComponentInChildren<Target>().tickEnemyToTravel = sharkTravelInTickHard;
                    cible[4].GetComponentInChildren<Target>().tickEnemyToTravel = sharkTravelInTickHard;

                    //CanonBall
                    cible[1].GetComponentInChildren<Target>().tickEnemyToTravel = canonBallTravelInTickHard;
                    cible[3].GetComponentInChildren<Target>().tickEnemyToTravel = canonBallTravelInTickHard;

                    //Seagull
                    cible[2].GetComponentInChildren<Target>().tickEnemyToTravel = seagullTravelInTickHard;
                }
            }
        }
    }
}
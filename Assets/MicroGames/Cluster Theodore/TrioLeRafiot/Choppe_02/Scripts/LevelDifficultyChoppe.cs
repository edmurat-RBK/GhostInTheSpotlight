using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Testing;

namespace LeRafiot
{
    namespace Choppe
    {
        /// <summary>
        /// Antoine LEROUX
        /// This script is use to manage the difficulty depending to the level (1,2,3) 
        /// All gameplay values changing need to be refer in this script
        /// </summary>

        public class LevelDifficultyChoppe : TimedBehaviour
        {
            #region Variables
            public DrinkManager managerScript;

            [Header("Level EASY")]
            public List<GameObject> drinkListEasy = new List<GameObject>();
            public GameObject backgroundEasy;


            [Header("Level MEDIUM")]
            public List<GameObject> drinkListMedium = new List<GameObject>();
            public GameObject backgroundMedium;


            [Header("Level HARD")]
            public int tickBubbleDisappear;
            public List<GameObject> drinkListHard = new List<GameObject>();
            public GameObject backgroundHard;

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
                    managerScript.drinkList = new List<GameObject>(drinkListEasy);
                    managerScript.vanishUi = false;
                    backgroundEasy.SetActive(true);
                    backgroundMedium.SetActive(false);
                    backgroundHard.SetActive(false);
                }
                else if (Manager.Instance.currentDifficulty == Difficulty.MEDIUM)
                {
                    managerScript.drinkList = new List<GameObject>(drinkListMedium);
                    managerScript.vanishUi = false;
                    backgroundEasy.SetActive(false);
                    backgroundMedium.SetActive(true);
                    backgroundHard.SetActive(false);
                }
                else if (Manager.Instance.currentDifficulty == Difficulty.HARD)
                {
                    managerScript.drinkList = new List<GameObject>(drinkListHard);
                    managerScript.tickToFade = tickBubbleDisappear;
                    managerScript.vanishUi = true;
                    backgroundEasy.SetActive(false);
                    backgroundMedium.SetActive(false);
                    backgroundHard.SetActive(true);
                }
            }
        }
    }
}
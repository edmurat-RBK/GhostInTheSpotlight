using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Testing;

namespace TrapioWare
{
    namespace Spin
    {
        public class SpinManager : TimedBehaviour
        {
            public DifficultySetup[] difficultySetups;
            public bool useCustomDifficulty;
            public int difficulty;
            public bool hasInlimitedTime;
            public GameObject[] holes;
            public GameObject canonHoleEffect;
            public GameObject canonHoleFinalEffect;
            public AudioClip[] canonBreakClips;
            public GameObject playerChara;
            public GameObject hammer;
            public GameObject ceillingWarn;
            public GameObject victoireGraph;

            [HideInInspector] public bool hasWon;
            [HideInInspector] public bool gameFinished;
            private AudioSource source;

            public override void Start()
            {
                base.Start();
                source = GetComponent<AudioSource>();

                if(!useCustomDifficulty)
                {
                    difficulty = (int)currentDifficulty;
                }
                InitializeDifficulty();

                for(int i = 0; i < holes.Length; i++)
                {
                    holes[i].SetActive(false);
                }

                victoireGraph.SetActive(false);
            }

            public override void FixedUpdate()
            {
                base.FixedUpdate();
            }

            public override void TimedUpdate()
            {
                base.TimedUpdate();
                if(Tick -1 < 7 && Tick - 1>= 0)
                {
                    SpawnHole(Tick - 1, false);
                }

                if(Tick == 8 && !hasInlimitedTime)
                {
                    if(!gameFinished)
                    {
                        Lose();
                    }
                    else if(hasWon)
                    {
                        Manager.Instance.Result(true);
                    }
                }
            }

            private void InitializeDifficulty()
            {
                for (int i = 0; i < 3; i++)
                {
                    difficultySetups[i].target.SetActive(false);
                    difficultySetups[i].background.SetActive(false);
                    difficultySetups[i].boundaries.SetActive(false);
                }

                difficultySetups[difficulty].target.SetActive(true);
                difficultySetups[difficulty].background.SetActive(true);
                difficultySetups[difficulty].boundaries.SetActive(true);

                switch(difficulty)
                {
                    case 0:
                        ceillingWarn.transform.position = new Vector2(ceillingWarn.transform.position.x, 5.2f);
                        break;

                    case 1:
                        ceillingWarn.transform.position = new Vector2(ceillingWarn.transform.position.x, 7.28f);
                        break;

                    case 2:
                        ceillingWarn.transform.position = new Vector2(ceillingWarn.transform.position.x, 7.28f);
                        break;
                }
            }

            public void Win()
            {
                if(!gameFinished)
                {
                    gameFinished = true;
                    hasWon = true;
                    StartCoroutine(EndAnim());
                }
            }


            public void Lose()
            {
                if (!gameFinished)
                {
                    gameFinished = true;
                    SpawnHole(7, true);
                    playerChara.SetActive(false);
                    hammer.SetActive(false);
                    Manager.Instance.Result(false);
                }
            }

            private void SpawnHole(int canonHoleIndex, bool isFinal)
            {
                Instantiate(!isFinal? canonHoleEffect : canonHoleFinalEffect, holes[canonHoleIndex].transform.position, Quaternion.identity);
                holes[canonHoleIndex].SetActive(true);
                source.pitch = Random.Range(0.8f, 1.2f);
                source.PlayOneShot(canonBreakClips[Random.Range(0, canonBreakClips.Length)]);
            }

            [System.Serializable]
            public class DifficultySetup
            {
                public GameObject target;
                public GameObject background;
                public GameObject boundaries;
            }

            private IEnumerator EndAnim()
            {
                victoireGraph.SetActive(true);
                ceillingWarn.SetActive(false);
                while (playerChara.transform.position.y < 100)
                {
                    yield return new WaitForEndOfFrame();
                    playerChara.transform.position = new Vector2(playerChara.transform.position.x, playerChara.transform.position.y + 0.3f);
                }
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Testing;


namespace TrapioWare
{
    namespace Climb
    {
        public class SunManager : TimedBehaviour
        {
            [SerializeField] private int timerDead = 0;

            public override void Start()
            {
                base.Start(); //Do not erase this line!
                
                switch (Manager.Instance.bpm)
                {
                    case BPM.Slow:
                        ClimbGameManager.Instance.mySpeed = 60;
                        break;
                    case BPM.Medium:
                        ClimbGameManager.Instance.mySpeed = 90;
                        break;
                    case BPM.Fast:
                        ClimbGameManager.Instance.mySpeed = 120;
                        break;
                    case BPM.SuperFast:
                        ClimbGameManager.Instance.mySpeed = 140;
                        break;
                    default:
                        break;
                }

                switch (Manager.Instance.currentDifficulty)
                {
                    case Difficulty.EASY:
                        ClimbGameManager.Instance.myDifficulty = 0;
                        Debug.Log(ClimbGameManager.Instance.myDifficulty);
                        ClimbGameManager.Instance.setDifficulty = true;
                        break;
                    case Difficulty.MEDIUM:
                        ClimbGameManager.Instance.myDifficulty = 1;
                        Debug.Log(ClimbGameManager.Instance.myDifficulty);
                        ClimbGameManager.Instance.setDifficulty = true;
                        break;
                    case Difficulty.HARD:
                        ClimbGameManager.Instance.myDifficulty = 2;
                        Debug.Log(ClimbGameManager.Instance.myDifficulty);
                        ClimbGameManager.Instance.setDifficulty = true;
                        break;
                    default:
                        break;
                }

            }

            //FixedUpdate is called on a fixed time.
            public override void FixedUpdate()
            {
                base.FixedUpdate(); //Do not erase this line!

                if (ClimbGameManager.Instance.win)
                {
                    Manager.Instance.Result(true);
                }

                if (ClimbGameManager.Instance.lose && ClimbGameManager.Instance.win == false)
                {
                    Manager.Instance.Result(false);
                }

            }

            //TimedUpdate is called once every tick.
            public override void TimedUpdate()
            {
                timerDead++;

                if (TrapioWare.Climb.ClimbGameManager.Instance.needToStop == false && timerDead !=8)
                {
                    gameObject.GetComponent<Animator>().SetTrigger("TickTrigger");
                }

                if(timerDead == 8 && ClimbGameManager.Instance.lose == false && ClimbGameManager.Instance.win == false)
                {
                    //Manager.Instance.Result(false);
                    Debug.Log("You Lose");
                    TrapioWare.Climb.ClimbGameManager.Instance.lose = true;
                    TrapioWare.Climb.ClimbGameManager.Instance.needToStop = true;
                }

            }
        }
    }
}
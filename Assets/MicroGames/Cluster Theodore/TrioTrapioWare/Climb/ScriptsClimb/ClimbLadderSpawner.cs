using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrioName
{
    namespace MiniGameName
    {
        public class ClimbLadderSpawner : MonoBehaviour
        {
            public GameObject ladderPrefab;


            // Start is called before the first frame update
            void Start()
            {
                if (TrapioWare.Climb.ClimbGameManager.Instance.numberOfLadder < TrapioWare.Climb.ClimbGameManager.Instance.numberOfLadderNeeded)
                {
                    GameObject instanciateLadder = Instantiate(ladderPrefab, transform.position, Quaternion.identity);
                    instanciateLadder.transform.parent = TrapioWare.Climb.ClimbGameManager.Instance.ladderParent.transform;
                    TrapioWare.Climb.ClimbGameManager.Instance.numberOfLadder += 1;
                    //Debug.Log("if");
                }
                else
                {
                    //Instancier le haut du Mat.
                    TrapioWare.Climb.ClimbGameManager.Instance.needToCheck = true;
                    //Debug.Log("else");
                }

            }
        }
    }
}
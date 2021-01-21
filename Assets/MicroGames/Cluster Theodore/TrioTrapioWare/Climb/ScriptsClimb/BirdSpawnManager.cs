using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrapioWare
{
    namespace Climb
    {
        public class BirdSpawnManager : MonoBehaviour
        {

            //GameObject with Children.
            [Header("OiseauManager")]
            public List<GameObject> OiseauGenerator1 = new List<GameObject>();
            public List<GameObject>  OiseauStoredPoints1 = new List<GameObject>();
            public List<GameObject> OiseauGenerator2 = new List<GameObject>();
            public List<GameObject> OiseauStoredPoints2 = new List<GameObject>();
            public List<GameObject> OiseauGenerator3 = new List<GameObject>();
            public List<GameObject> OiseauStoredPoints3 = new List<GameObject>();
            //Number of spawner available.
            [SerializeField] private int numberOfSpawner1 = 0;
            private int failed1 = 0;
            [SerializeField] private int numberOfSpawner2 = 0;
    


            [Header("Difficulty")]
            public int numberOfMaxGenerator;

            // Start is called before the first frame update
            void Update()
            {
                if (ClimbGameManager.Instance.setDifficulty)
                {
                    ClimbGameManager.Instance.setDifficulty = false;
                    SetDifficulty();
                    //SetDifficulty();
                }
            }

            void InitialisationGenerator1()
            {
                int random1 = Random.Range(0, OiseauGenerator1.Count);
                OiseauStoredPoints1.Add(OiseauGenerator1[random1]);
                OiseauGenerator1[random1].GetComponent<BirdSpawner>().selected = true;
                OiseauGenerator1.Remove(OiseauGenerator1[random1]);
                numberOfSpawner1++;
                if (numberOfSpawner1 != numberOfMaxGenerator)
                {
                    InitialisationGenerator1();
                }
            }

            void InitialisationGenerator2()
            {
                int random2 = Random.Range(0, OiseauGenerator2.Count);

                OiseauStoredPoints2.Add(OiseauGenerator2[random2]);
                OiseauGenerator2[random2].GetComponent<BirdSpawner>().selected = true;
                OiseauGenerator2.Remove(OiseauGenerator2[random2]);
                numberOfSpawner2++;
                if (numberOfSpawner2 != numberOfMaxGenerator)
                { 
                   
                    InitialisationGenerator2();
                }

            }

            void SetDifficultyAlt()
            {
                    numberOfMaxGenerator = Random.Range(2, 5);
                    InitialisationGenerator1();
                    InitialisationGenerator2();
            }


            void SetDifficulty()
            {
                //NumberOfMaxGenerator according difficulty
                switch (ClimbGameManager.Instance.myDifficulty)
                {
                    case 0:
                        numberOfMaxGenerator = 1;
                        Debug.Log(numberOfMaxGenerator);
                        InitialisationGenerator1();
                        InitialisationGenerator2();
                       
                        break;
                    case 1:
                        numberOfMaxGenerator = 2;
                        Debug.Log(numberOfMaxGenerator);
                        InitialisationGenerator1();
                        InitialisationGenerator2();
                       
                        break;
                    case 2:
                        numberOfMaxGenerator = 3;
                        Debug.Log(numberOfMaxGenerator);
                        InitialisationGenerator1();
                        InitialisationGenerator2();
                     
                        break;
                    default:
                        break;
                }
            }
        }
    }
}

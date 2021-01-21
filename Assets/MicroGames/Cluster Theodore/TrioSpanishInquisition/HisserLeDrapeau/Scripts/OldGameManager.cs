using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Testing;


namespace SpanishInquisition
{
    namespace HisserLeDrapeau
    {
        /// <summary>
        /// Adel Ahmed-Yahia
        /// </summary>

        public class OldGameManager : TimedBehaviour
        {
            private int QTEGen;
            private int numberOfButtons;
            private int buttonNumber;
            private bool easyGame;
            private bool medGame;
            private bool hardGame;
            private bool gameDone;
            private GameObject activeButton;
            private Transform flag;
            public Animator animator;

            private SoundManager sMngr;

            public override void Start()
            {
                base.Start();

                sMngr = GetComponentInChildren<SoundManager>();
                flag = GameObject.Find("/Graphs/Flag").transform;

                switch (currentDifficulty)
                {
                    case Difficulty.EASY:
                        easyGame = true;
                        break;

                    case Difficulty.MEDIUM:
                        medGame = true;
                        break;

                    case Difficulty.HARD:
                        hardGame = true;
                        break;
                }
            }

            public override void TimedUpdate()
            {
                if (Tick == 8 && gameDone == false)
                {
                    sMngr.PlayDefeat();
                    Debug.Log("You lose !)");
                    GameObject.Find("/Graphs/Defeat/Typo défaite").SetActive(true);
                    GameObject.Find("/Graphs/Defeat/Feedback défaite").SetActive(true);
                    Manager.Instance.Result(false);
                }

                if (Tick == 8 && gameDone == true)
                {
                    sMngr.PlayVictory();
                    Manager.Instance.Result(true);
                    sMngr.PlayFlagFirst();
                    Debug.Log("You win !)");
                    GameObject.Find("/Graphs/Victory/Typo victoire").SetActive(true);
                    GameObject.Find("/Graphs/Victory/Feedback victoire").SetActive(true);
                }
            }

            public void Update() 
            {             
                if (easyGame == true)
                {
                    StartCoroutine(GameStartDiffEasy());
                }

                if (medGame == true)
                {
                    StartCoroutine(GameStartDiffMed());
                }

                if (hardGame == true)
                {
                    StartCoroutine(GameStartDiffHard());
                }              
            }

            IEnumerator GameStartDiffEasy()
            {
                if (numberOfButtons < 3)
                {
                    QTEGen = Random.Range(1, 5);

                    if (QTEGen == 1)
                    {
                        Debug.Log("Bouton A");

                        if (numberOfButtons == 0)
                        {
                            activeButton = GameObject.Find("/Input/Buttons A/Button A 1");
                            activeButton.SetActive(true);
                        }

                        if (numberOfButtons == 1)
                        {
                            activeButton = GameObject.Find("/Input/Buttons A/Button A 2");
                            activeButton.SetActive(true);
                        }

                        if (numberOfButtons == 2)
                        {
                            activeButton = GameObject.Find("/Input/Buttons A/Button A 3");
                            activeButton.SetActive(true);
                        }

                        QTEGen = 0;
                        numberOfButtons++;
                    }

                    if (QTEGen == 2)
                    {
                        Debug.Log("Bouton B");

                        if (numberOfButtons == 0)
                        {
                            activeButton = GameObject.Find("/Input/Buttons B/Button B 1");
                            activeButton.SetActive(true);
                        }

                        if (numberOfButtons == 1)
                        {
                            activeButton = GameObject.Find("/Input/Buttons B/Button B 2");
                            activeButton.SetActive(true);
                        }

                        if (numberOfButtons == 2)
                        {
                            activeButton = GameObject.Find("/Input/Buttons B/Button B 3");
                            activeButton.SetActive(true);
                        }

                        QTEGen = 0;
                        numberOfButtons++;
                    }


                    if (QTEGen == 3)
                    {
                        Debug.Log("Bouton X");

                        if (numberOfButtons == 0)
                        {
                            activeButton = GameObject.Find("/Input/Buttons X/Button X 1");
                            activeButton.SetActive(true);
                        }

                        if (numberOfButtons == 1)
                        {
                            activeButton = GameObject.Find("/Input/Buttons X/Button X 2");
                            activeButton.SetActive(true);
                        }

                        if (numberOfButtons == 2)
                        {
                            activeButton = GameObject.Find("/Input/Buttons X/Button X 3");
                            activeButton.SetActive(true);
                        }

                        QTEGen = 0;
                        numberOfButtons++;
                    }

                    if (QTEGen == 4)
                    {
                        Debug.Log("Bouton Y");

                        if (numberOfButtons == 0)
                        {
                            activeButton = GameObject.Find("/Input/Buttons Y/Button Y 1");
                            activeButton.SetActive(true);
                        }

                        if (numberOfButtons == 1)
                        {
                            activeButton = GameObject.Find("/Input/Buttons Y/Button Y 2");
                            activeButton.SetActive(true);
                        }

                        if (numberOfButtons == 2)
                        {
                            activeButton = GameObject.Find("/Input/Buttons Y/Button Y 3");
                            activeButton.SetActive(true);
                        }

                        QTEGen = 0;
                        numberOfButtons++;
                    }
                }
              
                if (numberOfButtons == 3)
                {
                    switch (buttonNumber)
                    {

                        case 3:

                            animator.SetBool("SecondButton", false);
                            animator.SetBool("ThirdButton", true);

                            if (flag.position.y < GameObject.Find("/GameManager/Flag End").transform.position.y)
                            {
                                flag.Translate(Vector3.up * Time.deltaTime * 10);
                            }

                            gameDone = true;
                            break;

                        case 2:

                            animator.SetBool("FirstButton", false);
                            animator.SetBool("SecondButton", true);

                            if (flag.position.y < GameObject.Find("/GameManager/Flag Third").transform.position.y)
                            {
                                flag.Translate(Vector3.up * Time.deltaTime * 10);
                            }

                            if (GameObject.Find("/Input/Buttons A/Button A 3").activeSelf == true)
                            {
                                if (Input.GetButtonDown("A_Button"))
                                {
                                    Debug.Log("Success");
                                    sMngr.PlayGoodButton();
                                    GameObject.Find("/Input/Buttons A/Button A 3 A").SetActive(true);
                                    buttonNumber++;
                                    //drapeau.transform.Translate(0, 5, 0);
                                }

                                if (Input.GetButtonDown("B_Button") || Input.GetButtonDown("X_Button") || Input.GetButtonDown("Y_Button"))
                                {
                                    sMngr.PlayWrongButton();
                                    Debug.Log("Fail");
                                }
                            }

                            if (GameObject.Find("/Input/Buttons B/Button B 3").activeSelf == true)
                            {
                                if (Input.GetButtonDown("B_Button"))
                                {
                                    Debug.Log("Success");
                                    sMngr.PlayGoodButton();
                                    GameObject.Find("/Input/Buttons B/Button B 3 A").SetActive(true);
                                    buttonNumber++;
                                    //drapeau.transform.Translate(0, 5, 0);
                                }

                                if (Input.GetButtonDown("A_Button") || Input.GetButtonDown("X_Button") || Input.GetButtonDown("Y_Button"))
                                {
                                    sMngr.PlayWrongButton();
                                    Debug.Log("Fail");
                                }
                            }

                            if (GameObject.Find("/Input/Buttons X/Button X 3").activeSelf == true)
                            {
                                if (Input.GetButtonDown("X_Button"))
                                {
                                    Debug.Log("Success");
                                    sMngr.PlayGoodButton();
                                    GameObject.Find("/Input/Buttons X/Button X 3 A").SetActive(true);
                                    buttonNumber++;
                                    //drapeau.transform.Translate(0, 5, 0);
                                }

                                if (Input.GetButtonDown("A_Button") || Input.GetButtonDown("B_Button") || Input.GetButtonDown("Y_Button"))
                                {
                                    sMngr.PlayWrongButton();
                                    Debug.Log("Fail");
                                }
                            }

                            if (GameObject.Find("/Input/Buttons Y/Button Y 3").activeSelf == true)
                            {
                                if (Input.GetButtonDown("Y_Button"))
                                {
                                    Debug.Log("Success");
                                    sMngr.PlayGoodButton();
                                    GameObject.Find("/Input/Buttons Y/Button Y 3 A").SetActive(true);
                                    buttonNumber++;
                                    //drapeau.transform.Translate(0, 5, 0);
                                }

                                if (Input.GetButtonDown("A_Button") || Input.GetButtonDown("B_Button") || Input.GetButtonDown("X_Button"))
                                {
                                    sMngr.PlayWrongButton();
                                    Debug.Log("Fail");
                                }
                            }
                            break;

                        case 1:

                            animator.SetBool("FirstButton", true);

                            if (flag.position.y < GameObject.Find("/GameManager/Flag First").transform.position.y)
                            {
                                flag.Translate(Vector3.up * Time.deltaTime * 10);
                            }

                            if (GameObject.Find("/Input/Buttons A/Button A 2").activeSelf == true)
                            {
                                if (Input.GetButtonDown("A_Button"))
                                {
                                    Debug.Log("Success");
                                    sMngr.PlayGoodButton();
                                    GameObject.Find("/Input/Buttons A/Button A 2 A").SetActive(true);
                                    buttonNumber++;
                                    //drapeau.transform.Translate(0, 5, 0);
                                }

                                if (Input.GetButtonDown("B_Button") || Input.GetButtonDown("X_Button") || Input.GetButtonDown("Y_Button"))
                                {
                                    sMngr.PlayWrongButton();
                                    Debug.Log("Fail");
                                }
                            }

                            if (GameObject.Find("/Input/Buttons B/Button B 2").activeSelf == true)
                            {
                                if (Input.GetButtonDown("B_Button"))
                                {
                                    Debug.Log("Success");
                                    sMngr.PlayGoodButton();
                                    GameObject.Find("/Input/Buttons B/Button B 2 A").SetActive(true);
                                    buttonNumber++;
                                    //drapeau.transform.Translate(0, 5, 0);
                                }

                                if (Input.GetButtonDown("A_Button") || Input.GetButtonDown("X_Button") || Input.GetButtonDown("Y_Button"))
                                {
                                    sMngr.PlayWrongButton();
                                    Debug.Log("Fail");
                                }
                            }

                            if (GameObject.Find("/Input/Buttons X/Button X 2").activeSelf == true)
                            {
                                if (Input.GetButtonDown("X_Button"))
                                {
                                    Debug.Log("Success");
                                    sMngr.PlayGoodButton();
                                    GameObject.Find("/Input/Buttons X/Button X 2 A").SetActive(true);
                                    buttonNumber++;
                                    //drapeau.transform.Translate(0, 5, 0);
                                }

                                if (Input.GetButtonDown("A_Button") || Input.GetButtonDown("B_Button") || Input.GetButtonDown("Y_Button"))
                                {
                                    sMngr.PlayWrongButton();
                                    Debug.Log("Fail");
                                }
                            }

                            if (GameObject.Find("/Input/Buttons Y/Button Y 2").activeSelf == true)
                            {
                                if (Input.GetButtonDown("Y_Button"))
                                {
                                    Debug.Log("Success");
                                    sMngr.PlayGoodButton();
                                    GameObject.Find("/Input/Buttons Y/Button Y 2 A").SetActive(true);
                                    buttonNumber++;
                                    //drapeau.transform.Translate(0, 5, 0);
                                }

                                if (Input.GetButtonDown("A_Button") || Input.GetButtonDown("B_Button") || Input.GetButtonDown("X_Button"))
                                {
                                    sMngr.PlayWrongButton();
                                    Debug.Log("Fail");
                                }
                            }
                            break;

                        default:
                            if (GameObject.Find("/Input/Buttons A/Button A 1").activeSelf == true)
                            {
                                if (Input.GetButtonDown("A_Button"))
                                {
                                    Debug.Log("Success");
                                    sMngr.PlayGoodButton();
                                    GameObject.Find("/Input/Buttons A/Button A 1 A").SetActive(true);
                                    buttonNumber++;
                                    //drapeau.transform.Translate(0, 5, 0);
                                }

                                else if (Input.GetButtonDown("B_Button") || Input.GetButtonDown("X_Button") || Input.GetButtonDown("Y_Button"))
                                {
                                    Debug.Log("Fail");
                                    sMngr.PlayWrongButton();
                                }
                            }

                            if (GameObject.Find("/Input/Buttons B/Button B 1").activeSelf == true)
                            {
                                if (Input.GetButtonDown("B_Button"))
                                {
                                    Debug.Log("Success");
                                    sMngr.PlayGoodButton();
                                    GameObject.Find("/Input/Buttons B/Button B 1 A").SetActive(true);
                                    buttonNumber++;
                                    //drapeau.transform.Translate(0, 5, 0);
                                }

                                else if (Input.GetButtonDown("A_Button") || Input.GetButtonDown("X_Button") || Input.GetButtonDown("Y_Button"))
                                {
                                    Debug.Log("Fail");
                                    sMngr.PlayWrongButton();
                                }
                            }

                            if (GameObject.Find("/Input/Buttons X/Button X 1").activeSelf == true)
                            {
                                if (Input.GetButtonDown("X_Button"))
                                {
                                    Debug.Log("Success");
                                    sMngr.PlayGoodButton();
                                    GameObject.Find("/Input/Buttons X/Button X 1 A").SetActive(true);
                                    buttonNumber++;
                                    //drapeau.transform.Translate(0, 5, 0);
                                }

                                else if (Input.GetButtonDown("A_Button") || Input.GetButtonDown("B_Button") || Input.GetButtonDown("Y_Button"))
                                {
                                    sMngr.PlayWrongButton();
                                    Debug.Log("Fail");
                                }

                            }

                            if (GameObject.Find("/Input/Buttons Y/Button Y 1").activeSelf == true)
                            {
                                if (Input.GetButtonDown("Y_Button"))
                                {
                                    Debug.Log("Success");
                                    sMngr.PlayGoodButton();
                                    GameObject.Find("/Input/Buttons Y/Button Y 1 A").SetActive(true);
                                    buttonNumber++;
                                    //drapeau.transform.Translate(0, 5, 0);
                                }

                                else if (Input.GetButtonDown("A_Button") || Input.GetButtonDown("B_Button") || Input.GetButtonDown("X_Button"))
                                {
                                    sMngr.PlayWrongButton();
                                    Debug.Log("Fail");
                                }
                            }
                            break;
                    }
                }
               
                yield return null;
            }

            IEnumerator GameStartDiffMed()
            {
                if (numberOfButtons < 4)
                {
                    QTEGen = Random.Range(1, 5);

                    if (QTEGen == 1)
                    {
                        Debug.Log("Bouton A");

                        if (numberOfButtons == 0)
                        {
                            activeButton = GameObject.Find("/Input/Buttons A/Button A 1");
                            activeButton.SetActive(true);
                        }

                        if (numberOfButtons == 1)
                        {
                            activeButton = GameObject.Find("/Input/Buttons A/Button A 2");
                            activeButton.SetActive(true);
                        }

                        if (numberOfButtons == 2)
                        {
                            activeButton = GameObject.Find("/Input/Buttons A/Button A 3");
                            activeButton.SetActive(true);
                        }

                        if (numberOfButtons == 3)
                        {
                            activeButton = GameObject.Find("/Input/Buttons A/Button A 4");
                            activeButton.SetActive(true);
                        }

                        QTEGen = 0;
                        numberOfButtons++;
                    }

                    if (QTEGen == 2)
                    {
                        Debug.Log("Bouton B");

                        if (numberOfButtons == 0)
                        {
                            activeButton = GameObject.Find("/Input/Buttons B/Button B 1");
                            activeButton.SetActive(true);
                        }

                        if (numberOfButtons == 1)
                        {
                            activeButton = GameObject.Find("/Input/Buttons B/Button B 2");
                            activeButton.SetActive(true);
                        }

                        if (numberOfButtons == 2)
                        {
                            activeButton = GameObject.Find("/Input/Buttons B/Button B 3");
                            activeButton.SetActive(true);
                        }

                        if (numberOfButtons == 3)
                        {
                            activeButton = GameObject.Find("/Input/Buttons B/Button B 4");
                            activeButton.SetActive(true);
                        }

                        QTEGen = 0;
                        numberOfButtons++;
                    }


                    if (QTEGen == 3)
                    {
                        Debug.Log("Bouton X");

                        if (numberOfButtons == 0)
                        {
                            activeButton = GameObject.Find("/Input/Buttons X/Button X 1");
                            activeButton.SetActive(true);
                        }

                        if (numberOfButtons == 1)
                        {
                            activeButton = GameObject.Find("/Input/Buttons X/Button X 2");
                            activeButton.SetActive(true);
                        }

                        if (numberOfButtons == 2)
                        {
                            activeButton = GameObject.Find("/Input/Buttons X/Button X 3");
                            activeButton.SetActive(true);
                        }

                        if (numberOfButtons == 3)
                        {
                            activeButton = GameObject.Find("/Input/Buttons X/Button X 4");
                            activeButton.SetActive(true);
                        }

                        QTEGen = 0;
                        numberOfButtons++;
                    }

                    if (QTEGen == 4)
                    {
                        Debug.Log("Bouton Y");

                        if (numberOfButtons == 0)
                        {
                            activeButton = GameObject.Find("/Input/Buttons Y/Button Y 1");
                            activeButton.SetActive(true);
                        }

                        if (numberOfButtons == 1)
                        {
                            activeButton = GameObject.Find("/Input/Buttons Y/Button Y 2");
                            activeButton.SetActive(true);
                        }

                        if (numberOfButtons == 2)
                        {
                            activeButton = GameObject.Find("/Input/Buttons Y/Button Y 3");
                            activeButton.SetActive(true);
                        }

                        if (numberOfButtons == 3)
                        {
                            activeButton = GameObject.Find("/Input/Buttons Y/Button Y 4");
                            activeButton.SetActive(true);
                        }

                        QTEGen = 0;
                        numberOfButtons++;
                    }
                }
               
                if (numberOfButtons == 4)
                {
                    switch (buttonNumber)
                    {

                        case 4:

                            animator.SetBool("ThirdButton", false);
                            animator.SetBool("FirstButton", true);
                            if (flag.position.y < GameObject.Find("/GameManager/Flag End").transform.position.y)
                            {
                                flag.Translate(Vector3.up * Time.deltaTime * 10);
                            }

                            gameDone = true;
                            break;

                        case 3:

                            animator.SetBool("SecondButton", false);
                            animator.SetBool("ThirdButton", true);
                            if (flag.position.y < GameObject.Find("/GameManager/Flag Fourth").transform.position.y)
                            {
                                flag.Translate(Vector3.up * Time.deltaTime * 10);
                            }

                            if (GameObject.Find("/Input/Buttons A/Button A 4").activeSelf == true)
                            {
                                if (Input.GetButtonDown("A_Button"))
                                {
                                    Debug.Log("Success");
                                    sMngr.PlayGoodButton();
                                    GameObject.Find("/Input/Buttons A/Button A 4 A").SetActive(true);
                                    buttonNumber++;
                                    //drapeau.transform.Translate(0, 5, 0);
                                }

                                if (Input.GetButtonDown("B_Button") || Input.GetButtonDown("X_Button") || Input.GetButtonDown("Y_Button"))
                                {
                                    sMngr.PlayWrongButton();
                                    Debug.Log("Fail");
                                }
                            }

                            if (GameObject.Find("/Input/Buttons B/Button B 4").activeSelf == true)
                            {
                                if (Input.GetButtonDown("B_Button"))
                                {
                                    Debug.Log("Success");
                                    sMngr.PlayGoodButton();
                                    GameObject.Find("/Input/Buttons B/Button B 4 A").SetActive(true);
                                    buttonNumber++;
                                    //drapeau.transform.Translate(0, 5, 0);
                                }

                                if (Input.GetButtonDown("A_Button") || Input.GetButtonDown("X_Button") || Input.GetButtonDown("Y_Button"))
                                {
                                    sMngr.PlayWrongButton();
                                    Debug.Log("Fail");
                                }
                            }

                            if (GameObject.Find("/Input/Buttons X/Button X 4").activeSelf == true)
                            {
                                if (Input.GetButtonDown("X_Button"))
                                {
                                    Debug.Log("Success");
                                    sMngr.PlayGoodButton();
                                    GameObject.Find("/Input/Buttons X/Button X 4 A").SetActive(true);
                                    buttonNumber++;
                                    //drapeau.transform.Translate(0, 5, 0);
                                }

                                if (Input.GetButtonDown("A_Button") || Input.GetButtonDown("B_Button") || Input.GetButtonDown("Y_Button"))
                                {
                                    sMngr.PlayWrongButton();
                                    Debug.Log("Fail");
                                }
                            }

                            if (GameObject.Find("/Input/Buttons Y/Button Y 4").activeSelf == true)
                            {
                                if (Input.GetButtonDown("Y_Button"))
                                {
                                    Debug.Log("Success");
                                    sMngr.PlayGoodButton();
                                    GameObject.Find("/Input/Buttons Y/Button Y 4 A").SetActive(true);
                                    buttonNumber++;
                                    //drapeau.transform.Translate(0, 5, 0);
                                }

                                if (Input.GetButtonDown("A_Button") || Input.GetButtonDown("B_Button") || Input.GetButtonDown("X_Button"))
                                {
                                    sMngr.PlayWrongButton();
                                    Debug.Log("Fail");
                                }
                            }
                            break;

                        case 2:

                            animator.SetBool("FirstButton", false);
                            animator.SetBool("SecondButton", true);
                            if (flag.position.y < GameObject.Find("/GameManager/Flag Second").transform.position.y)
                            {
                                flag.Translate(Vector3.up * Time.deltaTime * 10);
                            }

                            if (GameObject.Find("/Input/Buttons A/Button A 3").activeSelf == true)
                            {
                                if (Input.GetButtonDown("A_Button"))
                                {
                                    Debug.Log("Success");
                                    sMngr.PlayGoodButton();
                                    GameObject.Find("/Input/Buttons A/Button A 3 A").SetActive(true);
                                    buttonNumber++;
                                    //drapeau.transform.Translate(0, 5, 0);
                                }

                                if (Input.GetButtonDown("B_Button") || Input.GetButtonDown("X_Button") || Input.GetButtonDown("Y_Button"))
                                {
                                    sMngr.PlayWrongButton();
                                    Debug.Log("Fail");
                                }
                            }

                            if (GameObject.Find("/Input/Buttons B/Button B 3").activeSelf == true)
                            {
                                if (Input.GetButtonDown("B_Button"))
                                {
                                    Debug.Log("Success");
                                    sMngr.PlayGoodButton();
                                    GameObject.Find("/Input/Buttons B/Button B 3 A").SetActive(true);
                                    buttonNumber++;
                                    //drapeau.transform.Translate(0, 5, 0);
                                }

                                if (Input.GetButtonDown("A_Button") || Input.GetButtonDown("X_Button") || Input.GetButtonDown("Y_Button"))
                                {
                                    sMngr.PlayWrongButton();
                                    Debug.Log("Fail");
                                }
                            }

                            if (GameObject.Find("/Input/Buttons X/Button X 3").activeSelf == true)
                            {
                                if (Input.GetButtonDown("X_Button"))
                                {
                                    Debug.Log("Success");
                                    sMngr.PlayGoodButton();
                                    GameObject.Find("/Input/Buttons X/Button X 3 A").SetActive(true);
                                    buttonNumber++;
                                    //drapeau.transform.Translate(0, 5, 0);
                                }

                                if (Input.GetButtonDown("A_Button") || Input.GetButtonDown("B_Button") || Input.GetButtonDown("Y_Button"))
                                {
                                    sMngr.PlayWrongButton();
                                    Debug.Log("Fail");
                                }
                            }

                            if (GameObject.Find("/Input/Buttons Y/Button Y 3").activeSelf == true)
                            {
                                if (Input.GetButtonDown("Y_Button"))
                                {
                                    Debug.Log("Success");
                                    sMngr.PlayGoodButton();
                                    GameObject.Find("/Input/Buttons Y/Button Y 3 A").SetActive(true);
                                    buttonNumber++;
                                    //drapeau.transform.Translate(0, 5, 0);
                                }

                                if (Input.GetButtonDown("A_Button") || Input.GetButtonDown("B_Button") || Input.GetButtonDown("X_Button"))
                                {
                                    sMngr.PlayWrongButton();
                                    Debug.Log("Fail");
                                }
                            }
                            break;

                        case 1:

                            animator.SetBool("FirstButton", true);
                            if (flag.position.y < GameObject.Find("/GameManager/Flag First").transform.position.y)
                            {
                                flag.Translate(Vector3.up * Time.deltaTime * 10);
                            }

                            if (GameObject.Find("/Input/Buttons A/Button A 2").activeSelf == true)
                            {
                                if (Input.GetButtonDown("A_Button"))
                                {
                                    Debug.Log("Success");
                                    sMngr.PlayGoodButton();
                                    GameObject.Find("/Input/Buttons A/Button A 2 A").SetActive(true);
                                    buttonNumber++;
                                    //drapeau.transform.Translate(0, 5, 0);
                                }

                                if (Input.GetButtonDown("B_Button") || Input.GetButtonDown("X_Button") || Input.GetButtonDown("Y_Button"))
                                {
                                    sMngr.PlayWrongButton();
                                    Debug.Log("Fail");
                                }
                            }

                            if (GameObject.Find("/Input/Buttons B/Button B 2").activeSelf == true)
                            {
                                if (Input.GetButtonDown("B_Button"))
                                {
                                    Debug.Log("Success");
                                    sMngr.PlayGoodButton();
                                    GameObject.Find("/Input/Buttons B/Button B 2 A").SetActive(true);
                                    buttonNumber++;
                                    //drapeau.transform.Translate(0, 5, 0);
                                }

                                if (Input.GetButtonDown("A_Button") || Input.GetButtonDown("X_Button") || Input.GetButtonDown("Y_Button"))
                                {
                                    sMngr.PlayWrongButton();
                                    Debug.Log("Fail");
                                }
                            }

                            if (GameObject.Find("/Input/Buttons X/Button X 2").activeSelf == true)
                            {
                                if (Input.GetButtonDown("X_Button"))
                                {
                                    Debug.Log("Success");
                                    sMngr.PlayGoodButton();
                                    GameObject.Find("/Input/Buttons X/Button X 2 A").SetActive(true);
                                    buttonNumber++;
                                    //drapeau.transform.Translate(0, 5, 0);
                                }

                                if (Input.GetButtonDown("A_Button") || Input.GetButtonDown("B_Button") || Input.GetButtonDown("Y_Button"))
                                {
                                    sMngr.PlayWrongButton();
                                    Debug.Log("Fail");
                                }
                            }

                            if (GameObject.Find("/Input/Buttons Y/Button Y 2").activeSelf == true)
                            {
                                if (Input.GetButtonDown("Y_Button"))
                                {
                                    Debug.Log("Success");
                                    sMngr.PlayGoodButton();
                                    GameObject.Find("/Input/Buttons Y/Button Y 2 A").SetActive(true);
                                    buttonNumber++;
                                    //drapeau.transform.Translate(0, 5, 0);
                                }

                                if (Input.GetButtonDown("A_Button") || Input.GetButtonDown("B_Button") || Input.GetButtonDown("X_Button"))
                                {
                                    sMngr.PlayWrongButton();
                                    Debug.Log("Fail");
                                }
                            }
                            break;

                        default:
                            if (GameObject.Find("/Input/Buttons A/Button A 1").activeSelf == true)
                            {
                                if (Input.GetButtonDown("A_Button"))
                                {
                                    Debug.Log("Success");
                                    sMngr.PlayGoodButton();
                                    GameObject.Find("/Input/Buttons A/Button A 1 A").SetActive(true);
                                    buttonNumber++;
                                    //drapeau.transform.Translate(0, 5, 0);
                                }

                                else if (Input.GetButtonDown("B_Button") || Input.GetButtonDown("X_Button") || Input.GetButtonDown("Y_Button"))
                                {
                                    Debug.Log("Fail");
                                    sMngr.PlayWrongButton();
                                }
                            }

                            if (GameObject.Find("/Input/Buttons B/Button B 1").activeSelf == true)
                            {
                                if (Input.GetButtonDown("B_Button"))
                                {
                                    Debug.Log("Success");
                                    sMngr.PlayGoodButton();
                                    GameObject.Find("/Input/Buttons B/Button B 1 A").SetActive(true);
                                    buttonNumber++;
                                    //drapeau.transform.Translate(0, 5, 0);
                                }

                                else if (Input.GetButtonDown("A_Button") || Input.GetButtonDown("X_Button") || Input.GetButtonDown("Y_Button"))
                                {
                                    Debug.Log("Fail");
                                    sMngr.PlayWrongButton();
                                }
                            }

                            if (GameObject.Find("/Input/Buttons X/Button X 1").activeSelf == true)
                            {
                                if (Input.GetButtonDown("X_Button"))
                                {
                                    Debug.Log("Success");
                                    sMngr.PlayGoodButton();
                                    GameObject.Find("/Input/Buttons X/Button X 1 A").SetActive(true);
                                    buttonNumber++;
                                    //drapeau.transform.Translate(0, 5, 0);
                                }

                                else if (Input.GetButtonDown("A_Button") || Input.GetButtonDown("B_Button") || Input.GetButtonDown("Y_Button"))
                                {
                                    sMngr.PlayWrongButton();
                                    Debug.Log("Fail");
                                }

                            }

                            if (GameObject.Find("/Input/Buttons Y/Button Y 1").activeSelf == true)
                            {
                                if (Input.GetButtonDown("Y_Button"))
                                {
                                    Debug.Log("Success");
                                    sMngr.PlayGoodButton();
                                    GameObject.Find("/Input/Buttons Y/Button Y 1 A").SetActive(true);
                                    buttonNumber++;
                                    //drapeau.transform.Translate(0, 5, 0);
                                }

                                else if (Input.GetButtonDown("A_Button") || Input.GetButtonDown("B_Button") || Input.GetButtonDown("X_Button"))
                                {
                                    sMngr.PlayWrongButton();
                                    Debug.Log("Fail");
                                }
                            }
                            break;
                    }
                }

                yield return null;
            }

            IEnumerator GameStartDiffHard()
            {
                if (numberOfButtons < 5)
                {
                    QTEGen = Random.Range(1, 5);

                    if (QTEGen == 1)
                    {
                        Debug.Log("Bouton A");

                        if (numberOfButtons == 0)
                        {
                            activeButton = GameObject.Find("/Input/Buttons A/Button A 1");
                            activeButton.SetActive(true);
                        }

                        if (numberOfButtons == 1)
                        {
                            activeButton = GameObject.Find("/Input/Buttons A/Button A 2");
                            activeButton.SetActive(true);
                        }

                        if (numberOfButtons == 2)
                        {
                            activeButton = GameObject.Find("/Input/Buttons A/Button A 3");
                            activeButton.SetActive(true);
                        }

                        if (numberOfButtons == 3)
                        {
                            activeButton = GameObject.Find("/Input/Buttons A/Button A 4");
                            activeButton.SetActive(true);
                        }

                        if (numberOfButtons == 4)
                        {
                            activeButton = GameObject.Find("/Input/Buttons A/Button A 5");
                            activeButton.SetActive(true);
                        }

                        QTEGen = 0;
                        numberOfButtons++;
                    }

                    if (QTEGen == 2)
                    {
                        Debug.Log("Bouton B");

                        if (numberOfButtons == 0)
                        {
                            activeButton = GameObject.Find("/Input/Buttons B/Button B 1");
                            activeButton.SetActive(true);
                        }

                        if (numberOfButtons == 1)
                        {
                            activeButton = GameObject.Find("/Input/Buttons B/Button B 2");
                            activeButton.SetActive(true);
                        }

                        if (numberOfButtons == 2)
                        {
                            activeButton = GameObject.Find("/Input/Buttons B/Button B 3");
                            activeButton.SetActive(true);
                        }

                        if (numberOfButtons == 3)
                        {
                            activeButton = GameObject.Find("/Input/Buttons B/Button B 4");
                            activeButton.SetActive(true);
                        }

                        if (numberOfButtons == 4)
                        {
                            activeButton = GameObject.Find("/Input/Buttons B/Button B 5");
                            activeButton.SetActive(true);
                        }

                        QTEGen = 0;
                        numberOfButtons++;
                    }


                    if (QTEGen == 3)
                    {
                        Debug.Log("Bouton X");

                        if (numberOfButtons == 0)
                        {
                            activeButton = GameObject.Find("/Input/Buttons X/Button X 1");
                            activeButton.SetActive(true);
                        }

                        if (numberOfButtons == 1)
                        {
                            activeButton = GameObject.Find("/Input/Buttons X/Button X 2");
                            activeButton.SetActive(true);
                        }

                        if (numberOfButtons == 2)
                        {
                            activeButton = GameObject.Find("/Input/Buttons X/Button X 3");
                            activeButton.SetActive(true);
                        }

                        if (numberOfButtons == 3)
                        {
                            activeButton = GameObject.Find("/Input/Buttons X/Button X 4");
                            activeButton.SetActive(true);
                        }

                        if (numberOfButtons == 4)
                        {
                            activeButton = GameObject.Find("/Input/Buttons X/Button X 5");
                            activeButton.SetActive(true);
                        }

                        QTEGen = 0;
                        numberOfButtons++;
                    }

                    if (QTEGen == 4)
                    {
                        Debug.Log("Bouton Y");

                        if (numberOfButtons == 0)
                        {
                            activeButton = GameObject.Find("/Input/Buttons Y/Button Y 1");
                            activeButton.SetActive(true);
                        }

                        if (numberOfButtons == 1)
                        {
                            activeButton = GameObject.Find("/Input/Buttons Y/Button Y 2");
                            activeButton.SetActive(true);
                        }

                        if (numberOfButtons == 2)
                        {
                            activeButton = GameObject.Find("/Input/Buttons Y/Button Y 3");
                            activeButton.SetActive(true);
                        }

                        if (numberOfButtons == 3)
                        {
                            activeButton = GameObject.Find("/Input/Buttons Y/Button Y 4");
                            activeButton.SetActive(true);
                        }

                        if (numberOfButtons == 4)
                        {
                            activeButton = GameObject.Find("/Input/Buttons Y/Button Y 5");
                            activeButton.SetActive(true);
                        }

                        QTEGen = 0;
                        numberOfButtons++;
                    }
                }

                
                if (numberOfButtons == 5)
                {
                    switch (buttonNumber)
                    {

                        case 5:

                            animator.SetBool("FirstButton", false);
                            animator.SetBool("SecondButton", true);

                            if (flag.position.y < GameObject.Find("/GameManager/Flag End").transform.position.y)
                            {
                                flag.Translate(Vector3.up * Time.deltaTime * 10);
                            }

                            gameDone = true;
                            break;

                        case 4:

                            animator.SetBool("ThirdButton", false);
                            animator.SetBool("FirstButton", true);

                            if (flag.position.y < GameObject.Find("/GameManager/Flag Fourth").transform.position.y)
                            {
                                flag.Translate(Vector3.up * Time.deltaTime * 10);
                            }

                            if (GameObject.Find("/Input/Buttons A/Button A 5").activeSelf == true)
                            {
                                if (Input.GetButtonDown("A_Button"))
                                {
                                    Debug.Log("Success");
                                    sMngr.PlayGoodButton();
                                    GameObject.Find("/Input/Buttons A/Button A 5 A").SetActive(true);
                                    buttonNumber++;
                                    //drapeau.transform.Translate(0, 5, 0);
                                }

                                if (Input.GetButtonDown("B_Button") || Input.GetButtonDown("X_Button") || Input.GetButtonDown("Y_Button"))
                                {
                                    sMngr.PlayWrongButton();
                                    Debug.Log("Fail");
                                }
                            }

                            if (GameObject.Find("/Input/Buttons B/Button B 5").activeSelf == true)
                            {
                                if (Input.GetButtonDown("B_Button"))
                                {
                                    Debug.Log("Success");
                                    sMngr.PlayGoodButton();
                                    GameObject.Find("/Input/Buttons B/Button B 5 A").SetActive(true);
                                    buttonNumber++;
                                    //drapeau.transform.Translate(0, 5, 0);
                                }

                                if (Input.GetButtonDown("A_Button") || Input.GetButtonDown("X_Button") || Input.GetButtonDown("Y_Button"))
                                {
                                    sMngr.PlayWrongButton();
                                    Debug.Log("Fail");
                                }
                            }

                            if (GameObject.Find("/Input/Buttons X/Button X 5").activeSelf == true)
                            {
                                if (Input.GetButtonDown("X_Button"))
                                {
                                    Debug.Log("Success");
                                    sMngr.PlayGoodButton();
                                    GameObject.Find("/Input/Buttons X/Button X 5 A").SetActive(true);
                                    buttonNumber++;
                                    //drapeau.transform.Translate(0, 5, 0);
                                }

                                if (Input.GetButtonDown("A_Button") || Input.GetButtonDown("B_Button") || Input.GetButtonDown("Y_Button"))
                                {
                                    sMngr.PlayWrongButton();
                                    Debug.Log("Fail");
                                }
                            }

                            if (GameObject.Find("/Input/Buttons Y/Button Y 5").activeSelf == true)
                            {
                                if (Input.GetButtonDown("Y_Button"))
                                {
                                    Debug.Log("Success");
                                    sMngr.PlayGoodButton();
                                    GameObject.Find("/Input/Buttons Y/Button Y 5 A").SetActive(true);
                                    buttonNumber++;
                                    //drapeau.transform.Translate(0, 5, 0);
                                }

                                if (Input.GetButtonDown("A_Button") || Input.GetButtonDown("B_Button") || Input.GetButtonDown("X_Button"))
                                {
                                    sMngr.PlayWrongButton();
                                    Debug.Log("Fail");
                                }
                            }
                            break;

                        case 3:

                            animator.SetBool("SecondButton", false);
                            animator.SetBool("ThirdButton", true);

                            if (flag.position.y < GameObject.Find("/GameManager/Flag Third").transform.position.y)
                            {
                                flag.Translate(Vector3.up * Time.deltaTime * 10);
                            }

                            if (GameObject.Find("/Input/Buttons A/Button A 4").activeSelf == true)
                            {
                                if (Input.GetButtonDown("A_Button"))
                                {
                                    Debug.Log("Success");
                                    sMngr.PlayGoodButton();
                                    GameObject.Find("/Input/Buttons A/Button A 4 A").SetActive(true);
                                    buttonNumber++;
                                    //drapeau.transform.Translate(0, 5, 0);
                                }

                                if (Input.GetButtonDown("B_Button") || Input.GetButtonDown("X_Button") || Input.GetButtonDown("Y_Button"))
                                {
                                    sMngr.PlayWrongButton();
                                    Debug.Log("Fail");
                                }
                            }

                            if (GameObject.Find("/Input/Buttons B/Button B 4").activeSelf == true)
                            {
                                if (Input.GetButtonDown("B_Button"))
                                {
                                    Debug.Log("Success");
                                    sMngr.PlayGoodButton();
                                    GameObject.Find("/Input/Buttons B/Button B 4 A").SetActive(true);
                                    buttonNumber++;
                                    //drapeau.transform.Translate(0, 5, 0);
                                }

                                if (Input.GetButtonDown("A_Button") || Input.GetButtonDown("X_Button") || Input.GetButtonDown("Y_Button"))
                                {
                                    sMngr.PlayWrongButton();
                                    Debug.Log("Fail");
                                }
                            }

                            if (GameObject.Find("/Input/Buttons X/Button X 4").activeSelf == true)
                            {
                                if (Input.GetButtonDown("X_Button"))
                                {
                                    Debug.Log("Success");
                                    sMngr.PlayGoodButton();
                                    GameObject.Find("/Input/Buttons X/Button X 4 A").SetActive(true);
                                    buttonNumber++;
                                    //drapeau.transform.Translate(0, 5, 0);
                                }

                                if (Input.GetButtonDown("A_Button") || Input.GetButtonDown("B_Button") || Input.GetButtonDown("Y_Button"))
                                {
                                    sMngr.PlayWrongButton();
                                    Debug.Log("Fail");
                                }
                            }

                            if (GameObject.Find("/Input/Buttons Y/Button Y 4").activeSelf == true)
                            {
                                if (Input.GetButtonDown("Y_Button"))
                                {
                                    Debug.Log("Success");
                                    sMngr.PlayGoodButton();
                                    GameObject.Find("/Input/Buttons Y/Button Y 4 A").SetActive(true);
                                    buttonNumber++;
                                    //drapeau.transform.Translate(0, 5, 0);
                                }

                                if (Input.GetButtonDown("A_Button") || Input.GetButtonDown("B_Button") || Input.GetButtonDown("X_Button"))
                                {
                                    sMngr.PlayWrongButton();
                                    Debug.Log("Fail");
                                }
                            }
                            break;

                        case 2:

                            animator.SetBool("FirstButton", false);
                            animator.SetBool("SecondButton", true);

                            if (flag.position.y < GameObject.Find("/GameManager/Flag Second").transform.position.y)
                            {
                                flag.Translate(Vector3.up * Time.deltaTime * 10);
                            }

                            if (GameObject.Find("/Input/Buttons A/Button A 3").activeSelf == true)
                            {
                                if (Input.GetButtonDown("A_Button"))
                                {
                                    Debug.Log("Success");
                                    sMngr.PlayGoodButton();
                                    GameObject.Find("/Input/Buttons A/Button A 3 A").SetActive(true);
                                    buttonNumber++;
                                    //drapeau.transform.Translate(0, 5, 0);
                                }

                                if (Input.GetButtonDown("B_Button") || Input.GetButtonDown("X_Button") || Input.GetButtonDown("Y_Button"))
                                {
                                    sMngr.PlayWrongButton();
                                    Debug.Log("Fail");
                                }
                            }

                            if (GameObject.Find("/Input/Buttons B/Button B 3").activeSelf == true)
                            {
                                if (Input.GetButtonDown("B_Button"))
                                {
                                    Debug.Log("Success");
                                    sMngr.PlayGoodButton();
                                    GameObject.Find("/Input/Buttons B/Button B 3 A").SetActive(true);
                                    buttonNumber++;
                                    //drapeau.transform.Translate(0, 5, 0);
                                }

                                if (Input.GetButtonDown("A_Button") || Input.GetButtonDown("X_Button") || Input.GetButtonDown("Y_Button"))
                                {
                                    sMngr.PlayWrongButton();
                                    Debug.Log("Fail");
                                }
                            }

                            if (GameObject.Find("/Input/Buttons X/Button X 3").activeSelf == true)
                            {
                                if (Input.GetButtonDown("X_Button"))
                                {
                                    Debug.Log("Success");
                                    sMngr.PlayGoodButton();
                                    GameObject.Find("/Input/Buttons X/Button X 3 A").SetActive(true);
                                    buttonNumber++;
                                    //drapeau.transform.Translate(0, 5, 0);
                                }

                                if (Input.GetButtonDown("A_Button") || Input.GetButtonDown("B_Button") || Input.GetButtonDown("Y_Button"))
                                {
                                    sMngr.PlayWrongButton();
                                    Debug.Log("Fail");
                                }
                            }

                            if (GameObject.Find("/Input/Buttons Y/Button Y 3").activeSelf == true)
                            {
                                if (Input.GetButtonDown("Y_Button"))
                                {
                                    Debug.Log("Success");
                                    sMngr.PlayGoodButton();
                                    GameObject.Find("/Input/Buttons Y/Button Y 3 A").SetActive(true);
                                    buttonNumber++;
                                    //drapeau.transform.Translate(0, 5, 0);
                                }

                                if (Input.GetButtonDown("A_Button") || Input.GetButtonDown("B_Button") || Input.GetButtonDown("X_Button"))
                                {
                                    sMngr.PlayWrongButton();
                                    Debug.Log("Fail");
                                }
                            }
                            break;

                        case 1:

                            animator.SetBool("FirstButton", true);

                            if (flag.position.y < GameObject.Find("/GameManager/Flag First").transform.position.y)
                            {
                                flag.Translate(Vector3.up * Time.deltaTime * 10);
                            }

                            if (GameObject.Find("/Input/Buttons A/Button A 2").activeSelf == true)
                            {
                                if (Input.GetButtonDown("A_Button"))
                                {
                                    Debug.Log("Success");
                                    sMngr.PlayGoodButton();
                                    GameObject.Find("/Input/Buttons A/Button A 2 A").SetActive(true);
                                    buttonNumber++;
                                    //drapeau.transform.Translate(0, 5, 0);
                                }

                                if (Input.GetButtonDown("B_Button") || Input.GetButtonDown("X_Button") || Input.GetButtonDown("Y_Button"))
                                {
                                    sMngr.PlayWrongButton();
                                    Debug.Log("Fail");
                                }
                            }

                            if (GameObject.Find("/Input/Buttons B/Button B 2").activeSelf == true)
                            {
                                if (Input.GetButtonDown("B_Button"))
                                {
                                    Debug.Log("Success");
                                    sMngr.PlayGoodButton();
                                    GameObject.Find("/Input/Buttons B/Button B 2 A").SetActive(true);
                                    buttonNumber++;
                                    //drapeau.transform.Translate(0, 5, 0);
                                }

                                if (Input.GetButtonDown("A_Button") || Input.GetButtonDown("X_Button") || Input.GetButtonDown("Y_Button"))
                                {
                                    sMngr.PlayWrongButton();
                                    Debug.Log("Fail");
                                }
                            }

                            if (GameObject.Find("/Input/Buttons X/Button X 2").activeSelf == true)
                            {
                                if (Input.GetButtonDown("X_Button"))
                                {
                                    Debug.Log("Success");
                                    sMngr.PlayGoodButton();
                                    GameObject.Find("/Input/Buttons X/Button X 2 A").SetActive(true);
                                    buttonNumber++;
                                    //drapeau.transform.Translate(0, 5, 0);
                                }

                                if (Input.GetButtonDown("A_Button") || Input.GetButtonDown("B_Button") || Input.GetButtonDown("Y_Button"))
                                {
                                    sMngr.PlayWrongButton();
                                    Debug.Log("Fail");
                                }
                            }

                            if (GameObject.Find("/Input/Buttons Y/Button Y 2").activeSelf == true)
                            {
                                if (Input.GetButtonDown("Y_Button"))
                                {
                                    Debug.Log("Success");
                                    sMngr.PlayGoodButton();
                                    GameObject.Find("/Input/Buttons Y/Button Y 2 A").SetActive(true);
                                    buttonNumber++;
                                    //drapeau.transform.Translate(0, 5, 0);
                                }

                                if (Input.GetButtonDown("A_Button") || Input.GetButtonDown("B_Button") || Input.GetButtonDown("X_Button"))
                                {
                                    sMngr.PlayWrongButton();
                                    Debug.Log("Fail");
                                }
                            }
                            break;

                        default:
                            if (GameObject.Find("/Input/Buttons A/Button A 1").activeSelf == true)
                            {
                                if (Input.GetButtonDown("A_Button"))
                                {
                                    Debug.Log("Success");
                                    sMngr.PlayGoodButton();
                                    GameObject.Find("/Input/Buttons A/Button A 1 A").SetActive(true);
                                    buttonNumber++;
                                    //drapeau.transform.Translate(0, 5, 0);
                                }

                                else if (Input.GetButtonDown("B_Button") || Input.GetButtonDown("X_Button") || Input.GetButtonDown("Y_Button"))
                                {
                                    Debug.Log("Fail");
                                    sMngr.PlayWrongButton();
                                }
                            }

                            if (GameObject.Find("/Input/Buttons B/Button B 1").activeSelf == true)
                            {
                                if (Input.GetButtonDown("B_Button"))
                                {
                                    Debug.Log("Success");
                                    sMngr.PlayGoodButton();
                                    GameObject.Find("/Input/Buttons B/Button B 1 A").SetActive(true);
                                    buttonNumber++;
                                    //drapeau.transform.Translate(0, 5, 0);
                                }

                                else if (Input.GetButtonDown("A_Button") || Input.GetButtonDown("X_Button") || Input.GetButtonDown("Y_Button"))
                                {
                                    Debug.Log("Fail");
                                    sMngr.PlayWrongButton();
                                }
                            }

                            if (GameObject.Find("/Input/Buttons X/Button X 1").activeSelf == true)
                            {
                                if (Input.GetButtonDown("X_Button"))
                                {
                                    Debug.Log("Success");
                                    sMngr.PlayGoodButton();
                                    GameObject.Find("/Input/Buttons X/Button X 1 A").SetActive(true);
                                    buttonNumber++;
                                    //drapeau.transform.Translate(0, 5, 0);
                                }

                                else if (Input.GetButtonDown("A_Button") || Input.GetButtonDown("B_Button") || Input.GetButtonDown("Y_Button"))
                                {
                                    sMngr.PlayWrongButton();
                                    Debug.Log("Fail");
                                }

                            }

                            if (GameObject.Find("/Input/Buttons Y/Button Y 1").activeSelf == true)
                            {
                                if (Input.GetButtonDown("Y_Button"))
                                {
                                    Debug.Log("Success");
                                    sMngr.PlayGoodButton();
                                    GameObject.Find("/Input/Buttons Y/Button Y 1 A").SetActive(true);
                                    buttonNumber++;
                                    //drapeau.transform.Translate(0, 5, 0);
                                }

                                else if (Input.GetButtonDown("A_Button") || Input.GetButtonDown("B_Button") || Input.GetButtonDown("X_Button"))
                                {
                                    sMngr.PlayWrongButton();
                                    Debug.Log("Fail");
                                }
                            }
                            break;
                    }
                }

                yield return null;
            }
        }
    }
}

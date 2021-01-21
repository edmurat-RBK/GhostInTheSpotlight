using System.Collections.Generic;
using Testing;
using UnityEngine;

namespace SpanishInquisition
{

    namespace HisserLeDrapeau
    {

        /// <summary>
        /// Adel Ahmed-Yahia
        /// </summary>
        
        public class NewGameManager : TimedBehaviour
        {
            private static NewGameManager _instance;
            public static NewGameManager instance
            {
                get
                {
                    if (_instance == null)
                    {
                        _instance = FindObjectOfType<NewGameManager>();
                    }
                    return _instance;
                }
            }

            public GameObject[] buttons;
            public List<ButtonMovement> activeButtons = new List<ButtonMovement>();
            public GameObject spawner;
            public GameObject flag;
            public GameObject flagToEnd;
            public GameObject victoryText;
            public GameObject defeatText;
            public GameObject victoryFeedback;
            public GameObject defeatFeedback;
            public Animator animator;
            public int objectiveNumber;
            public bool gameIsWon;
            public Transform target;
            public float radius;
            public float speed;
            public float flagStep;
            public Vector3 baseFlagPosition;
            public Vector3 targetFlagPosition;
            public ParticleSystem feedbackParticle;
            [HideInInspector] public int score;

            private SoundManager soundManager;

            public override void Start()
            {
                base.Start(); //Do not erase this line!

                //currentDifficulty = Difficulty.EASY;
                feedbackParticle.GetComponent<ParticleSystem>();
                speed = bpm / 5;
                score = 0;
                soundManager = GetComponentInChildren<SoundManager>();
                baseFlagPosition = flag.transform.position;
                targetFlagPosition = baseFlagPosition;

                switch (bpm)                   
                {
                    case 60:
                        soundManager.PlayFlagMusicSlow();
                        break;

                    case 90:
                        soundManager.PlayFlagMusicMedium();
                        break;

                    case 120:
                        soundManager.PlayFlagMusicFast();
                        break;

                    case 140:
                        soundManager.PlayFlagMusicSuperFast();
                        break;
                }
                
                switch (currentDifficulty)
                {
                    case Difficulty.EASY:
                        objectiveNumber = 3;
                        FlagMove();
                        break;

                    case Difficulty.MEDIUM:
                        objectiveNumber = 4;
                        FlagMove();
                        break;

                    case Difficulty.HARD:
                        objectiveNumber = 5;
                        FlagMove();
                        break;
                }
            }

            //FixedUpdate is called on a fixed time.
            public override void FixedUpdate()
            {
                base.FixedUpdate(); //Do not erase this line!

                
            }

            public void Update()
            {
                flag.transform.position = Vector3.Lerp(flag.transform.position, targetFlagPosition, Time.deltaTime * 5f);

                InputFailSuccessConditions();

                if (score >= objectiveNumber && !gameIsWon)
                {
                    gameIsWon = true;
                    soundManager.PlayVictory();
                    victoryText.SetActive(true);
                    victoryFeedback.SetActive(true);
                }

                switch (score)
                {
                    case 5:
                        animator.SetBool("FirstButton", false);
                        animator.SetBool("SecondButton", true);
                        break;

                    case 4:
                        animator.SetBool("ThirdButton", false);
                        animator.SetBool("FirstButton", true);
                        break;

                    case 3:
                        animator.SetBool("SecondButton", false);
                        animator.SetBool("ThirdButton", true);
                        break;

                    case 2:
                        animator.SetBool("FirstButton", false);
                        animator.SetBool("SecondButton", true);
                        break;

                    case 1:
                        animator.SetBool("FirstButton", true);
                        break;

                    default:
                        break;
                }
            }

            //TimedUpdate is called once every tick.
            public override void TimedUpdate()
            {
                base.TimedUpdate();

                if (Tick < 8 && !gameIsWon)
                {
                    Spawner();
                }

                if (Tick == 8)
                {
                    Manager.Instance.Result (gameIsWon);
                }

                if (Tick == 8 && !gameIsWon)
                {
                    soundManager.PlayDefeat();
                    defeatText.SetActive(true);
                    defeatFeedback.SetActive(true);
                }
            }

            private void Spawner()
            {

                int buttonNumber = Random.Range(0, 4);

                //create new clone
                GameObject newButtonInstance = GameObject.Instantiate(buttons[buttonNumber], spawner.transform.position, Quaternion.identity);

                //activate the gameobject (because the templates are inactive in the scene, so it makes the clone inactive when instantiated)
                newButtonInstance.SetActive(true);

                //add the script to a list of all the button script existing
                activeButtons.Add(newButtonInstance.GetComponent<ButtonMovement>());

                
                soundManager.PlayButtonApparition();
            }

            private void FlagMove()
            {
                flagStep = ((flagToEnd.transform.position - flag.transform.position).magnitude) / objectiveNumber;
            }

            private void InputFailSuccessConditions()
            {
                // Iterate over each buttons
                foreach (ButtonMovement btnMovement in activeButtons)
                {
                    if (btnMovement.InZone())
                    {
                        //Debug.Log("button input= " + btnMovement.type.ToString());
                        //Debug.Log("input= " + System.Enum.GetName(typeof(ButtonsType), btnMovement.type));



                        //
                        // si le bon bouton est appuyé : réussite
                        /*if ((Input.GetButtonDown("A_Button") && btnMovement.type == ButtonsType.A)
                        || (Input.GetButtonDown("B_Button") && btnMovement.type == ButtonsType.B)
                        || (Input.GetButtonDown("X_Button") && btnMovement.type == ButtonsType.X)
                        || (Input.GetButtonDown("Y_Button") && btnMovement.type == ButtonsType.Y))
                        {*/
                        string buttonString = btnMovement.type.ToString();
                        if (Input.GetButtonDown(buttonString + "_Button")) 
                        {
                            ButtonSuccess(btnMovement);
                        } else
                        {
                            if ((Input.GetButtonDown("A_Button") && btnMovement.type != ButtonsType.A)
                                || (Input.GetButtonDown("B_Button") && btnMovement.type != ButtonsType.B)
                                || (Input.GetButtonDown("X_Button") && btnMovement.type != ButtonsType.X)
                                || (Input.GetButtonDown("Y_Button") && btnMovement.type != ButtonsType.Y))
                            {
                                ButtonFail();
                            }
                        }

                        return;
                    }
                }


                //No buttons are found in the zone
                // si un bouton est appuyé : échec
                if (Input.GetButtonDown("A_Button") || Input.GetButtonDown("B_Button")|| Input.GetButtonDown("X_Button")|| Input.GetButtonDown("Y_Button")) 
                {
                    ButtonFail();
                }
            }


            public void ButtonSuccess(ButtonMovement btnMovement)
            {

                if (score < objectiveNumber)
                {
                    score++;

                    targetFlagPosition = baseFlagPosition + ((Vector3.up * flagStep) * score);

                    feedbackParticle.Play();
                    soundManager.PlayGoodButton();

                    activeButtons.Remove(btnMovement);
                    Destroy(btnMovement.gameObject);
                }
            }

            public void ButtonFail()
            {
                if (score >= 0 && !gameIsWon)
                {
                    score--;

                    targetFlagPosition = baseFlagPosition + ((Vector3.up * flagStep) * score);
                    soundManager.PlayWrongButton();
                }
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Testing;

namespace TrioTrapioWare

{
    namespace Swipe
    {
        /// <summary>
        /// Roman Detue
        /// </summary>
        public class Initialisation : TimedBehaviour
        {

            public GameObject[] tinderProfile;
            public GameObject[] tinderPaper;
            public GameObject[] tinderPaperDirty;
            public GameObject breakScreen;
            public GameObject wanted;
            public GameObject paper;

            public GameObject auraMatch;
            public GameObject auraPersonnage;
            public GameObject personnageRondVert;
            public GameObject match;
            public GameObject ecranNoir;
            public GameObject[] greenCircle;
            public GameObject epeeDroit;
            public GameObject epeeGauche;

            public GameObject personnageRondRouge;
            public GameObject auraPersonnageDefaite;
            public GameObject rate;
            public GameObject ecranDefaite;

            //UI

            public GameObject rondB;
            public GameObject rondX;
            public GameObject rondBAppuye;
            public GameObject rondXAppuye;
            public GameObject auraVerteUI;
            public GameObject auraRougeUI;
            public GameObject AuraVertProfil;
            public GameObject AuraRougeProfil;

            public AudioSource source;

            public AudioClip swipeSound;
            public AudioClip winSound;
            public AudioClip defeatSound;
            public AudioClip paperSound;
            public AudioClip angelSound;
            public AudioClip clock;
            public AudioClip clockFast;

            public bool[] goodProfile;
            public bool canPlay;
            public bool gameEnd;

            public bool win;
            public bool defeat;

            public bool angelFlag;

            public int goodProfileNumber;
            public int profilAnalizing = 0;
            public int tickSwipe = 0;
            Vector3 RotationSwipe = new Vector3(0f, 0f, 28f);
            Vector3 RotationEpees = new Vector3(0f, 0f, 25f);


            public override void Start()
            {
                base.Start(); //Do not erase this line!

                source = GetComponent<AudioSource>();
                source.volume = 0.5f;
                source.PlayOneShot(paperSound);
                RandomSorting();
                ecranNoir.SetActive(false);

                switch (currentDifficulty)
                {
                    case Difficulty.EASY:
                        ProfilSelectionEasy();
                        tinderPaper[goodProfileNumber].SetActive(true);
                        breakScreen.SetActive(false);
                        break;
                    case Difficulty.MEDIUM:
                        ProfilSelectionMedium();
                        tinderPaper[goodProfileNumber].SetActive(true);
                        breakScreen.SetActive(true);
                        break;
                    case Difficulty.HARD:
                        ProfilSelectionHard();
                        tinderPaperDirty[goodProfileNumber].SetActive(true);
                        breakScreen.SetActive(true);
                        break;
                }
            }

            //FixedUpdate is called on a fixed time.
            public override void FixedUpdate()
            {
                base.FixedUpdate(); //Do not erase this line!
            }

            //TimedUpdate is called once every tick.
            public override void TimedUpdate()
            {
                tickSwipe++;
                if (Tick == 8 && canPlay == true || Tick == 8 && defeat == true)
                {
                    canPlay = false;
                    Manager.Instance.Result(false);
                }
                if (Tick == 8 && win == true)
                {
                    canPlay = false;
                    Manager.Instance.Result(true);
                }
            }

            void Update()
            {
                PaperInit();
                AuraRotation();
                AuraFacile();

                if (Input.GetButtonDown("B_Button") && canPlay)
                {
                    PressionBAnnim();
                    SwipeDroite();

                    if (goodProfile[profilAnalizing] == true)
                    {
                        AnimVictoire();
                    }
                    if (goodProfile[profilAnalizing] == false)
                    {
                        AnimDefaite();
                    }
                }
                if (Input.GetButtonDown("X_Button") && canPlay)
                {
                    SwipeGauche();
                    PressionXAnnim();

                    if (goodProfile[profilAnalizing] == true)
                    {
                        AnimDefaite();
                    }
                    if (goodProfile[profilAnalizing] == false)
                    {
                        profilAnalizing++;
                    }
                }
                if (Input.GetButtonUp("B_Button"))
                {
                    RelacherBAnnim();
                }
                if (Input.GetButtonUp("X_Button"))
                {
                    RelacherXAnnim();
                }
            }
            public void RandomSorting()
            {
                tinderProfile = GameObject.FindGameObjectsWithTag("Button1");
                tinderPaper = GameObject.FindGameObjectsWithTag("Button2");
                tinderPaperDirty = GameObject.FindGameObjectsWithTag("Wall");
                greenCircle = GameObject.FindGameObjectsWithTag("Finish");
                for (int positionOfArray = 0; positionOfArray < tinderProfile.Length; positionOfArray++)
                {
                    GameObject obj = tinderProfile[positionOfArray];
                    GameObject obj2 = tinderPaper[positionOfArray];
                    GameObject obj3 = tinderPaperDirty[positionOfArray];
                    GameObject obj4 = greenCircle[positionOfArray];
                    int randomizeArray = Random.Range(0, positionOfArray);
                    tinderProfile[positionOfArray] = tinderProfile[randomizeArray];
                    tinderPaper[positionOfArray] = tinderPaper[randomizeArray];
                    tinderPaperDirty[positionOfArray] = tinderPaperDirty[randomizeArray];
                    greenCircle[positionOfArray] = greenCircle[randomizeArray];
                    tinderProfile[randomizeArray] = obj;
                    tinderPaper[randomizeArray] = obj2;
                    tinderPaperDirty[randomizeArray] = obj3;
                    greenCircle[randomizeArray] = obj4;

                    for (int i = 0; i < 16; i++)
                    {
                        tinderProfile[i].GetComponent<SpriteRenderer>().sortingOrder = (17 - i);
                    }
                }
            }

            public void ProfilSelectionEasy()
            {
                goodProfileNumber = Random.Range(1, 12);
                for (int i = 0; i < 16; i++)
                {
                    goodProfile[i] = false;
                }
                for (int i = 0; i < tinderPaper.Length; i++)
                {
                    tinderPaper[i].SetActive(false);
                    tinderPaperDirty[i].SetActive(false);
                }
                goodProfile[goodProfileNumber] = true;
            }

            public void ProfilSelectionMedium()
            {
                goodProfileNumber = Random.Range(1, 10);
                for (int i = 0; i < 16; i++)
                {
                    goodProfile[i] = false;
                }
                for (int i = 0; i < tinderPaper.Length; i++)
                {
                    tinderPaper[i].SetActive(false);
                    tinderPaperDirty[i].SetActive(false);
                }
                goodProfile[goodProfileNumber] = true;
            }

            public void ProfilSelectionHard()
            {
                goodProfileNumber = Random.Range(1, 8);
                for (int i = 0; i < 16; i++)
                {
                    goodProfile[i] = false;
                }
                for (int i = 0; i < tinderPaper.Length; i++)
                {
                    tinderPaper[i].SetActive(false);
                    tinderPaperDirty[i].SetActive(false);
                }
                goodProfile[goodProfileNumber] = true;
            }

            public void SwipeGauche()
            {
                source.PlayOneShot(swipeSound);
                tinderProfile[profilAnalizing].transform.DOMoveX(1f, 0.2f);
                tinderProfile[profilAnalizing].transform.DOMoveY(-3f, 0.2f);
                tinderProfile[profilAnalizing].transform.DORotate(RotationSwipe, 0.2f);
            }
            public void SwipeDroite()
            {
                source.PlayOneShot(swipeSound);
                tinderProfile[profilAnalizing].transform.DOMoveX(7f, 0.2f);
                tinderProfile[profilAnalizing].transform.DOMoveY(-3f, 0.2f);
                tinderProfile[profilAnalizing].transform.DORotate(-RotationSwipe, 0.2f);
            }

            public void AnimVictoire()
            {
                source.PlayOneShot(winSound);
                canPlay = false;
                win = true;
                ecranNoir.SetActive(true);
                for (int i = 0; i < greenCircle.Length; i++)
                {
                    greenCircle[i].SetActive(false);
                }
                greenCircle[goodProfileNumber].SetActive(true);
                greenCircle[goodProfileNumber].transform.DOScale(1f, (0.2f * 60 / bpm));
                personnageRondVert.transform.DOScale(0.2f, (0.2f * 60 / bpm));
                auraMatch.transform.DOScale(0.3f, (0.2f * 60 / bpm));
                auraPersonnage.transform.DOScale(0.3f, (0.2f * 60 / bpm));
                match.transform.DOScale(0.23f, (0.2f * 60 / bpm));
                epeeDroit.transform.DOMoveX(0.2f, (0.4f * 60 / bpm));
                epeeDroit.transform.DORotate(RotationEpees, (0.4f * 60 / bpm));
                epeeGauche.transform.DOMoveX(-0.2f, (0.4f * 60 / bpm));
                epeeGauche.transform.DORotate(-RotationEpees, (0.4f * 60 / bpm));
            }

            public void AnimDefaite()
            {
                source.PlayOneShot(defeatSound);
                canPlay = false;
                defeat = true;
                ecranDefaite.SetActive(true);
                personnageRondRouge.transform.DOScale(0.2f, (0.2f * 60 / bpm));
                auraPersonnageDefaite.transform.DOScale(0.3f, (0.2f * 60 / bpm));
                rate.transform.DOScale(0.23f, (0.2f * 60 / bpm));
            }

            public void PaperInit()
            {
                if (Tick == 0)
                {
                    paper.transform.DOMoveY(-0.6f, 0.2f);
                }
            }

            public void AuraRotation()
            {
                auraMatch.transform.Rotate(new Vector3(0, 0, 100f * Time.deltaTime));
                auraPersonnage.transform.Rotate(new Vector3(0, 0, 100f * Time.deltaTime));
                auraPersonnageDefaite.transform.Rotate(new Vector3(0, 0, 100f * Time.deltaTime));
                auraVerteUI.transform.Rotate(new Vector3(0, 0, 100f * Time.deltaTime));
                auraRougeUI.transform.Rotate(new Vector3(0, 0, 100f * Time.deltaTime));
                AuraVertProfil.transform.Rotate(new Vector3(0, 0, 100f * Time.deltaTime));
                AuraRougeProfil.transform.Rotate(new Vector3(0, 0, 100f * Time.deltaTime));
            }

            public void AuraFacile()
            {
                switch (currentDifficulty)
                {
                    case Difficulty.EASY:
                        if (goodProfile[profilAnalizing] == true)
                        {
                            auraRougeUI.SetActive(false);
                            auraVerteUI.SetActive(true);
                            AuraRougeProfil.SetActive(false);
                            AuraVertProfil.SetActive(true);
                            if (angelFlag)
                            {
                                source.PlayOneShot(angelSound);
                                angelFlag = false;
                            }
                        }
                        if (goodProfile[profilAnalizing] == false)
                        {
                            auraRougeUI.SetActive(true);
                            auraVerteUI.SetActive(false);
                            AuraRougeProfil.SetActive(true);
                            AuraVertProfil.SetActive(false);
                        }
                        break;
                }
            }

            public void PressionBAnnim()
            {
                rondBAppuye.SetActive(true);
                rondB.SetActive(false);
            }
            public void RelacherBAnnim()
            {
                rondBAppuye.SetActive(false);
                rondB.SetActive(true);
            }
            public void PressionXAnnim()
            {
                rondXAppuye.SetActive(true);
                rondX.SetActive(false);
            }
            public void RelacherXAnnim()
            {
                rondXAppuye.SetActive(false);
                rondX.SetActive(true);
            }
        }
    }
}

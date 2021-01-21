using System.Collections;
using UnityEngine;

namespace Brigantin
{
    namespace BouffeLesJoelle
    {
        /// <summary>
        /// Ludovic Eugenot
        /// </summary>
        public class Enemy : MonoBehaviour
        {
            #region Initiatlization
            [SerializeField] Collider2D col2D;
            [SerializeField] Collider2D dashReceptor;
            [SerializeField] SpriteRenderer sprRend;
            [SerializeField] SpriteRenderer sprColored;
            [SerializeField] DeathAnimation deathAnimation;
            [HideInInspector] public float timeUntilVulnerable = 2f;
            [HideInInspector] public float vulnerableTime = 1f;
            [HideInInspector] public float worldTimeOfDeath = 1f;
            [SerializeField] AudioSource source;
            [SerializeField] AudioClip fall1;
            [SerializeField] AudioClip fall2;
            bool amVulnerable = false;
            bool amNoLongerMoving = false;
            bool goingToDie = false;
            bool haveSpawned = false;
            bool amDead = false;

            float lerpValue;
            float startAlpha;
            float alpha;
            float birthTime;
            float ogVulTime;
            float panicTime = 0f;
            Vector2 startScale;
            Vector2 coloredSpriteBirthPosition;
            [HideInInspector] public Vector2 vulnerablePosition;
            Vector2 apparitionPosition;
            #endregion


            void Start()
            {
                col2D = col2D ? col2D : GetComponent<Collider2D>();
                //dashReceptor = dashReceptor ? dashReceptor : transform.GetChild(0).GetComponent<Collider2D>();
                sprRend = sprRend ? sprRend : GetComponent<SpriteRenderer>();
                sprColored = sprColored ? sprColored : transform.GetChild(1).GetComponent<SpriteRenderer>();
                deathAnimation = deathAnimation ? deathAnimation : transform.GetChild(2).GetComponent<DeathAnimation>();
                sprColored.enabled = false;
                startAlpha = 0.2f;
                alpha = startAlpha;
                sprRend.color = GetColor(255, 172, 172, startAlpha);
                col2D.enabled = false;
                sprRend.enabled = false;
                haveSpawned = false;
                source.volume = 0;
                startScale = transform.localScale;

                SetEnemyStats();

                source.clip = name[name.Length - 1].ToString().ToInt32() % 2 == 0 ? fall2 : fall1;
                ogVulTime = vulnerableTime;
            }

            void Update()
            {
                if (LUE_Manager.Instance.gameIsRunning)
                {
                    if (haveSpawned)
                    {
                        if (alpha < 0.75f)
                        {
                            lerpValue = (Time.time - birthTime) / timeUntilVulnerable;
                            alpha = Mathf.Lerp(0.3f, 0.8f, lerpValue);
                            transform.position = Vector2.Lerp(apparitionPosition, vulnerablePosition, lerpValue);
                            sprRend.color = GetColor(0, 0, 0, alpha);
                            source.volume = Mathf.Lerp(0, 0.5f, lerpValue);
                            transform.localScale = Vector2.Lerp(Vector2.zero, startScale, lerpValue);
                        }
                        else if (!amNoLongerMoving)
                        {
                            if (vulnerableTime > 0f)
                            {
                                coloredSpriteBirthPosition = (Vector2)transform.position + Vector2.up * 5f;
                                transform.localScale = startScale;
                                transform.position = vulnerablePosition;
                                vulnerableTime -= Time.deltaTime;
                                sprColored.enabled = true;
                                amNoLongerMoving = true;
                            }
                        }
                        else
                        {
                            vulnerableTime -= Time.deltaTime;
                            lerpValue = Mathf.InverseLerp(ogVulTime, 0f, vulnerableTime);
                            lerpValue = lerpValue > 0.3f ? 1 : lerpValue * 3;

                            if (!amVulnerable)
                            {
                                source.volume = Mathf.Lerp(0.5f, 1, lerpValue);
                                sprRend.color = GetColor(0, 0, 0, Mathf.Lerp(0.8f, 1, lerpValue));

                                if (lerpValue > 0.8f)
                                {
                                    col2D.enabled = true;
                                    sprRend.enabled = false;
                                    source.Stop();
                                    amVulnerable = true;
                                }
                            }
                            if (vulnerableTime > 0f)
                            {
                                if (lerpValue > .99f)
                                {
                                    if (panicTime < 0f)
                                    {
                                        sprColored.flipX = sprColored.flipX ? false : true;
                                        panicTime = 0.2f + Random.Range(-0.2f, 0.2f);
                                    }
                                    panicTime -= Time.deltaTime;
                                }

                                //alpha = Mathf.Lerp(0.6f, 1, lerpValue);
                                sprColored.transform.position = Vector2.Lerp(coloredSpriteBirthPosition, transform.position, lerpValue);
                                sprColored.color = new Color(255, 255, 255, deathAnimation.coloredEnemyAlpha);
                            }
                            else
                            {
                                sprColored.transform.localScale = new Vector3(deathAnimation.coloredEnemyScale, deathAnimation.coloredEnemyScale);
                                if (!goingToDie)
                                    IHaveSunk();
                            }
                        }
                    }
                    else
                    {
                        /*Debug.Log(transform.name+" doit spawn à Time : " + (Time.time - LUE_Manager.Instance.startTime) 
                            + " > que "+(worldTimeOfDeath - vulnerableTime - timeUntilVulnerable)
                            + ") + timeOfDeath (" + worldTimeOfDeath + ") - timeUntilVulnerable (" + timeUntilVulnerable+"))");*/
                        if (Time.time - LUE_Manager.Instance.startTime > worldTimeOfDeath - vulnerableTime - timeUntilVulnerable)
                        {
                            Spawn();
                        }
                    }
                }
            }

            private void SetEnemyStats()
            {
                switch (LUE_Manager.Instance.currentDifficulty)
                {
                    case Difficulty.EASY:
                        timeUntilVulnerable = TtT(2.5f);
                        vulnerableTime = TtT(1.5f);
                        break;
                    case Difficulty.MEDIUM:
                        timeUntilVulnerable = TtT(2.5f);
                        vulnerableTime = TtT(1.5f);
                        break;
                    case Difficulty.HARD:
                        switch (LUE_Manager.Instance.bpm)
                        {
                            case 120:
                                timeUntilVulnerable = TtT(2);
                                break;
                            case 140:
                                timeUntilVulnerable = TtT(2);
                                break;
                            default:
                                timeUntilVulnerable = TtT(1f);
                                break;
                        }
                        vulnerableTime = TtT(2f);
                        break;
                    default:
                        break;
                }
            }

            private void IHaveSunk()
            {
                if (!amDead)
                {
                    col2D.enabled = false;
                    deathAnimation.DieByDrowning();
                    amDead = true;
                }
            }

            public void KillMe()
            {
                col2D.enabled = false;
                goingToDie = true;
                deathAnimation.DieByJoelle(); // will destroy object from animation
            }

            public void NotifyEnemyDeathByJoelleBool()
            {
                LUE_Manager.Instance.DecrementEnemiesToEat();
            }

            public void NotifyEnemyMissedBool()
            {
                LUE_Manager.Instance.oneEnemyMissed = true;
            }

            public void DestroyThisObject()
            {
                Destroy(gameObject);
            }

            public void Spawn()
            {
                birthTime = Time.time;
                haveSpawned = true;
                sprRend.enabled = true;
                apparitionPosition = transform.position;
                source.Play();
            }

            Color GetColor(float r, float g, float b, params float[] optionalAlpha)
            {
                return new Color(Mathf.InverseLerp(0, 255, r), Mathf.InverseLerp(0, 255, g), Mathf.InverseLerp(0, 255, b), optionalAlpha.Length > 0 ? optionalAlpha[0] : 1f);
            }

            float TtT(float tickNumber)
            {
                return tickNumber * 60 / LUE_Manager.Instance.SceneBPM;
            }
        }
    }
}

using System;
using System.Collections;
using UnityEngine;

namespace Brigantin
{
    namespace BouffeLesJoelle
    {
        /// <summary>
        /// Ludovic Eugenot
        /// </summary>
        public class Player : MonoBehaviour
        {
            #region Initiatlization
            [SerializeField] [Range(0f, 50f)] float speed = 2f;
            //[SerializeField] [Range(0f, 50f)] float rotateSpeed = 2f;
            [SerializeField] [Range(0f, 1f)] float minimumInput = 0.5f;
            //[SerializeField] [Range(0f, 15f)] float minimumSpeed = 1.2f;
            [SerializeField] [Range(0f, 60f)] float maxSpeed = 5f;
            [SerializeField] [Range(0f, 60f)] float dashSpeed = 50f;
            [SerializeField] [Range(0f, 0.8f)] float dashTime = 0.2f;
            [SerializeField] [Range(0f, 0.8f)] float dashCooldown = 0.2f;
            [SerializeField] [Range(0f, 50f)] float boundariesPushStrength = 3f;
            Vector2 input = Vector3.zero;

            [SerializeField] Rigidbody2D rb2d;
            [SerializeField] Collider2D myCollider;
            [SerializeField] PlayerSounds myAudio;

            bool inControl = true;
            bool canDash = true;
            bool managerDependantInitDone = false;
            string enemyFound = "";
            bool dashTouchedEnemyFound = false;
            float currentDashTime;
            float currentDashCooldown;
            public float currentSpeed { get { return rb2d.velocity.magnitude; } }
            #endregion

            void Start()
            {
                rb2d = rb2d ? rb2d : GetComponent<Rigidbody2D>();
                myAudio = myAudio ? myAudio : transform.GetChild(0).GetComponent<PlayerSounds>();
                inControl = true;
                canDash = true;
                managerDependantInitDone = false;
            }

            private void Update()
            {
                if (!managerDependantInitDone)
                {
                    if (LUE_Manager.Instance)
                    {
                        SetPlayerStats();
                        managerDependantInitDone = true;
                    }
                }
                if (LUE_Manager.Instance.gameIsRunning)
                {
                    currentDashTime -= Time.deltaTime;
                    currentDashCooldown -= Time.deltaTime;
                    if (inControl)
                    {
                        GetInput();

                        if (canDash)
                        {
                            if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("A_Button"))
                            {
                                StartCoroutine(Dash(input));
                            }
                        }
                    }
                }
            }

            void FixedUpdate()
            {
                if (LUE_Manager.Instance.gameIsRunning)
                {
                    if (input.magnitude > minimumInput) //polish sur le demi-tour... il avance toujours en face de lui et le fait de tourner se fait litt�ralement avec les rotations
                    {
                        input.Normalize();
                        rb2d.AddForce(input * speed);
                        if (rb2d.velocity.magnitude > maxSpeed)
                        {
                            rb2d.velocity = rb2d.velocity.normalized * maxSpeed;
                        }
                        /*if (rb2d.velocity.magnitude < minimumSpeed)
                        {
                            rb2d.velocity = rb2d.velocity.normalized * minimumSpeed;
                        }*/

                        Vector2 rotGoal = Vector2.Lerp(new Vector2(rb2d.velocity.x, rb2d.velocity.y), new Vector2(input.x, input.y), 0.4f);
                        transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(-rotGoal.x, rotGoal.y) * Mathf.Rad2Deg);
                    }
                    /*else
                    {
                        rb2d.velocity = rb2d.velocity.normalized * minimumSpeed;
                    }*/
                    StayWithinMapBoudaries();

                }

            }

            private void SetPlayerStats()
            {
                switch (LUE_Manager.Instance.currentDifficulty)
                {
                    case Difficulty.EASY:
                        break;
                    case Difficulty.MEDIUM:
                        break;
                    case Difficulty.HARD:
                        dashCooldown *= 0.8f;
                        break;
                    default:
                        break;
                }
                switch (LUE_Manager.Instance.bpm)
                {
                    case 60:
                        break;
                    case 90:
                        break;
                    case 120:
                        dashCooldown *= 0.7f;
                        dashTime *= 0.7f;
                        break;
                    case 140:
                        dashCooldown *= 0.5f;
                        dashTime *= 0.6f;
                        break;
                    default:
                        break;
                }
            }

            private IEnumerator Dash(Vector2 direction)
            {
                myAudio.PlayJoelleDash();
                canDash = false;
                inControl = false;
                direction = EnemyInDashDrection(direction);
                speed = dashSpeed;
                currentDashTime = dashTime;
                rb2d.velocity = direction * dashSpeed;
                yield return new WaitUntil(new Func<bool>(() => currentDashTime < 0f || dashTouchedEnemyFound));
                inControl = true;
                currentDashCooldown = dashCooldown;
                yield return new WaitUntil(new Func<bool>(() => currentDashCooldown < 0f || canDash)); // WaitForSeconds(dashCooldown);
                canDash = true;
            }

            private Vector2 EnemyInDashDrection(Vector2 direction)
            {
                RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction, 30f, LayerMask.GetMask("Enemy"));

                if (hits.Length > 0)
                {
                    RaycastHit2D chosenHit = hits[0];
                    int enemyIndex = hits[0].transform.name != "Dash Receptor" ? -1 : (int)hits[0].transform.parent.name[hits[0].transform.parent.name.Length - 1].ToString().ToInt32();

                    foreach (RaycastHit2D hit in hits)
                    {
                        if ((hit.transform.name != "Dash Receptor" ? 0 : (int)hits[0].transform.parent.name[hits[0].transform.parent.name.Length - 1].ToString().ToInt32()) > enemyIndex &&
                            Vector2.Angle(direction, hit.transform.position - transform.position) < 30)
                        {
                            chosenHit = hit;
                        }
                    }

                    if (Vector2.Angle(direction, chosenHit.transform.position - transform.position) < 30)
                    {
                        enemyFound = chosenHit.transform.name;
                        return (chosenHit.transform.position - transform.position).normalized;
                    }
                    else
                    {
                        return direction;
                    }
                }
                else
                {
                    return direction;
                }
            }

            void GetInput()
            {
                input = Vector3.zero;
                if (LUE_Manager.Instance.isOnMyPrivateScene)
                {
                    if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                    {
                        input.x++;
                    }
                    if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow))
                    {
                        input.x--;
                    }
                    if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                    {
                        input.y--;
                    }
                    if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.UpArrow))
                    {
                        input.y++;
                    }
                }

                if (Input.GetAxis("Left_Joystick_X") != 0 || Input.GetAxis("Left_Joystick_Y") != 0)
                {
                    input = new Vector2(Input.GetAxis("Left_Joystick_X"), Input.GetAxis("Left_Joystick_Y"));
                }
                //input.Normalize();
            }

            void StayWithinMapBoudaries()
            {
                float power = 4.3f; //linéarité de la force des boundaries
                //bool outX = false;
                if (transform.position.x < -LUE_Manager.Instance.mapBoundariesX + 1 || transform.position.x > LUE_Manager.Instance.mapBoundariesX-1)
                {
                    //outX = true;
                    if (transform.position.x < 0)
                    {
                        if (input == Vector2.zero || Vector2.Angle(Vector2.right - (Vector2)transform.position, input) > 90)
                        {
                            rb2d.AddForce(Vector2.right * (float)(Math.Pow(Mathf.Abs(transform.position.x + /*--*/ (LUE_Manager.Instance.mapBoundariesX - 1)), power) * boundariesPushStrength));
                        }
                    }
                    else
                    {
                        if (input == Vector2.zero || Vector2.Angle(Vector2.left - (Vector2)transform.position, input) > 90)
                        {
                            rb2d.AddForce(Vector2.left * (float)(Math.Pow(Mathf.Abs(transform.position.x - (LUE_Manager.Instance.mapBoundariesX - 1)), power) * boundariesPushStrength));
                        }
                    }
                }
                if (transform.position.y < (-LUE_Manager.Instance.mapBoundariesY+1) || transform.position.y > (LUE_Manager.Instance.mapBoundariesY-1))
                {
                    if (transform.position.y < 0)
                    {
                        if (input == Vector2.zero || Vector2.Angle(Vector2.up - (Vector2)transform.position, input) > 90)
                        {
                            rb2d.AddForce(Vector2.up * (float)(Math.Pow(Mathf.Abs(transform.position.y + /*--*/ (LUE_Manager.Instance.mapBoundariesY - 1f)), power) * boundariesPushStrength));
                        }
                    }
                    else
                    {
                        if (input == Vector2.zero || Vector2.Angle(Vector2.down - (Vector2)transform.position, input) > 90)
                        {
                            rb2d.AddForce(Vector2.down * (float)(Math.Pow(Mathf.Abs(transform.position.y - (LUE_Manager.Instance.mapBoundariesY - 1f)), power) * boundariesPushStrength));
                        }
                    }
                    /*if (outX)
                    {
                        rb2d.velocity = (Vector2.down - (Vector2)transform.position).normalized * minimumSpeed;
                    }*/
                }
            }

            private void OnTriggerEnter2D(Collider2D collision)
            {
                if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    if (collision.transform.name != "Dash Receptor")
                    {
                        collision.GetComponent<Enemy>().KillMe();
                        myAudio.PlayJoelleCrunch();
                    }
                    else
                    {
                        if (collision.name == enemyFound)
                        {
                            if (rb2d.velocity.magnitude > maxSpeed)
                            {
                                rb2d.velocity = rb2d.velocity.normalized * maxSpeed;
                            }
                            dashTouchedEnemyFound = true;
                            canDash = true;
                        }
                    }
                }
            }

            /*private void OnCollisionEnter2D(Collision2D collision)
            {
                if (collision.gameObject.CompareTag("Wall"))
                {
                    float teleportDistance = 0.7f;
                    if (collision.transform.name == "Haut")
                    {
                        transform.position += Vector3.down * teleportDistance;
                    }
                    else if (collision.transform.name == "Bas")
                    {
                        transform.position += Vector3.up * teleportDistance;
                    }
                    else if (collision.transform.name == "Gauche")
                    {
                        transform.position += Vector3.right * teleportDistance;
                    }
                    else// (collision.transform.name == "Droite")
                    {
                        transform.position += Vector3.left * teleportDistance;
                    }
                }
            }*/
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TrapioWare
{
    namespace Spin
    {
        public class HammerPhysicController : TimedBehaviour
        {
            [Header("Spin Settings")]
            [Range(0.0f, 720.0f)] public float rotationStepForAddForce;
            [Range(0.0f, 720.0f)] public float rotationStepForceIncrease;
            [Range(0.0f, 100.0f)] public float baseForce;
            [Range(0.0f, 1.0f)] public float forceIncrease;
            [Range(0.0f, 10.0f)] public float forceFlatIncrease;
            [Range(0.0f, 30.0f)] public float maxForce;
            public DistanceJoint2D joint;

            [Header("Technical Settings")]
            [Range(0.0f, 1.0f)] public float minimumJoystickTilt = 0.8f;
            public bool isUsingRightJoystick = true;
            public Text[] debugText;
            public AudioClip whooshClip;
            public AudioClip whooshLaunchClip;
            public GameObject joystickGizmo;
            public GameObject bumperGizmo;

            [Header("Difficulty Settings")]
            public SpinManager spinManager;

            private Vector2 currentJoystickDirection;
            private Rigidbody2D hammerRb;
            private bool hammerStartedSpinning;
            private bool hammerReleased;
            private float currentForce;

            private float joystickAngleProgression;
            private float joystickAngleProgression2;
            private float joystickAngleBackwardProgression;
            private float joystickAngleBackwardProgression2;
            private float currentJoystickAngle;
            private bool isStartAngleSet;
            private float previousJoystickAngle;
            private float currentJoystickAngleMovement;
            private bool bumperPressed;
            private AudioSource source;
            private bool canPlayWhoosh;
            private bool showJoystick;

            public override void Start()
            {
                base.Start();
                hammerRb = GetComponent<Rigidbody2D>();
                currentForce = baseForce;
                rotationStepForAddForce /= bpm / 60;
                rotationStepForceIncrease /= bpm / 60;
                source = GetComponent<AudioSource>();
                canPlayWhoosh = false;
            }

            public void Update()
            {
                if(!spinManager.gameFinished)
                {
                    bumperPressed = Input.GetButton("Right_Bumper" ) || Input.GetButton("Left_Bumper");

                    currentJoystickDirection = new Vector2(isUsingRightJoystick ? Input.GetAxis("Right_Joystick_X") : Input.GetAxis("Left_Joystick_X"),
                        -(isUsingRightJoystick ? Input.GetAxis("Right_Joystick_Y") : Input.GetAxis("Left_Joystick_Y")));
                    currentJoystickAngle = Vector2.SignedAngle(Vector2.right, currentJoystickDirection);
                    if (currentJoystickAngle < 0)
                    {
                        currentJoystickAngle += 360;
                    }
                }

                RotateSprite();
            }


            public override void FixedUpdate()
            {
                base.FixedUpdate();

                UpdateHammerMovement();
            }


            public override void TimedUpdate()
            {
                debugText[4].text = (Tick - 1).ToString();
                if(Tick-1 >= 8 || spinManager.gameFinished)
                {
                    debugText[4].text = spinManager.hasWon ? "Won" : "Lose";
                }
            }

            private void UpdateHammerMovement()
            {
                if (currentJoystickDirection.magnitude > minimumJoystickTilt)
                {
                    if (!isStartAngleSet)
                    {
                        isStartAngleSet = true;
                        previousJoystickAngle = currentJoystickAngle;
                    }

                    currentJoystickAngleMovement = currentJoystickAngle - previousJoystickAngle;
                    if (currentJoystickAngleMovement < 180 && currentJoystickAngleMovement >= 0)
                    {
                        joystickAngleProgression += currentJoystickAngleMovement;
                        joystickAngleProgression2 += currentJoystickAngleMovement;
                    }

                    if (joystickAngleProgression > rotationStepForAddForce)
                    {
                        joystickAngleProgression = 0;
                        isStartAngleSet = false;
                        HammerAddForce(true);
                    }

                    if (joystickAngleProgression2 > rotationStepForceIncrease)
                    {
                        joystickAngleProgression2 = 0;
                        isStartAngleSet = false;
                        HammerIncreaseForce();
                    }


                    if (currentJoystickAngleMovement > -180 && currentJoystickAngleMovement < 0)
                    {
                        joystickAngleBackwardProgression -= currentJoystickAngleMovement;
                        joystickAngleBackwardProgression2 -= currentJoystickAngleMovement;
                    }

                    if (joystickAngleBackwardProgression > rotationStepForAddForce)
                    {
                        joystickAngleBackwardProgression = 0;
                        isStartAngleSet = false;
                        HammerAddForce(false);
                    }

                    if (joystickAngleBackwardProgression2 > rotationStepForceIncrease)
                    {
                        joystickAngleBackwardProgression2 = 0;
                        isStartAngleSet = false;
                        HammerIncreaseForce();
                    }

                    previousJoystickAngle = currentJoystickAngle;
                }
                else
                {
                    isStartAngleSet = false;
                    joystickAngleProgression = 0;
                    joystickAngleProgression2 = 0;
                    joystickAngleBackwardProgression = 0;
                    joystickAngleBackwardProgression2 = 0;
                }
                debugText[0].text = "Current Force : " + currentForce;
                debugText[1].text = "Joystick Angle Progression : " + joystickAngleProgression;
                debugText[2].text = "Joystick Angle Back Progression : " + joystickAngleBackwardProgression;


                if (bumperPressed && hammerStartedSpinning)
                {
                    if(!hammerReleased)
                    {
                        ReleaseHammer();
                    }
                    else
                    {
                        //TakeHammer();
                    }
                }

                Vector2 hammerDirectionFromCenter = transform.position - joint.transform.position;
                if (!canPlayWhoosh)
                {
                    if (hammerDirectionFromCenter.y > 0)
                    {
                        canPlayWhoosh = true;
                    }
                }
                else if(hammerDirectionFromCenter.y < -2 && !spinManager.gameFinished)
                {
                    canPlayWhoosh = false;
                    source.pitch = hammerRb.velocity.magnitude / 15;
                    source.PlayOneShot(whooshClip);
                }

                if(hammerRb.velocity.magnitude > 30)
                {
                    showJoystick = false;
                }
                if(hammerRb.velocity.magnitude < 5 && hammerDirectionFromCenter.y < -2 && !spinManager.gameFinished)
                {
                    showJoystick = true;
                }

                joystickGizmo.SetActive(showJoystick);
                bumperGizmo.SetActive(!showJoystick && !hammerReleased);

                if(spinManager.hasWon && hammerReleased)
                {
                    hammerRb.gravityScale = 0;
                }

            }

            private void HammerAddForce(bool clockwise)
            {
                if(!hammerReleased)
                {
                    hammerStartedSpinning = true;

                    Vector2 hammerDirectionFromCenter = transform.position - joint.transform.position;
                    float hammerClockwiseAngle = Vector2.SignedAngle(Vector2.right, hammerDirectionFromCenter) + (clockwise ? 90 : -90);
                    Vector2 hammerClockwiseDirection = new Vector2(Mathf.Cos(Mathf.Deg2Rad * hammerClockwiseAngle), Mathf.Sin(Mathf.Deg2Rad * hammerClockwiseAngle));
                    hammerRb.velocity += hammerClockwiseDirection.normalized * currentForce;
                }
            }
            private void HammerIncreaseForce()
            {
                if (!hammerReleased)
                {
                    hammerStartedSpinning = true;

                    currentForce *= 1 + forceIncrease;
                    currentForce += forceFlatIncrease;

                    if(currentForce > maxForce)
                    {
                        currentForce = maxForce;
                    }
                }
            }

            private void ReleaseHammer()
            {
                source.pitch = 1;
                source.PlayOneShot(whooshLaunchClip);
                joint.enabled = false;
                hammerReleased = true;
            }

            private void TakeHammer()
            {
                Debug.Log("Taijb");
                joint.enabled = true;
                hammerReleased = false;
            }

            private void RotateSprite()
            {
                if(!hammerReleased)
                {
                    Vector2 hammerDirectionFromCenter = transform.position - joint.transform.position;
                    transform.rotation = Quaternion.Euler(0.0f, 0.0f, Vector2.SignedAngle(Vector2.up, hammerDirectionFromCenter));
                }
            }
        }
    }
}
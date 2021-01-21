using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TrapioWare
{
    namespace Spin
    {
        public class HammerController : TimedBehaviour
        {
            [Header("Spin Settings")]
            public float baseSpeed;
            [Range(0.0f, 720.0f)] public float rotationStep;
            [Range(0.0f, 10.0f)] public float flatAcceleration;
            [Range(0.0f, 200.0f)] public float accelerationPercentage;
            [Range(0.0f, 1000.0f)] public float maxSpiningSpeed;
            [Range(0.0f, 1.0f)] public float lerpRatio;

            [Header("Technical Settings")]
            [Range(0.0f, 1.0f)] public float minimumJoystickTilt = 0.8f;
            public bool isRotationClockwise = false;
            public bool isUsingRightJoystick = true;
            public Text[] debugText;

            [Header("Launch Settings")]
            public float launchSpeedRatio;

            private float currentSpinSpeed;
            private float targetSpinSpeed;

            private float joystickAngleProgression;
            private float currentJoystickAngle;
            private Vector2 currentJoystickDirection;
            private bool isStartAngleSet;
            private float previousJoystickAngle;
            private float currentJoystickAngleMovement;

            private float joystickAngleBackwardProgression;

            public override void Start()
            {
                base.Start();
                currentSpinSpeed = 0;
            }

            public void Update()
            {
                currentJoystickDirection = new Vector2(isUsingRightJoystick ? Input.GetAxis("Right_Joystick_X") : Input.GetAxis("Left_Joystick_X"),
                    -(isUsingRightJoystick ? Input.GetAxis("Right_Joystick_Y") : Input.GetAxis("Left_Joystick_Y")));
                currentJoystickAngle = Vector2.SignedAngle(Vector2.right, currentJoystickDirection);
                if (currentJoystickAngle < 0)
                {
                    currentJoystickAngle += 360;
                }

                if(isRotationClockwise)
                {
                    currentJoystickAngle *= -1;
                }
            }


            public override void FixedUpdate()
            {
                base.FixedUpdate();

                UpdateHammerMovement();
                SpinHammer();
            }


            public override void TimedUpdate()
            {

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
                    }

                    if (joystickAngleProgression > rotationStep)
                    {
                        joystickAngleProgression = 0;
                        isStartAngleSet = false;
                        IncreaseHammerSpeed();
                    }

                    if (currentJoystickAngleMovement > -180 && currentJoystickAngleMovement < 0)
                    {
                        joystickAngleBackwardProgression -= currentJoystickAngleMovement;
                    }

                    if (joystickAngleBackwardProgression > rotationStep)
                    {
                        joystickAngleBackwardProgression = 0;
                        isStartAngleSet = false;
                        DecreaseHammerSpeed();
                    }


                    previousJoystickAngle = currentJoystickAngle;
                }
                else
                {
                    isStartAngleSet = false;
                    joystickAngleProgression = 0;
                    joystickAngleBackwardProgression = 0;
                }
                debugText[0].text = "Current Joystick Angle : " + currentJoystickAngle;
                debugText[1].text = "Joystick Angle Progression : " + joystickAngleProgression;
                debugText[2].text = "Joystick Angle Back Progression : " + joystickAngleBackwardProgression;
            }

            private void IncreaseHammerSpeed()
            {
                targetSpinSpeed *= 1 + accelerationPercentage / 100;
                targetSpinSpeed += flatAcceleration;

                if (targetSpinSpeed > maxSpiningSpeed)
                {
                    targetSpinSpeed = maxSpiningSpeed;
                }
            }

            private void DecreaseHammerSpeed()
            {
                targetSpinSpeed *= 1 - accelerationPercentage / 100;
                targetSpinSpeed -= flatAcceleration;

                if (targetSpinSpeed < 0)
                {
                    targetSpinSpeed = 0;
                }
            }

            private void SpinHammer()
            {
                currentSpinSpeed = Mathf.Lerp(currentSpinSpeed, targetSpinSpeed, lerpRatio);

                if(targetSpinSpeed != 0)
                {
                    transform.rotation = Quaternion.Euler(0.0f, 0.0f, transform.rotation.eulerAngles.z + (isRotationClockwise ? -currentSpinSpeed : currentSpinSpeed));
                }
                debugText[3].text = "Target speed : " + targetSpinSpeed;
            }
        }
    }
}
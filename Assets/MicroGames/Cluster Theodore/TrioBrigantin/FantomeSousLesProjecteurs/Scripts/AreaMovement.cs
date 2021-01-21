using System.Collections;
using UnityEngine;

namespace Brigantin
{
    namespace FantomeSousLesProjecteurs
    {
        public class AreaMovement : MonoBehaviour
        {
            #region Variables

            public float areaSize;
            public float moveSpeed;
            public bool canMove;

            #endregion

            #region Unity methods

            private void Awake()
            {
                canMove = true;
            }

            private void Start()
            {
                transform.localScale = new Vector3(areaSize, areaSize, 1f);
            }

            private void Update()
            {
                if(!canMove)
                {
                    return;
                }


                float inputHorizontal = Mathf.Abs(Input.GetAxis("Left_Joystick_X")) > 0.1 ? Input.GetAxis("Left_Joystick_X") : 0f;
                float inputVertical = Mathf.Abs(Input.GetAxis("Left_Joystick_Y")) > 0.1 ? Input.GetAxis("Left_Joystick_Y") : 0f;

                Vector3 movement = Vector3.zero;
                if(inputHorizontal == 0f && inputVertical == 0f)
                {
                    movement = Vector3.zero;
                }
                else
                {
                    movement = new Vector3(inputHorizontal, inputVertical, 0f).normalized;
                    transform.position = Vector3.MoveTowards(transform.position, transform.position + movement, moveSpeed * Time.deltaTime);
                }
            }

            #endregion

            #region Methods

            #endregion
        }
    }
}
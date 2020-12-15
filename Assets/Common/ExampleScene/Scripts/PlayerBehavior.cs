using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExampleScene
{
    public class PlayerBehavior : MonoBehaviour
    {
       
        public Vector3 rightPosition;
        public Vector3 leftPosition;


        // Update is called once per frame
        void Update()
        {
            if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetAxis("Left_Joystick_X") > 0.8f) && transform.position != rightPosition)
            {
                transform.position = rightPosition;
            }
            else if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetAxis("Left_Joystick_X") < -0.8f) && transform.position != leftPosition)
            {
                transform.position = leftPosition;
            }
        }


    }
}

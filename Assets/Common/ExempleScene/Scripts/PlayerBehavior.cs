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
            if (Input.GetKeyDown(KeyCode.RightArrow) && transform.position != rightPosition)
            {
                transform.position = rightPosition;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && transform.position != leftPosition)
            {
                transform.position = leftPosition;
            }
        }


    }
}

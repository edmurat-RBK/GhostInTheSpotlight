using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NuagesBehaviour : TimedBehaviour
{
    

    [Header("RestartPositions")]
    [SerializeField] private GameObject leftPosition;
    [SerializeField] private GameObject rightPosition;

    [Header("Mode")]
    [SerializeField] private bool goLeft;
    [SerializeField] private bool goRight;

    [SerializeField] private float speed;

    // Update is called once per frame
    void Update()
    {
        if (goLeft && TrapioWare.Climb.ClimbGameManager.Instance.needToStop == false)
        {
            if(transform.position.x <= leftPosition.transform.position.x)
            {
                transform.position = new Vector3(rightPosition.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
            }

            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
        }
        else if (goRight && TrapioWare.Climb.ClimbGameManager.Instance.needToStop == false)
        {
            if (transform.position.x >= rightPosition.transform.position.x)
            {
                transform.position = new Vector3(leftPosition.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
            }

            transform.Translate(Vector3.right * speed *Time.deltaTime, Space.World);
        }
        else
        {
            transform.Translate(Vector3.zero);
            
        }
    }

   
}

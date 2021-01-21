using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpanishInquisition
{
    namespace VegetablePirate
    {

        public enum ObjectsType
        {
            fruit,
            bomb
        }

        public class ObjectMovement : TimedBehaviour
        {
            private Transform katanaPosition;
            private Transform objectMovementTarget;
            private float radius;
            private float distanceToTarget;
            public bool hasBeenInZone;
            public bool isDestroyed;
            public float speed;
            public float scaleSpeed = 10f;
            public ObjectsType type;

            private GameManager manager;
            private SoundManager soundMngr;

            [SerializeField]
            private AudioSource bombNoise;

            public bool InZone ()
            {
                
                distanceToTarget = Vector2.Distance(katanaPosition.position, transform.position);

                //distanceToTarget = Mathf.Abs((target.position - transform.position).magnitude);
                if (distanceToTarget <= radius && !isDestroyed)
                {
                    hasBeenInZone = true;
                    return true;
                }                   
                else
                    return false;                        
            }

            public override void Start()
            {
                base.Start(); //Do not erase this line!               
                manager = GameManager.instance;
                soundMngr = SoundManager.instance;
                radius = manager.radius;
                katanaPosition = manager.target;
                objectMovementTarget = manager.trueTarget;
                speed = manager.speed;
            }       

            private void Update()
            {
                //fruit or bomb movement
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, objectMovementTarget.position, speed * Time.deltaTime); 
                if (transform.localScale.x < 1)
                {
                    transform.localScale += new Vector3(1, 1, 1) * scaleSpeed * Time.deltaTime;
                }
                
                transform.Rotate (Vector3.forward * (500 * Time.deltaTime));

                PassedZone();
            }

            private void PassedZone()
            {
                if (hasBeenInZone && !InZone() && type == ObjectsType.fruit && !manager.gameIsFinished)
                {
                    manager.gameIsFinished = true;
                    manager.gameIsWon = false;
                    manager.EndOfGameFeedback();
                }
            }
        }
    }
}
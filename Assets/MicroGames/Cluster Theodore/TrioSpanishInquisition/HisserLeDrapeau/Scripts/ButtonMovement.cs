using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpanishInquisition
{
    namespace HisserLeDrapeau
    {

        public enum ButtonsType
        {
            A, 
            B,
            X,
            Y
        }
        public class ButtonMovement : TimedBehaviour
        {
            private Transform target;
            private float radius;
            private float distanceToTarget;
            public float speed;
            public GameObject flag;
            public ButtonsType type;

            private NewGameManager manager;
            private SoundManager soundMngr;

            public bool InZone ()
            {
                distanceToTarget = Mathf.Abs((target.position - transform.position).magnitude);
                if (distanceToTarget <= radius)
                    return true;
                else
                    return false;                        
            }

            public override void Start()
            {
                base.Start(); //Do not erase this line!
                manager = NewGameManager.instance;
                soundMngr = SoundManager.instance;
                radius = manager.radius;
                target = manager.target;
                speed = manager.speed;

                //spawner = manager.spawner.transform;
            }       

            private void Update()
            {
                transform.position += Vector3.down * speed * Time.deltaTime;
            }

            private void OnBecameInvisible()
            {
                manager.activeButtons.Remove(this);
                Destroy(gameObject);
            }
        }
    }
}
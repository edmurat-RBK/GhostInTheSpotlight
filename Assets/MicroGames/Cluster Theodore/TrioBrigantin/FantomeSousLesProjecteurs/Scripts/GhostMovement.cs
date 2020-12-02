using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Brigantin
{
    namespace FantomeSousLesProjecteurs
    {
        public class GhostMovement : TimedBehaviour
        {
            [Range(5,25)]
            public float _baseSpeed;

            public float speed { get; set; }
            public Vector3 direction { get; set; }

            private void Awake()
            {
                speed = _baseSpeed;
                direction = new Vector3((2 * Random.value - 1), (2 * Random.value - 1), 0f).normalized;
                Debug.Log($"Random direction : {direction.x}, {direction.y}");
            }

            public override void Start()
            {
                // DO NOT REMOVE -----
                base.Start();
                // -------------------
            }

            public override void FixedUpdate()
            {
                // DO NOT REMOVE -----
                base.FixedUpdate();
                // -------------------

                transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
            }

            public override void TimedUpdate()
            {
                // DO NOT REMOVE -----
                base.TimedUpdate();
                // -------------------
            }

            private void OnCollisionEnter2D(Collision2D other)
            {
                if(other.gameObject.name.Equals("Top wall") || other.gameObject.name.Equals("Bottom wall"))
                {
                    direction = new Vector3(direction.x, -direction.y, 0f);
                }
                else if (other.gameObject.name.Equals("Left wall") || other.gameObject.name.Equals("Right wall"))
                {
                    direction = new Vector3(-direction.x, direction.y, 0f);
                }
            }
        }
    }
}

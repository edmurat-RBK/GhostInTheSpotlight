using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Brigantin
{
    namespace FantomeSousLesProjecteurs
    {
        public class GhostMovement : MonoBehaviour
        {
            [Range(5,25)]
            public float _baseSpeed;

            public float speed { get; set; }
            public Vector3 direction { get; set; }
            public bool inArea { get; set; }

            private void Awake()
            {
                speed = _baseSpeed;
                direction = new Vector3((2 * Random.value - 1), (2 * Random.value - 1), 0f).normalized;
                inArea = false;
            }

            public void Update()
            {
                transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
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

            private void OnTriggerEnter2D(Collider2D other)
            {
                if (other.gameObject.name.Equals("Middle Collider"))
                {
                    inArea = true;
                }
            }

            private void OnTriggerExit2D(Collider2D other)
            {
                if (other.gameObject.name.Equals("Middle Collider"))
                {
                    inArea = false;
                }
            }
        }
    }
}

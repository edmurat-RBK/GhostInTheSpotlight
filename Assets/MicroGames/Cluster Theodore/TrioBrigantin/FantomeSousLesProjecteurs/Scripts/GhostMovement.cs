using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Brigantin
{
    namespace FantomeSousLesProjecteurs
    {
        public class GhostMovement : MonoBehaviour
        {
            public bool canMove = true;

            public float speed { get; set; }
            public Vector3 direction { get; set; }
            public bool inArea { get; set; }

            private MicroManager microManager;
            private Animator anim;

            private void Awake()
            {
                direction = new Vector3((2 * Random.value - 1), (2 * Random.value - 1), 0f).normalized;
                inArea = false;
            }

            private void Start()
            {
                microManager = GameObject.Find("Micro Manager").GetComponent<MicroManager>();
                anim = GetComponent<Animator>();
                float iterationTime = 0.01f;
                float randomTime = Random.Range(microManager.blackScreenDelay + 0.5f, microManager.maxGameTime - 0.5f);

                float _x = Random.Range(-0.25f, 0.25f);
                float _y = Random.Range(-0.25f, 0.25f);
                Vector3 reverseDirection = -direction;

                for(float t=0; t<randomTime; t += iterationTime)
                {
                    _x += reverseDirection.x * (speed * iterationTime);
                    _y += reverseDirection.y * (speed * iterationTime);

                    if(-10f > _y || _y > 10f)
                    {
                        reverseDirection = new Vector3(reverseDirection.x, -reverseDirection.y, 0f);
                    }
                    if (-17.8f > _x || _x > 17.8f)
                    {
                        reverseDirection = new Vector3(-reverseDirection.x, reverseDirection.y, 0f);
                    }
                }

                if(direction.x > 0)
                {
                    anim.SetBool("leftDirection", false);
                }
                else
                {
                    anim.SetBool("leftDirection", true);
                }

                transform.position = new Vector3(_x, _y, 0f);
            }

            private void Update()
            {
                if (canMove)
                {
                    transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
                }
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
                    anim.SetBool("leftDirection", !anim.GetBool("leftDirection"));
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

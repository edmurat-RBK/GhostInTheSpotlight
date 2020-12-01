using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Testing;

namespace ExampleScene
{
    public class Enemy : MonoBehaviour
    {
        [Range(0.1f,1f)]
        public float speed;

        //distance at wich the enemy will hit the player
        public float minDistance;

        private void Start()
        {
            if(Manager.Instance.currentDifficulty >= Manager.Difficulty.MEDIUM)
            {
                speed = speed * 1.2f;
            }
        }

        void Update()
        {
            transform.Translate(Vector3.down*speed);

            TestPosition();

            if (transform.position.y <= -11)
            {
                Destroy(gameObject);
            }
        }

        // Raycast down and test if the ennemy is close enough to collid with the player
        private void TestPosition()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);
            if (hit.collider != null)
            {
                if (transform.position.y - hit.transform.position.y <= minDistance)
                {
                    Manager.Instance.Result(false);
                }
            }
        }
    }

}

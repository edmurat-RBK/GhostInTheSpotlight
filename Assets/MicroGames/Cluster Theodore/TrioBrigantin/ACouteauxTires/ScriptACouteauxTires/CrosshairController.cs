using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrioBrigantin
{
	namespace CouteauxTires
	{
		/// <summary>
		/// CHB -- Crosshair movement and interactions
		/// </summary>
		public class CrosshairController : MonoBehaviour
		{
			#region Variables
			[Range(0.1f, 500f)]
			public float movementSpeed = 1;

			Rigidbody2D crosshairRb;

			static public CrosshairController instance;

			// movement direction variables
			float horizontal = 0, vertical = 0;
			Vector2 movementVector = Vector2.zero;

            //target enemy
            [HideInInspector] public Enemy targetEnemy = null;

			[Header("Feedback fields")]
			public GameObject goodLock;
			[SerializeField] GameObject wrongLock;
			[SerializeField] string anyLockSound;
			#endregion
			private void Awake()
            {
				if(instance == null)
					instance = this;
                else
					Destroy(gameObject);
				
			}
            // Start is called before the first frame update
            void Start()
			{
				crosshairRb = GetComponent<Rigidbody2D>();
			}

            // Update is called once per frame
            void Update()
			{
				Lock();
				Move();
			}

			void Move()
            {
				horizontal = Input.GetAxis("Left_Joystick_X");
				vertical = -Input.GetAxis("Left_Joystick_Y");

				if (horizontal < -0.15 || horizontal > 0.15 || vertical < -0.15 || vertical > 0.15)
                {
					movementVector = new Vector2(horizontal, -vertical);
				}
                else
                {
					movementVector = Vector2.zero;
                }

				crosshairRb.velocity = movementVector.normalized * movementSpeed;
			}

			void Lock()
            {
                if (Input.GetButtonDown("A_Button"))
                {
					Debug.Log("Lock button pressed");
					if (ACouteauxTiré_Manager.instance.GetAmmoZero())
						return;
					
					if(targetEnemy != null)
                    {
						targetEnemy.TakeLock();
					}
                    else
                    {
						Instantiate(wrongLock, transform.position, Quaternion.identity).transform.SetParent(ACouteauxTiré_Manager.instance.spawnSetAnchor.transform);
					}

					ACouteauxTiré_Manager.instance.soundManager.Play(anyLockSound);
					ACouteauxTiré_Manager.instance.MinusAmmo();
				}
			}
		}
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Testing;

namespace LeRafiot
{
	namespace Choppe
	{
		/// <summary>
		/// Antoine LEROUX
		/// This script is use to detect drink in catch zone and by pressing A, catch the drink
		/// </summary>

		public class CatchSystem : MonoBehaviour
		{
			#region Variables
			public Color colorTriggered;
			private Color colorBase;

			private bool drinkInZone;
			private GameObject drinkTriggered;
            public bool goodDrink;

            [Header("Arms")]
            public GameObject armDown;
            public GameObject armUp;

            private bool canCatch = true;
			#endregion

			// Start is called before the first frame update
			void Start()
			{
				colorBase = GetComponent<SpriteRenderer>().color;
				drinkInZone = false;

                armDown.SetActive(true);
                armUp.SetActive(false);
            }

			// Update is called once per frame
			void Update()
			{
                if (canCatch == true)
                {

                    if (drinkInZone)
                    {
					    GetComponent<SpriteRenderer>().color = colorTriggered;
					
					    if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("A_Button") && !Manager.Instance.panel.activeSelf)
                        {
                            armDown.SetActive(false);
                            armUp.SetActive(true);

                            if (goodDrink)
                            {
                                canCatch = false;
                                DrinkManager.Instance.canSpawn = false;
                                Destroy(drinkTriggered);
                                Manager.Instance.Result(true);
                                SoundManagerChoppe.Instance.sfxSound[4].Play();
                                SoundManagerChoppe.Instance.sfxSound[0].Play();
                            }
                            else
                            {
                                canCatch = false;
                                DrinkManager.Instance.canSpawn = false;
                                Destroy(drinkTriggered);
                                Manager.Instance.Result(false);
                                SoundManagerChoppe.Instance.sfxSound[5].Play();
                                SoundManagerChoppe.Instance.sfxSound[1].Play();
                            }
					    }
				    }

                    else
                    {
					    GetComponent<SpriteRenderer>().color = colorBase;
				    }
                }
			}

            private void OnTriggerEnter2D(Collider2D col)
            {
                if (col.CompareTag("Enemy2"))
                {
                    goodDrink = true;
                    drinkTriggered = col.gameObject;
                    drinkInZone = true;
                }

                if (col.CompareTag("Enemy1"))
                {
                    drinkInZone = true;
                    drinkTriggered = col.gameObject;
                }
            }

            private void OnTriggerExit2D(Collider2D col)
            {
                if (col.CompareTag("Enemy1"))
                {
					drinkInZone = false;					
				}

                if (col.CompareTag("Enemy2"))
                {
                    goodDrink = false;
                }
            }
        }
	}
}
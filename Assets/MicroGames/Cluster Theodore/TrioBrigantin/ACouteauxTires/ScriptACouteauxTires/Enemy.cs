using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrioBrigantin
{
	namespace CouteauxTires
	{
		/// <summary>
		/// CHB -- Enemy behavior
		/// </summary>
		public class Enemy : MonoBehaviour
		{
			#region Variables
			BoxCollider2D detectZone;

			//placeholder feedbacks
			public SpriteRenderer mySprite;
			public SpriteRenderer barrelSprite;

			[Range(1,2)]
			[SerializeField] int needLock = 1;

			[Header("Feedback fields")]
			[SerializeField] string superEnemyHey;
			[SerializeField] string goodLockSound;
			public string deathSound;
			public string defeatSound;
			[SerializeField] AmmoCounter needLockCounter;
			#endregion

			// Start is called before the first frame update
			void Start()
			{
				detectZone = GetComponent<BoxCollider2D>();
				needLockCounter.InitAmmoCounter(needLock);
			}

            #region Trigger calls
            private void OnTriggerEnter2D(Collider2D collision)
            {
                if(collision.gameObject == CrosshairController.instance.gameObject)
                {
					CrosshairController.instance.targetEnemy = this;
					mySprite.color = Color.blue;
                }
            }

            private void OnTriggerExit2D(Collider2D collision)
            {
				if (collision.gameObject == CrosshairController.instance.gameObject)
				{
					CrosshairController.instance.targetEnemy = null;
					mySprite.color = Color.white;
				}
			}
            #endregion

			public void TakeLock()
            {
				needLock--;
				needLockCounter.DiscountKnife(needLock);

				if(needLock == 1)
                {
					ACouteauxTiré_Manager.instance.soundManager.Play(superEnemyHey);
				}
				else if(needLock == 0)
                {
					detectZone.enabled = false;
					CrosshairController.instance.targetEnemy = null;
					ACouteauxTiré_Manager.instance.enemiesKilled.Add(this);
					Instantiate(CrosshairController.instance.goodLock, transform.position, Quaternion.identity).transform.SetParent(gameObject.transform);
					mySprite.color = Color.magenta;
					ACouteauxTiré_Manager.instance.soundManager.Play(goodLockSound);
				}
            }
        }
    }
}
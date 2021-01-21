using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrioBrigantin
{
	namespace CouteauxTires
	{
		public class AmmoCounter : MonoBehaviour
		{
			#region Variables
			[SerializeField] GameObject[] knifeIcons;
            #endregion
            private void Awake()
            {
				foreach (GameObject icon in knifeIcons)
					icon.SetActive(false);
            }
            public void InitAmmoCounter(int _amount)
            {
				for(int i = 0; i < _amount; i++)
                {
					knifeIcons[i].SetActive(true);
                }
            }
			public void DiscountKnife(int _id)
            {
				knifeIcons[_id].SetActive(false);
			}
		}
	}
}
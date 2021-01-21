using System.Collections;
using UnityEngine;

namespace Brigantin
{
    namespace BouffeLesJoelle
    {
        /// <summary>
        /// Ludovic Eugenot
        /// </summary>
        public class DefeatJoelleScript : MonoBehaviour
        {
            #region Initiatlization
            [Range(0f, 360f)] public float rotateSpeed = 20f;
            #endregion

            void Update()
            {
                transform.Rotate(new Vector3(0, 0, rotateSpeed * Time.deltaTime));
            }
        }
    }
}

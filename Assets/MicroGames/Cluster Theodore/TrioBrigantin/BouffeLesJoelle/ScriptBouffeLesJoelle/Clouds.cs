using System.Collections;
using UnityEngine;

namespace Brigantin
{
    namespace BouffeLesJoelle
    {
        public class Clouds : MonoBehaviour
        {
            #region Initiatlization
            [SerializeField] SpriteRenderer spr;

            public float cloudDistance = 2f;
            public float alpha = 2f;

            float startAlpha = 0f;
            float endAlpha = 0f;
            float startScale = 0f;
            float endScale = 0f;

            Vector2 startPos;
            Vector2 endPos;

            float timing = 0f;
            float currentTime = 0f;
            #endregion

            void Start()
            {
                spr = spr ? spr : transform.GetComponent<SpriteRenderer>();

                startScale = Random.Range(0.4f, 1f);
                endScale = Random.Range(0.4f, 1f);
                startAlpha = alpha;
                startPos = transform.position;
                endPos = new Vector2(transform.position.x + cloudDistance, transform.position.y);

                timing = TtT(7);
            }

            void Update()
            {
                currentTime += Time.deltaTime;
                transform.position = Vector2.Lerp(startPos, endPos, currentTime / timing);

                transform.localScale = Vector3.Lerp(new Vector3(startScale, startScale), new Vector3(endScale, endScale), currentTime / timing);
                spr.color = new Color(0, 0, 0, Mathf.Lerp(startAlpha, endAlpha, currentTime / timing));
            }
            float TtT(float tickNumber)
            {
                return tickNumber * 60 / LUE_Manager.Instance.SceneBPM;
            }
        }
    }
}

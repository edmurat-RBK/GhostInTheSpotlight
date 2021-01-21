using System.Collections;
using UnityEngine;

namespace Brigantin
{
    namespace BouffeLesJoelle
    {
        public class DeathAnimation : MonoBehaviour
        {
            #region Initialization
            [Range(0,1)] public float coloredEnemyAlpha=1;
            [Range(0,1)] public float coloredEnemyScale=1;
            [SerializeField] SpriteRenderer coloredSpr;
            [SerializeField] Enemy enemyScript;
            [SerializeField] Animation anim;
            #endregion

            private void Start()
            {
                coloredSpr = coloredSpr ? coloredSpr : transform.parent.GetChild(1).GetComponent<SpriteRenderer>();
                enemyScript = enemyScript ? enemyScript : transform.parent.GetComponent<Enemy>();
                anim = anim ? anim : transform.parent.GetComponent<Animation>();
                anim.clip = anim.GetClip("idle");
                coloredEnemyAlpha = 1;
                coloredEnemyScale = 1;
            }

            public void DieByJoelle()
            {
                anim.clip = anim.GetClip("Enemy Death by Joelle Animation");
                anim.Play();
            }

            public void DieByDrowning()
            {
                anim.clip = anim.GetClip("Enemy Death by Drowning Animation");
                anim.Play();
            }


            //From Animation
            public void AnimationFinished()
            {
                enemyScript.DestroyThisObject();
            }

            public void NotifyEnemyMissedBool()
            {
                enemyScript.NotifyEnemyMissedBool();
            }

            public void NotifyEnemyDeathByJoelleBool()
            {
                enemyScript.NotifyEnemyDeathByJoelleBool();
            }
        }
    }
}

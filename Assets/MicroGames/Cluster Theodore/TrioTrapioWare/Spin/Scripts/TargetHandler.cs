using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrapioWare
{
    namespace Spin
    {
        public class TargetHandler : TimedBehaviour
        {
            public SpinManager spinManager;
            public GameObject destroyEffectPrefab;
            public GameObject hitEffectPrefab;
            public AudioClip crushClip;

            private Collider2D targetCollider;
            private ContactFilter2D hammerFilter;
            private SpriteRenderer spriteRenderer;
            private AudioSource source;

            public override void Start()
            {
                base.Start();
                targetCollider = GetComponent<Collider2D>();
                spriteRenderer = GetComponent<SpriteRenderer>();
                hammerFilter.SetLayerMask(LayerMask.GetMask("Enemy"));
                hammerFilter.useTriggers = true;
                source = GetComponent<AudioSource>();
            }


            public override void FixedUpdate()
            {
                base.FixedUpdate();
                List<Collider2D> colliders = new List<Collider2D>();
                if(Physics2D.OverlapCollider(targetCollider, hammerFilter, colliders) > 0 && !spinManager.gameFinished)
                {
                    Explode(colliders[0].transform.position);
                }
            }


            public override void TimedUpdate()
            {

            }

            public void Explode(Vector2 hitPos)
            {
                Instantiate(destroyEffectPrefab, transform.position, Quaternion.identity);
                Instantiate(hitEffectPrefab, hitPos, Quaternion.identity);
                source.PlayOneShot(crushClip);
                spriteRenderer.enabled = false;
                spinManager.Win();
            }
        }
    }
}
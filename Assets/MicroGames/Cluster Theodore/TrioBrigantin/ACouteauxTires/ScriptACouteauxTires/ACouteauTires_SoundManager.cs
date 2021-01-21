using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrioBrigantin
{
	namespace CouteauxTires
	{
		public class ACouteauTires_SoundManager : MonoBehaviour
		{
			#region Variables
			private AudioSource[] sourceGetter;
			private Dictionary<string, AudioSource> d_sounds = new Dictionary<string, AudioSource>();
            public string namePlayTest;
            #endregion

            private void Awake()
            {
                sourceGetter = GetComponents<AudioSource>();

                foreach (AudioSource source in sourceGetter)
                {
					d_sounds.Add(source.clip.name, source);
                }
            }
            private void Update()
            {
                if (Input.GetKeyDown(KeyCode.P))
                {
                    Play(namePlayTest);
                }
            }
            public void Play(string name)
            {
                AudioSource s2g = d_sounds[name];

                if (s2g == null)
                {
                    Debug.LogWarning("Sound: " + name + " not found!");
                    return;
                }

                s2g.Play();
            }
		}
	}
}
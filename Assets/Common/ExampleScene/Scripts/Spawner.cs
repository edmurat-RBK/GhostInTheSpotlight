using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Testing;


namespace ExampleScene
{
    public class Spawner : TimedBehaviour
    {
        private float spawnCooldown;

        //references
        public PlayerBehavior player;
        public GameObject ennemy;
        public Vector3 rightPosition;
        public Vector3 leftPosition;
        // previous ennemy was left or right
        private bool left;

        //stop spawns on end game
        private bool canSpawn = true;

        private bool isHard;

        [Header("UI")]
        //win panel
        public GameObject panel;
        public TextMeshProUGUI resultText;
        public TextMeshProUGUI bpmText;
        public Slider timerUI;
        public TextMeshProUGUI tickNumber;

        public override void Start()
        {
            base.Start();
            bpmText.text = "bpm: " + bpm.ToString();
            if (Manager.Instance.currentDifficulty == Difficulty.HARD)
                isHard = true;
            spawnCooldown = 60 / bpm;
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            timerUI.value = (float)timer / spawnCooldown;

        }

        public override void TimedUpdate()
        {
            base.TimedUpdate();
            if (canSpawn)
            {
             
                if ( Tick<8)
                {
                    if (!isHard)
                        NormalSpawn();
                    else
                        StartCoroutine(HardSpawn());
                }

               
                tickNumber.text = Tick.ToString();
                if (Tick == 8)
                {
                    Result(true);
                }
            }
        }

        private void NormalSpawn()
        {
            if (left)
                Instantiate(ennemy, leftPosition, Quaternion.identity, transform);
            else
                Instantiate(ennemy, rightPosition, Quaternion.identity, transform);
            left = !left;
        }
        private IEnumerator HardSpawn()
        {
            NormalSpawn();
            if (Random.Range(0, 2) == 1)
            {
                yield return new WaitForSeconds(spawnCooldown / 2);
                NormalSpawn();
            }

        }
        public void Result(bool win)
        {
            canSpawn = false;
            Manager.Instance.Result(win);
        }
    }

}

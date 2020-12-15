using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Testing { 
    public class Manager : Singleton<Manager>
    {
     
        private void Awake()
        {
            CreateSingleton(true);
        }

        #region Variables
        public IDCard idCard;
      
        public BPM bpm = BPM.Slow;
        
        public Difficulty currentDifficulty;

        [Header("UI Management")]
        public GameObject panel;
        public TextMeshProUGUI resultText;
        public GameObject verbePanel;
        public TextMeshProUGUI verbeText;
        public Image inputImage;
        public GameObject sceneCam;
        public bool isLoaded;
        #endregion

        #region Methods
        public IEnumerator Start()
        {
            var _scene = SceneManager.LoadSceneAsync(idCard.microGameScene.BuildIndex, LoadSceneMode.Additive);
            _scene.allowSceneActivation = false;
            verbeText.text = idCard.verbe;
            inputImage.sprite = idCard.inputs;
            yield return new WaitForSeconds(2f);
            sceneCam.SetActive(false);
            verbePanel.SetActive(false);
            _scene.allowSceneActivation = true;
            isLoaded = true;
        }

        /// <summary>
        /// Debug the result of the game on a panel
        /// </summary>
        /// <param name="win"> if true the game is won , if false the game is lost</param>
        public void Result(bool win)
        {
            if(win)           
                resultText.text = "You Won!";
            
            else        
                resultText.text = "You Lost!";
            
            panel.SetActive(true);

        }
        #endregion
    }
}
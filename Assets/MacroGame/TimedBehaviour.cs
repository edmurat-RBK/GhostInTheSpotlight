using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Testing;
using UnityEngine.SceneManagement;

public class TimedBehaviour : MonoBehaviour
{
   [HideInInspector] public float bpm = 60;
    [HideInInspector] public Manager.difficulty currentDifficulty = 0;

    private bool isInPlayableScene;
    public double timer;
    public double currentTime;

    // tic increment every timed update, at 8 you must call the result
    public int Tick
    {
        get;
        private set;
    }

    public virtual void Start()
    {
        if (SceneManager.GetActiveScene().name == "TestingScene")
        {
            isInPlayableScene = true;
            bpm =(float) Manager.Instance.bpm;
            currentDifficulty = Manager.Instance.currentDifficulty;
            currentTime = AudioSettings.dspTime;
        }
    }


    public virtual void FixedUpdate()
    {
        if (isInPlayableScene)
            UpdatePlayableScene();
        else
            UpdateCustomScene();
    }
    #region SecuredUpdates
    /// <summary>
    /// Update on a custom scene of trio
    /// </summary>
    private void UpdateCustomScene()
    {
        timer = Time.deltaTime;
        if (timer >= 60 / bpm)
        {
            timer = 0;
            TimedUpdate();            
        }
    }
    /// <summary>
    /// Update on a scen with manager
    /// </summary>
    private void UpdatePlayableScene()
    {
        timer = AudioSettings.dspTime - currentTime;
        if (timer >= 60 / bpm)
        {
            currentTime = AudioSettings.dspTime;
            TimedUpdate();            
        }
    }
    #endregion

    /// <summary>
    /// TimedUdpate is call at each tic. Use this if you want your script to update with rithme
    /// </summary>
    public virtual void TimedUpdate()
    {
        Tick++;
    }

}

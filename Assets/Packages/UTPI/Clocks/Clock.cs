﻿using UnityEngine;
using UTPI.Clocks;

public class Clock
{
    #region Variables

    /// <summary>
    /// Current time of the clock.
    /// </summary>
    public float time { get; protected set; } = 0f;

    /// <summary>
    /// Returns true if the clock is paused.
    /// </summary>
    public bool paused { get; protected set; } = false;

    /// <summary>
    /// Returns true when the clock hits zero.
    /// </summary>
    public bool onFinish { get; protected set; } = false;

    /// <summary>
    /// Returns true if the clock's timer is equal to zero.
    /// </summary>
    public bool finished { get; protected set; } = false;

    bool subscription = false;

    #endregion

    #region Constructors

    public Clock()
    {
        if (time < 0f)
        {
            Debug.LogError("Can not create a clock with a negative time");
            return;
        }
        SubscribeToUpdate();
    }

    public Clock(float time)
    {
        if (time <= 0f)
        {
            Debug.LogError("Can not create a clock with a negative time");
            return;
        }
        this.time = time;
        SubscribeToUpdate();
    }

    public Clock(float time, bool startsPaused)
    {
        if (time <= 0f)
        {
            Debug.LogError("Can not create a clock with a negative time");
            return;
        }
        this.time = time;
        paused = startsPaused;
        SubscribeToUpdate();
    }

    #endregion

    #region Internal Methods

    protected virtual void Run()
    {
        if (paused)
            return;

        if (time <= 0)
        {
            onFinish = false;
            subscription = false;
            UpdateCaller.OnUpdate -= Run;
            return;
        }

        CountDown();
    }

    protected virtual void CountDown()
    {
        time -= Time.smoothDeltaTime;

        if (time <= 0f)
        {
            finished = true;
            onFinish = true;
        }
    }

    protected void SubscribeToUpdate()
    {
        if (UpdateCaller.Instance == null)
        {
            UpdateCaller caller = new GameObject("[Update Caller]").AddComponent<UpdateCaller>();
            UpdateCaller.Instance = caller;
            Object.DontDestroyOnLoad(UpdateCaller.Instance.gameObject);
        }
        UpdateCaller.Instance.gameObject.hideFlags = HideFlags.HideInHierarchy;
        subscription = true;
        UpdateCaller.OnUpdate += Run;
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Pauses the clock.
    /// </summary>
    /// <param name="clock"></param>
    public void Pause()
    {
        paused = true;
    }

    /// <summary>
    /// Unpauses the clock.
    /// </summary>
    /// <param name="clock"></param>
    public void Play()
    {
        paused = false;
    }

    /// <summary>
    /// Stops the clock and set its time to zero.
    /// </summary>
    /// <param name="clock"></param>
    public void Stop()
    {
        time = 0f;
    }

    /// <summary>
    /// Gives a new time to the clock.
    /// </summary>
    /// <param name="time"></param>
    public void SetTime(float time)
    {
        if (time <= 0f)
            return;
        onFinish = false;
        finished = false;
        this.time = time;
        if (subscription == false)
        {
            subscription = true;
            UpdateCaller.OnUpdate += Run;
        }
    }

    #endregion
}
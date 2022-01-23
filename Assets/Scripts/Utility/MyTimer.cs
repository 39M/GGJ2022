using System;
using UnityEngine;
using System.Collections.Generic;

public enum CounterState
{
    start,
    disable,
}

public class MyTimer : MonoBehaviour
{
    public static MyTimer instance;
    private List<TimeCounter> timerArray = new List<TimeCounter>();

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        instance = this;
    }

    private void FixedUpdate()
    {
        for (int i = timerArray.Count - 1; i >= 0; i--)
        {
            if (timerArray[i].IsAlive())
            {
                timerArray[i].Update(Time.deltaTime);
            }
            else
            {
                timerArray[i] = null;
                timerArray.RemoveAt(i);
            }
        }
    }

    public TimeCounter StartTimer(float maxCount)
    {
        TimeCounter t = new TimeCounter(maxCount, 1, null, false);
        t.Start();
        timerArray.Add(t);
        return t;
    }

    public TimeCounter StartTimer(float interval, Action action)
    {
        TimeCounter t = new TimeCounter(interval, action);
        t.Start();
        timerArray.Add(t);
        return t;
    }

    public TimeCounter StartTimer(float interval, int runNum, Action action)
    {
        TimeCounter t = new TimeCounter(interval, runNum, action);
        t.Start();
        timerArray.Add(t);
        return t;
    }

    public void StopTimer(TimeCounter t)
    {
        t.Kill();
    }

    public TimeCounter WaitTimer(float wait, Action action)
    {
        TimeCounter t = new TimeCounter(wait, 1, action, true);
        t.Start();
        timerArray.Add(t);
        return t;
    }

    public void StopAllTimer()
    {
        timerArray.Clear();
    }

    private void OnDestroy()
    {
        //StopAllTimer();
    }

}


public class TimeCounter
{
    public float time;          //计时器
    private float interval;     //执行间隔
    private CounterState state; //执行状态
    private Action action;      //执行行为
    private int runNum;         //设定执行次数
    private int curNum;         //当前计数值
    private bool countAble;     //是否需要计数
    private bool initRun;       //初始化执行一次


    public TimeCounter(float interval, Action action)
    {
        time = interval;
        this.interval = interval;
        state = CounterState.disable;
        this.action = action;
        countAble = false;
    }

    public TimeCounter(float interval, int runNum, Action action)
    {
        time = interval;
        this.interval = interval;
        state = CounterState.disable;
        this.action = action;
        curNum = 0;
        this.runNum = runNum;
        countAble = true;
    }

    public TimeCounter(float interval, int runNum, Action action, bool initRun)
    {
        this.interval = interval;
        state = CounterState.disable;
        this.action = action;
        curNum = 0;
        this.runNum = runNum;
        this.initRun = initRun;
        countAble = true;
        if (this.initRun)
        {
            time = 0;
        }
        else
        {
            time = interval;
        }
    }

    public void Start()
    {
        state = CounterState.start;
    }

    public void Kill()
    {
        state = CounterState.disable;
    }

    public bool IsAlive()
    {
        return state == CounterState.start;
    }

    public void Update(float delt)
    {
        time += delt;
        if (time >= interval)
        {
            time -= interval;
            if (action != null)
            {
                action.Invoke();
            }
            curNum++;
            if (countAble && curNum >= runNum && runNum != -1)
            {
                Kill();
            }
        }
    }
    
}
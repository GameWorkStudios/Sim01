using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoSingleton<TimeController>
{

    #region Events
    [SerializeField] private VoidEvent OnTimerTick;
    #endregion Events

    [SerializeField] private float timeMultiplier = 1f;
    [SerializeField] private float startHour;
    private DateTime currentTime;

    private int lastMin = 0;

    public DateTime CurrentTime{
        get{
            return this.currentTime;
        }
    }

    void Start()
    {
        currentTime = DateTime.Now.Date + TimeSpan.FromHours(startHour);
    }

    void Update()
    {
        UpdateTimeOfDay();        
    //    Debug.Log(currentTime.TimeOfDay.ToString());
    }

    private void UpdateTimeOfDay(){
        currentTime = currentTime.AddSeconds(Time.deltaTime * timeMultiplier);   
        MinuteEventRaiser();    
        lastMin = currentTime.Minute;    
    }

    public void IncreaseTimeSpeed(int multiplier){
        this.timeMultiplier *= multiplier;
    }

    private void MinuteEventRaiser(){
        if(OnTimerTick == null) return;

        if(currentTime.Minute != lastMin){
            OnTimerTick.Raise(null);    
        }
    }
}

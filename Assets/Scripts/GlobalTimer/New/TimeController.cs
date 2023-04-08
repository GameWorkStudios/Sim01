using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoSingleton<TimeController>
{
    [SerializeField] private float timeMultiplier = 1f;
    [SerializeField] private float startHour;
    private DateTime currentTime;

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
    }

    private void UpdateTimeOfDay(){
        currentTime = currentTime.AddSeconds(Time.deltaTime * timeMultiplier);        
    }

    public void IncreaseTimeSpeed(int multiplier){
        this.timeMultiplier *= multiplier;
    }
}

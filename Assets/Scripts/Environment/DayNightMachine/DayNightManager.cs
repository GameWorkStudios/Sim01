using UnityEngine;
using System;

public class DayNightManager : DayNightStateMachine
{
    [Header("Light Sources")]
    [SerializeField] private Light sunSource;
    
    [Header("DayTime Hours")]
    [SerializeField] private float sunRiseHour;
    [SerializeField] private float sunSetHour;

    [Header("Ambient Colors")]
    [SerializeField] private Color dayTimeAmbientColor;
    [SerializeField] private Color nightTimeAmbientColor;

    [SerializeField]
    private AnimationCurve ambientCurve;

    [SerializeField] private float maxSunLightIntensity;
    [SerializeField] private Light moonLigth;
    [SerializeField] private float maxMoonLightIntensity;

    private TimeSpan sunRiseTime;
    private TimeSpan sunSetTime;

    protected override void Start()
    {
        base.Start();
        this.sunRiseTime = TimeSpan.FromHours(sunRiseHour);
        this.sunSetTime = TimeSpan.FromHours(sunSetHour);
    }

    protected override void Update()
    {
        base.Update();
        this.RotateSun();
        this.UpdateLightSettings();
    }

    private void RotateSun(){
        float sunLightRotation;
        if(TimeController.Instance.CurrentTime.TimeOfDay > sunRiseTime && TimeController.Instance.CurrentTime.TimeOfDay < sunSetTime ){
            //Day Time
            TimeSpan sunriseToSunsetDuration = CalculateTimeDiff(sunRiseTime, sunSetTime);
            TimeSpan timeSinceSunrise = CalculateTimeDiff(sunRiseTime, TimeController.Instance.CurrentTime.TimeOfDay);
            double percent = timeSinceSunrise.TotalMinutes / sunriseToSunsetDuration.TotalMinutes;
            sunLightRotation = Mathf.Lerp(0,180, (float) percent);
        }else{
            //Night Time
            TimeSpan sunsetToSunRiseDuration = CalculateTimeDiff(sunSetTime, sunRiseTime);
            TimeSpan timeSinceSunset = CalculateTimeDiff(sunSetTime, TimeController.Instance.CurrentTime.TimeOfDay);
            double percent = timeSinceSunset.TotalMinutes / sunsetToSunRiseDuration.TotalMinutes;
            sunLightRotation = Mathf.Lerp(180, 360, (float)percent);
        }
        sunSource.transform.rotation = Quaternion.AngleAxis(sunLightRotation, Vector3.right);
    }

    private void UpdateLightSettings(){
        float dotProduct = Vector3.Dot(sunSource.transform.forward, Vector3.down);
        sunSource.intensity = Mathf.Lerp(0, maxSunLightIntensity, ambientCurve.Evaluate(dotProduct));
        moonLigth.intensity = Mathf.Lerp(maxMoonLightIntensity, 0, ambientCurve.Evaluate(dotProduct));
        RenderSettings.ambientLight = Color.Lerp(nightTimeAmbientColor, dayTimeAmbientColor, ambientCurve.Evaluate(dotProduct));
    }

    private TimeSpan CalculateTimeDiff(TimeSpan fromTime, TimeSpan toTime){
        TimeSpan diff = toTime - fromTime;
        if(diff.TotalSeconds < 0){
            diff += TimeSpan.FromHours(24);
        }
        return diff;
    }
}

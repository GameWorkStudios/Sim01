using UnityEngine;

public class GlobalTimer : MonoSingleton<GlobalTimer>
{

    float seconds, minutes, hours, days, months, years = 0;

    void Update()
    {
        seconds += Time.deltaTime;

        Slicer(ref seconds, ref minutes);
        Slicer(ref minutes, ref hours);
        Slicer(ref hours, ref days, 24);
        Slicer(ref days, ref months, 30);
        Slicer(ref months, ref years, 12);

        /*
        if((seconds > 1) && (((int)seconds % 60) == 0)){
            minutes++;
            seconds=0;
        }

        if((minutes > 1) && (((int)minutes % 60) == 0)){
            hours++;
            minutes=0;
        }

        if((hours > 1) && (((int)hours % 24) == 0)){
            days++;
            hours = 0;
        }

        if((hours > 1) && (((int)hours % 24) == 0)){
            days++;
            hours = 0;
        }*/
        
        //Debug.Log(hours+"HH : "+minutes+"MM : "+seconds+" ss");        
    }

    private void Slicer(ref float value, ref float upperSegment, int modul = 60){
        if((value > 1) && (((int)value % modul) == 0)){
            upperSegment++;
            value = 0;
        }
    }
}

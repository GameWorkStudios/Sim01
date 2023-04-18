using UnityEngine;
 
public class Genes
{
    public readonly Gender gender;

    public Genes(){
        this.gender = Random.Range(0f,1f) < 0.5f ? Gender.Male : Gender.Female;
    }            

}


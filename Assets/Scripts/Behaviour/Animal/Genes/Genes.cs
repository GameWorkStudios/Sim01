using UnityEngine;

public class Genes
{
    public readonly Gender gender;

    /// <summary>
    /// In this region we can find the gene multipliers for
    /// gene specifications.
    /// </summary>
    #region GeneMultipliers
    private float moveSpeedMultiplier = 1f;
    private float eatSpeedMultiplier = 1f;
    #endregion GeneMultipliers

    #region Getters
    public float MoveSpeedMultiplier{
        get{
            return this.moveSpeedMultiplier;
        }
    }
    public float EatSpeedMultiplier{
        get{
            return this.eatSpeedMultiplier;
        }
    }
    #endregion Getters

    public Genes(){
        this.gender = Random.Range(0f,1f) < 0.5f ? Gender.Male : Gender.Female;
    }

}


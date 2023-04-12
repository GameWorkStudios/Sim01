using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Animal Settings", menuName = "Data Containers/Animal Setting")]
public class AnimalSettings : EntitySettings
{
    #region AnimalSpecs
    [Header("Animal Settings")]
    [SerializeField] private string animalName;
    [SerializeField] private Vorous vorousType;
    [SerializeField] private float sleepPeriod;
    [SerializeField] private float feedingPerios;
    [SerializeField] private float matingPeriods;
    [SerializeField] private float socialPeriods;
    [SerializeField] private float incubationPeriods = 0; // NOTE : what happens if gener male?
    #endregion AnimalSpecs

    #region PhysicallySpecs
    [Header("Animal Physically Settings")] 
    [SerializeField] private float oneTimeJumpDistance;
    [SerializeField] private float moveRadius;
    [SerializeField] private float oneJumpHeight;
    [SerializeField] private float chasingSpeed;
    [SerializeField] private float MovenmentType; // Note : I think this property is obsolote
    #endregion PhysicallySpecs

    public float OneTimeJumpDistance{
        get{
            return this.oneTimeJumpDistance;
        }
    }

    public float MoveRadius{
        get{
            return this.moveRadius;
        }
    }

    public float OneJumpHeight{
        get{
            return this.oneJumpHeight;
        }
    }

}

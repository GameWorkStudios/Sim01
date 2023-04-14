using UnityEngine;

[CreateAssetMenu(fileName = "New Animal Settings", menuName = "Data Containers/Animal Setting")]
public class AnimalSettings : EntitySettings
{
    #region AnimalSpecs
    [Header("Animal Settings")]
    [SerializeField] private string animalName;
    [SerializeField] private Vorous vorousType;
    //--
    [SerializeField] private float tiredPeriod; // Unit: Min
    [SerializeField] private float thirstPeriod; // Unit: Min
    [SerializeField] private float feedingPerios; // Unit: Min
    [SerializeField] private float matingPeriods; // Unit: Min
    [SerializeField] private float socialPeriods; // Unit: Min
    //--
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

    public float TiredPeriod{
        get{
            return this.tiredPeriod;
        }
    }

    public float ThirstPeriod{
        get{
            return this.thirstPeriod;
        }
    }

    public float MatingPeriods{
        get{
            return this.matingPeriods;
        }
    }

    public float FeedingPerios{
        get{
            return this.feedingPerios;
        }
    }

    public Vorous VorousType{
        get{
            return this.vorousType;
        }
    }


}

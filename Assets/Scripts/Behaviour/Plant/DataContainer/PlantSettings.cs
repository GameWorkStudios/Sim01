using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Plant Setting", menuName = "Data Containers/Plant Setting")]
public class PlantSettings : EntitySettings
{
    [Header("Plant Settings")]
    [SerializeField] private float plantSaplingDuration;
    [SerializeField] private float plantGrowingDuration;
    [SerializeField] private int maxConsumableAmount;
    /// <summary>
    /// This variable is representing fruit creation duration for plant.
    /// </summary>
    /// <value></value>
    [SerializeField] private float consumableCreationDuration;
    [SerializeField] private float propagationRadius;
    /// <summary>
    /// This variable is representing populating duration for plant.
    /// In other words, representing fruit drop duration.
    /// </summary>
    /// <value></value>
    [SerializeField] private float propagationDuration;

    [SerializeField] private float consumeHardness;

    public float PlantSaplingDuration{
        get{
            return this.plantSaplingDuration;
        }
    }

    public float PlantyGrowingDuration{
        get{
            return this.plantGrowingDuration;
        }
    }

    public int MaxConsumableAmount{
        get{
            return this.maxConsumableAmount;
        }
    }

    public float ConsumableCreationDuration{
        get{
            return this.consumableCreationDuration;
        }
    }

    public float PropagationRadius{
        get{
            return this.propagationRadius;
        }
    }

    public float PropagationDuration{
        get{
            return this.propagationDuration;
        }
    }

    public float ConsumeHardness{
        get{
            return this.consumeHardness;
        }
    }
}

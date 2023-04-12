using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base of all entities data containers for settings.
/// </summary>
public abstract class EntitySettings : GameAsset
{
    [Header("Entity Settings")]
    [SerializeField] protected float lifeTimeDuration;

    public float LifeTimeDuration{
        get{
            return this.lifeTimeDuration;
        }
    }
}

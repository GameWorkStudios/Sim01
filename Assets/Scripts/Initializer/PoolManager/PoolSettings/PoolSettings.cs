using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pool Settings", menuName = "Object Pooling/New Pool Settings")]
public class PoolSettings : GameAsset
{
    [SerializeField] private string poolTag;
    [SerializeField] private int poolSize;
    [SerializeField] private GameObject prefab;

    public string PoolTag{
        get{
            return this.poolTag;
        }
    }

    public int PoolSize{
        get{
            return this.poolSize;
        }
    }

    public GameObject Prefab{
        get{
            return this.prefab;
        }
    }
}

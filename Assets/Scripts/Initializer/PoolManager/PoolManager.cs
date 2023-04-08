using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoSingleton<PoolManager>
{

    [SerializeField] private PoolSettings[] poolSettings;

    public Dictionary<string, Queue<GameObject>> pools;

    void Start()
    {
        pools = new Dictionary<string, Queue<GameObject>>();
        foreach(PoolSettings poolSetting in poolSettings){
            Queue<GameObject> pool = new Queue<GameObject>();
            for(int i = 0; i < poolSetting.PoolSize; i++){
                GameObject toPoolObject = Instantiate(poolSetting.Prefab);
                toPoolObject.SetActive(false);
                pool.Enqueue(toPoolObject);
            }
            pools.Add(poolSetting.PoolTag,pool);
        }
    }

    public GameObject GetObjectFromPool(string poolTag, bool createIfNeed = false){
        Queue<GameObject> pool = this.pools[poolTag];
        if(pool != null){
            GameObject dePooledObject = pool.Dequeue();            
            IPoolOperation poolOp = dePooledObject.GetComponent<IPoolOperation>();
            poolOp.UnPooled();
            dePooledObject.SetActive(true);
            pool.Enqueue(dePooledObject);
            poolOp.Pooled();
            return dePooledObject;
        }
        return null;
    }

}

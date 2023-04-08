using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// This class is work only start!
/// </summary>
public class Initializer : MonoBehaviour
{

    [Serializable]
    public struct SpawnAreaLimits{
        public float posX;
        public float negX;
        public float posZ;
        public float negZ;        
    }    

    [SerializeField] private SpawnAreaLimits spawnAreaLimits;
    [SerializeField] private Transform groundObject;
    [SerializeField] [Range(10f,100f)] private float groundYOffset = 40f;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] [Range(5f, 25f)] private float spawnDistance = 5f;

    [SerializeField] private GameObject plantPrefab;

    [SerializeField] private int plantStartCount = 20;

    private Vector3 lastLocation;

    void Start()
    {
        SpawnPlants();
    }

    private void SpawnPlants(){
        for(int i = 0; i < plantStartCount; i++){
            Vector3 spawnLocation = CreateRandomSpawnPosition();
            //Instantiate(plantPrefab, spawnLocation, Quaternion.identity);
            GameObject plantObject = PoolManager.Instance.GetObjectFromPool("PlantPool");
            plantObject.transform.position = spawnLocation;
        }
    }

    /// <summary>
    /// TODO : Write!
    /// </summary>
    /// <returns></returns>
    private Vector3 CreateRandomSpawnPosition(){
        float randomX = UnityEngine.Random.Range(this.spawnAreaLimits.posX, this.spawnAreaLimits.negX);
        float randomZ = UnityEngine.Random.Range(this.spawnAreaLimits.posZ, this.spawnAreaLimits.negZ);
        Vector3 tempSpawnLocation = new Vector3(randomX, groundObject.position.y + groundYOffset, randomZ);
        RaycastHit hit;
        if(Physics.Raycast(tempSpawnLocation,Vector3.down,out hit,100f,groundLayerMask)){
            tempSpawnLocation.y = hit.point.y;
        }        

        if(this.lastLocation != null){
            Vector3 comparableTempLocation = tempSpawnLocation;
            comparableTempLocation.y = 0;
            Vector3 comparableLastLocation = lastLocation;
            comparableLastLocation.y = 0;
            if(Vector3.Distance(comparableLastLocation, comparableTempLocation) < spawnDistance){
                tempSpawnLocation = CreateRandomSpawnPosition();
            }
        }
        lastLocation = tempSpawnLocation;
        return tempSpawnLocation;
    }

}

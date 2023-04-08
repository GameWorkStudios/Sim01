using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is responsible for the propagation of plant.
/// </summary>
public class PlantBehaviour : PlantStateMachine, IPoolOperation
{
    [SerializeField] private float nutrient = 0;
    [SerializeField] private float ticknessOfSpawnArea;
    [SerializeField] private LayerMask spawnObjectLayer;
    private int maxConsumableAmount; 
    private float propagationRadius;
    private float propagationDuration;    
    private float elapsedTime = 0;

    private Transform tr;

    protected override void Start()
    {
        base.Start();
        this.tr = transform;
        this.propagationRadius = base.PlantSettings.PropagationRadius;
        this.maxConsumableAmount = base.PlantSettings.MaxConsumableAmount;
        this.propagationDuration = base.PlantSettings.PropagationDuration;
    }

    protected override void Update()
    {
        base.Update();
        this.DropFruitAndCreateSapling();
    }    

    /// <summary>
    /// This function is used for increasing nutrients (or fruits) on plants.
    /// Related states will access this function and increase the nutrient count.
    /// </summary>
    public override void CreateNutrient(){
        if(this.nutrient == 0 || this.nutrient <= this.maxConsumableAmount){
            nutrient++;
        }
    }

    private void DropFruitAndCreateSapling(){
        if(nutrient == 0){
            return;
        }
        if(elapsedTime >= this.propagationDuration){
            this.SpawnSapling();
            elapsedTime = 0;
        }
        elapsedTime+=Time.deltaTime;        
    }

    private void SpawnSapling(){
        Vector3 spawnLocation = RandomCircle(this.tr.position, this.propagationRadius);
        GameObject newSaplingObject = PoolManager.Instance.GetObjectFromPool("PlantPool");
        newSaplingObject.transform.position = spawnLocation;
    }

    /// <summary>
    /// This function is giving Vector3 for spawning new plant.
    /// </summary>
    /// <param name="center">Center of spawn area. For us this is the parent plant.</param>
    /// <param name="radius">Radius of spawn area.</param>
    /// <returns>Calculated spawn location</returns>
    private Vector3 RandomCircle (Vector3 center, float radius){
        float tickness = Random.Range(-ticknessOfSpawnArea,ticknessOfSpawnArea);
         float ang = Random.value * 360;         
         Vector3 pos;
         pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad) + tickness;
         pos.y = center.y;
         pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad) + tickness;
        RaycastHit hit;
        center.y += 50f;
        if(Physics.Raycast(center, Vector3.down, out hit, 100f, spawnObjectLayer)){
            pos.y = hit.point.y - 0.1f;
        }
         return pos;
     }

    public void Pooled()
    {

    }

    public void UnPooled()
    {
    }
}

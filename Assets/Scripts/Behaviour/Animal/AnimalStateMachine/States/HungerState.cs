using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class HungerState : MoverState
{    


    private float interactionRadius;
    private AnimalStateMachine animalStateMachine;
    private Vorous vorous;

    private PlantBehaviour selectedPlantForEat;

    public override void StartState(StateMachine machine)
    {
        Debug.LogWarning("HUNGER STATE");
        base.tr = machine.transform;
        this.animalStateMachine = machine.AnimalStateMachine();
        this.animalStateMachine.SetIdentifier(StateIdentifier.HUNGER);
        this.interactionRadius = this.animalStateMachine.GetAnimalSettings.MoveRadius;
        initializeMoveSettings(
                this.animalStateMachine.GroundLayerMask,
                this.animalStateMachine.GetAnimalSettings.OneTimeJumpDistance,
                this.animalStateMachine.GetAnimalSettings.OneJumpHeight,
                this.animalStateMachine.ObjectVerticalLength,
                this.DestinationReached
            );
        this.vorous = this.animalStateMachine.GetAnimalSettings.VorousType;
        FindNearestFood();
    }

    private void DestinationReached(){
        this.animalStateMachine.ChangeState(new FeedingState(this.selectedPlantForEat));
    }

    public override void UpdateState(StateMachine machine)
    {
        base.UpdateState(machine);
        if(selectedPlantForEat == null || !selectedPlantForEat.gameObject.activeInHierarchy){
            StopMoveOperation();            
            FindNearestFood();
            return;
        }
        if (selectedPlantForEat.gameObject.activeInHierarchy){ // TODO: some times this line is returning null!
            if(Vector3.Distance(this.tr.position, base.targetPosition) < 0.2f){
                this.animalStateMachine.ChangeState(new FeedingState(this.selectedPlantForEat));
            }
        }else{
            // TODO : Test here.
            base.StopMoveOperation();
            this.FindNearestFood();   
        }
    }

    public override void LeaveState(StateMachine machine)
    {
        base.onDestinationReached -= DestinationReached;
        base.StopMoveOperation();
    }

    /// <summary>
    /// Overrided from base class.
    /// </summary>
    /// <param name="groundLayerMask"></param>
    /// <param name="oneTimeJumpDistance"></param>
    /// <param name="jumpHeight"></param>
    /// <param name="objectVerticalLength"></param>
    /// <param name="OnDestinationReached"></param>
    public override void initializeMoveSettings(LayerMask groundLayerMask, float oneTimeJumpDistance, float jumpHeight, float objectVerticalLength, System.Action OnDestinationReached = null)
    {
        base.GroundLayerMask = groundLayerMask;
        base.OneTimeJumpDistance = oneTimeJumpDistance;
        base.JumpHeight = jumpHeight;
        base.ObjectVertialLength = objectVerticalLength;
        base.onDestinationReached += OnDestinationReached;
    }

    /// <summary>
    /// This function is getting all nearby object and detecting nearest and high quality food resource position.
    /// </summary>
    /// <returns></returns>
    private Vector3 FindNearestFood(){            
        Collider[] objectsInRadius = Physics.OverlapSphere(this.tr.position,this.interactionRadius);
        List<GameObject> extractedFoodSources = ExtractObjectForFoodType(objectsInRadius);
        Vector3 foodPosition;
        if(extractedFoodSources.Count != 0){ // if extracted food is not zero then there is a food resource nearby.
             foodPosition = FindBestFoodSourcePosition(extractedFoodSources);
        }else{ // TODO : This part of code may will change in the future. If the extracted food list is empty then we must find direction of far plant area and move the animal there.
            foodPosition = this.tr.position;
            this.animalStateMachine.ChangeState(this.animalStateMachine.idleState);
        }
        base.targetPosition = foodPosition;
        base.StartMoveOperation();
        return foodPosition;
    }

    /// <summary>
    /// This function is responsible for detecting more nutrient carrying plant. 
    /// </summary>
    /// <param name="extractedFoodSources"></param>
    /// <returns></returns>
    private Vector3 FindBestFoodSourcePosition(List<GameObject> extractedFoodSources){
        switch(this.animalStateMachine.GetAnimalSettings.VorousType){
            case Vorous.HERBIVOROUS:
                PlantBehaviour lastPb = null;
                foreach(GameObject extractedFoodSource in extractedFoodSources){
                    PlantBehaviour pb = extractedFoodSource.GetComponent<PlantBehaviour>();
                    if(!pb.IsSapling){
                        if(lastPb == null || pb.Nutrient > lastPb.Nutrient){
                            lastPb = pb;
                        }
                    }
                }           
                selectedPlantForEat = lastPb;
                return lastPb == null ? Vector3.zero : lastPb.transform.position; // TODO : If return null || Vector3.zero what happens ?
            break;
            case Vorous.CARNIVOROUS:
            // TODO : Program here later.
            return Vector3.zero;
            break;
            default:
            return Vector3.zero;
            break;
        }
    }

    /// <summary>
    /// This function is extracting and giving all food resources inside 
    /// the radius on animal depending on animal vorous type.
    /// </summary>
    /// <param name="findList">All Object around the animal containing Collider.</param>
    /// <returns>List<GameObject> list of all food resources.</returns>
    private List<GameObject> ExtractObjectForFoodType(Collider[] findList){
        List<GameObject> extractedFoodSources = new List<GameObject>();
        foreach(Collider obj in findList){
            switch(this.animalStateMachine.GetAnimalSettings.VorousType) 
            {
                case Vorous.CARNIVOROUS: // Hayvan etçil ise
                    if(obj.GetComponent<Entity>().VorousTarget == Vorous.CARNIVOROUS){ // etçile uygun hayvanları extract et
                        extractedFoodSources.Add(obj.gameObject);
                    }   
                break;
                case Vorous.HERBIVOROUS: // Hayvan otçul ise
                    if(obj.GetComponent<Entity>()){
                        if(obj.GetComponent<Entity>().VorousTarget == Vorous.HERBIVOROUS){ // otçula uygun hayvanları extract et
                            extractedFoodSources.Add(obj.gameObject);
                        }
                    }                    
                break;
            }
        }
        return extractedFoodSources;
    }

}


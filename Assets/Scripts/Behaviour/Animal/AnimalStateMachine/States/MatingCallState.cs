using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 

 /// <summary>
 /// In this state, animal move randomly in short radius and call for mating.
 /// </summary>
public class MatingCallState : MoverState
{

    private float interactionRadius;
    private AnimalStateMachine animalStateMachine;
    private float randomPositionRadius;
    private float matingCallRadius;

    private float elapsedTime = 0;
    private float matingCallDuration = 10f;
    private bool callForMate = false;

    public override void StartState(StateMachine machine)
    {
        Debug.LogWarning("MATING CALL STATE");
        base.tr = machine.transform;        
        this.animalStateMachine = machine.AnimalStateMachine();
        this.animalStateMachine.SetIdentifier(StateIdentifier.MATINGCALL);
        this.interactionRadius = this.animalStateMachine.GetAnimalSettings.MoveRadius;
        this.randomPositionRadius = this.animalStateMachine.GetAnimalSettings.MoveRadius/2f;
        this.matingCallRadius = this.animalStateMachine.GetAnimalSettings.MoveRadius * 3f;
        initializeMoveSettings(
            this.animalStateMachine.GroundLayerMask,
            this.animalStateMachine.GetAnimalSettings.OneTimeJumpDistance,
            this.animalStateMachine.GetAnimalSettings.OneJumpHeight,
            this.animalStateMachine.ObjectVerticalLength,
            this.DestinationReached
        );
        FindRandomPosition();
    }

    private void DestinationReached(){
        callForMate = true;
    }

    public override void UpdateState(StateMachine machine)
    {
        base.UpdateState(machine);
        if(Vector3.Distance(this.tr.position, base.targetPosition) < 1f){
            FindRandomPosition();
        }
        if(!callForMate)
            return;
        
        MatingCall();
    }

    public override void LeaveState(StateMachine machine)
    {
    }

    private void MatingCall(){
        Debug.LogWarning("MATING CALL!");
        if(elapsedTime >= matingCallDuration){
            Call();
        }
        elapsedTime += Time.deltaTime;        
    }

    private void Call(){
        Collider[] getObjectsAround = Physics.OverlapSphere(this.tr.position, this.matingCallRadius, LayerMask.GetMask("Animal"));
        foreach(Collider unknownObject in getObjectsAround){
            if(unknownObject.GetComponent<Animal>()){
                // This is an animal!
                if(unknownObject.GetComponent<AnimalBehaviour>().GetAnimalSettings.AnimalType == this.animalStateMachine.GetAnimalSettings.AnimalType){ // Are we same type of animal ?
                    AnimalBehaviour sameTypeAnimal = unknownObject.GetComponent<AnimalBehaviour>();
                    if(sameTypeAnimal.GenderOfAnimal != this.animalStateMachine.GenderOfAnimal){ // Are we opposite gender ?
                        sameTypeAnimal.SendMessage("ResponseToMatingCall", this.animalStateMachine.gameObject);   // TODO : Check for performance!
                    }
                }
            }
        }
        FindRandomPosition();
        callForMate = false;
        elapsedTime = 0;
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

    private Vector3 FindRandomPosition(){
        float xPos = Random.Range(base.tr.position.x - randomPositionRadius, base.tr.position.x + randomPositionRadius);
        float zPos = Random.Range(base.tr.position.z - randomPositionRadius, base.tr.position.z + randomPositionRadius);
        Vector3 tempPosition = new Vector3(xPos, 50f, zPos);
        tempPosition = base.YvalueCorrection(tempPosition);
        if(Vector3.Distance(tempPosition, base.targetPosition) < 1f){
            tempPosition = FindRandomPosition();
        }  
        base.targetPosition = tempPosition;
        base.StartMoveOperation();
        return tempPosition;
    }


}


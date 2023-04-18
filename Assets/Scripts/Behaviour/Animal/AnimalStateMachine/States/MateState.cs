using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class MateState : MoverState
{
    private float interactionRadius;
    private AnimalStateMachine animalStateMachine;
    private AnimalBehaviour selectedPartner = null;

    public override void StartState(StateMachine machine)
    {
        Debug.LogWarning("MATE STATE");
        base.tr = machine.transform;
        this.animalStateMachine = machine.AnimalStateMachine();
        if(this.animalStateMachine.GenderOfAnimal == Gender.Female){
            machine.ChangeState(this.animalStateMachine.matingCallState);
            return;
        }
        this.animalStateMachine.SetIdentifier(StateIdentifier.MATE);
        this.interactionRadius = this.animalStateMachine.GetAnimalSettings.MoveRadius;
        initializeMoveSettings(
            this.animalStateMachine.GroundLayerMask,
            this.animalStateMachine.GetAnimalSettings.OneTimeJumpDistance,
            this.animalStateMachine.GetAnimalSettings.OneJumpHeight,
            this.animalStateMachine.ObjectVerticalLength,
            this.DestinationReached
        );
        FindPartner();        
    }

    private void DestinationReached(){

    }

    public override void UpdateState(StateMachine machine)
    {
        if(selectedPartner == null){
            FindPartner();
            return;
        }
        base.targetPosition = selectedPartner.transform.position;
        base.StartMoveOperation();
        if(Vector3.Distance(this.tr.position, selectedPartner.transform.position) < 1f){
            Debug.Log("SOCIAL INTERACTION!");
        }
    }

    public override void LeaveState(StateMachine machine)
    {
    }

    private void FindPartner(){
        Collider[] objectsInRadius = Physics.OverlapSphere(this.tr.position, this.interactionRadius, LayerMask.GetMask("Animal"));
        Debug.Log("objectsInRadius : " + objectsInRadius.Length);
        if(objectsInRadius.Length > 0){
            AnimalBehaviour selectedPartner = null;
            foreach(Collider objectInRadius in objectsInRadius){
                if(objectInRadius.GetComponent<AnimalBehaviour>()){
                    AnimalBehaviour animal = objectInRadius.GetComponent<AnimalBehaviour>();
                    if(animal.GetAnimalSettings.AnimalType == AnimalTypes.RABBIT && this.animalStateMachine.GenderOfAnimal != animal.GenderOfAnimal){
                        // TODO : Check here gene quality ?
                        // TODO : What about partner's mate progress ?
                        selectedPartner = animal;
                        break;
                    }   
                }
            }                    
        }else{// TODO : This part of code may will change in the future. If the extracted food list is empty then we must find direction of far animals area and move the animal there.

        }
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
}


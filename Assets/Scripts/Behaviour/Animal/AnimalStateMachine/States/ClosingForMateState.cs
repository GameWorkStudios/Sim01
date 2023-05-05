using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
 /// <summary>
 /// This state is using for only male animals.
 /// Male animals when hear a mating call and accept the matingCall 
 /// then it needs to close enough for mating. This state is helping
 /// to the male animal for finding and closing to the female animal.
 /// 
 /// </summary>
public class ClosingForMateState : MoverState
{

    private float moveRadius;

    private AnimalStateMachine animalStateMachine;
    private AnimalBehaviour female;
    private Transform femaleTransform;

    public ClosingForMateState(AnimalBehaviour female, Vector3 matingCallPosition){
        this.female = female;
        this.femaleTransform = female.transform;
        base.targetPosition = matingCallPosition;
    }

    public override void StartState(StateMachine machine)
    {
        Debug.LogWarning("ClosingForMateState STATE");
        base.tr = machine.transform;
        this.animalStateMachine = machine.AnimalStateMachine();
        this.animalStateMachine.SetIdentifier(StateIdentifier.IDLE);
        this.moveRadius = this.animalStateMachine.GetAnimalSettings.MoveRadius;
        initializeMoveSettings(
                this.animalStateMachine.GroundLayerMask,
                this.animalStateMachine.GetAnimalSettings.OneTimeJumpDistance,
                this.animalStateMachine.GetAnimalSettings.OneJumpHeight,
                this.animalStateMachine.ObjectVerticalLength,
                this.DestinationReached
            );
        if(base.targetPosition != null){
            base.StartMoveOperation();
        }
    }

    public override void UpdateState(StateMachine machine)
    {
        base.UpdateState(machine);
        if(Vector3.Distance(base.tr.position, this.femaleTransform.position) < 1f){
                this.female.RequestForMatingCall((AnimalBehaviour)animalStateMachine);
                return;
        }
        if(Vector3.Distance(base.tr.position, base.targetPosition) < 1f){
            if(Vector3.Distance(base.tr.position, this.femaleTransform.position) > 1f){
                animalStateMachine.ChangeState(animalStateMachine.mateState);
            }
        }
    }

    public override void LeaveState(StateMachine machine)
    {
        base.onDestinationReached -= DestinationReached;
        base.StopMoveOperation();
    }

    public override void initializeMoveSettings(LayerMask groundLayerMask, float oneTimeJumpDistance, float jumpHeight, float objectVerticalLength, Action OnDestinationReached)
    {
        base.GroundLayerMask = groundLayerMask;
        base.OneTimeJumpDistance = oneTimeJumpDistance;
        base.JumpHeight = jumpHeight;
        base.ObjectVertialLength = objectVerticalLength;
        base.onDestinationReached += OnDestinationReached;
    }

    private void DestinationReached(){
        //FindRandomPosition();
        if (Vector3.Distance(base.tr.position, base.targetPosition) < 0.2f){
            if(Vector3.Distance(base.tr.position, this.femaleTransform.position) < 0.2f){
                this.female.RequestForMatingCall((AnimalBehaviour)animalStateMachine);
            }else{
                animalStateMachine.ChangeState(animalStateMachine.mateState);
            }
        }
    }
}


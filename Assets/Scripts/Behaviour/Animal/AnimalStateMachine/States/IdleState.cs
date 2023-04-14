using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
 /// <summary>
 /// If an animal in IdleState then it's moving randomly.
 /// </summary>
public class IdleState : MoverState
{
    private float randomPositionRadius;
    private AnimalStateMachine animalStateMachine;

    public override void StartState(StateMachine machine)
    {        
        Debug.LogWarning("IDLE STATE");
        base.tr = machine.transform;
        this.animalStateMachine = machine.AnimalStateMachine();
        this.animalStateMachine.SetIdentifier(StateIdentifier.IDLE);
        this.randomPositionRadius = this.animalStateMachine.GetAnimalSettings.MoveRadius;
        initializeMoveSettings(
                this.animalStateMachine.GroundLayerMask,
                this.animalStateMachine.GetAnimalSettings.OneTimeJumpDistance,
                this.animalStateMachine.GetAnimalSettings.OneJumpHeight,
                this.animalStateMachine.ObjectVerticalLength,
                this.DestinationReached
            );
        FindRandomPosition();        
    }

    public override void UpdateState(StateMachine machine)
    {
        base.UpdateState(machine);
        if(Vector3.Distance(this.tr.position, base.targetPosition) < 1f){
            FindRandomPosition();
        }
    }

    public override void LeaveState(StateMachine machine)
    {
        base.onDestinationReached -= DestinationReached;
        base.StopMoveOperation();
    }

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

    private void DestinationReached(){
        FindRandomPosition();
    }


}


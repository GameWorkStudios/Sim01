using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class ThirstState : MoverState
{

    private float interactionRadius;
    private AnimalStateMachine animalStateMachine;

    private GameObject[] waterSources;

    private Vector3 selectedWaterSource;    

    public override void StartState(StateMachine machine)
    {
        Debug.LogWarning("THIRST STATE");
        base.tr = machine.transform;
        this.animalStateMachine = machine.AnimalStateMachine();
        this.animalStateMachine.SetIdentifier(StateIdentifier.THIRST);
        this.interactionRadius = this.animalStateMachine.GetAnimalSettings.MoveRadius;
        initializeMoveSettings(
                this.animalStateMachine.GroundLayerMask,
                this.animalStateMachine.GetAnimalSettings.OneTimeJumpDistance,
                this.animalStateMachine.GetAnimalSettings.OneJumpHeight,
                this.animalStateMachine.ObjectVerticalLength,
                this.DestinationReached
            );
        GetAllWaterSourcesToMemory();
        FindNearestWaterSource();
    }

    private void DestinationReached(){
        this.animalStateMachine.ChangeState(this.animalStateMachine.drinkingState);
    }

    public override void UpdateState(StateMachine machine)
    {
        base.UpdateState(machine);
        if(Vector3.Distance(this.tr.position , base.targetPosition) < 0.2f){
            this.animalStateMachine.ChangeState(this.animalStateMachine.drinkingState);
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
    /// this will be change when the water system change!
    /// </summary>
    private void GetAllWaterSourcesToMemory(){
        this.waterSources = GameObject.FindGameObjectsWithTag("WaterSource");
    }

    private void FindNearestWaterSource(){
        Transform closestPositionTransform = null;
        foreach(GameObject waterSource in this.waterSources){
            if(closestPositionTransform == null){
                closestPositionTransform = waterSource.transform;
            }else{
                if(Vector3.Distance(this.tr.position, waterSource.transform.position) < Vector3.Distance(this.tr.position, closestPositionTransform.position)){
                    closestPositionTransform = waterSource.transform;
                }
            }
        }
        base.targetPosition = closestPositionTransform.position;
        this.selectedWaterSource = closestPositionTransform.position;
        base.StartMoveOperation();
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class FeedingState : State
{

    private AnimalStateMachine animalStateMachine;
    private PlantBehaviour toEatPlant;
    private float consumeHardness;
    private float elapsedTime = 0;

    public FeedingState(PlantBehaviour toEatPlant){
        this.toEatPlant = toEatPlant;
        this.consumeHardness = toEatPlant.ConsumeHardness;
    }

    public override void StartState(StateMachine machine)
    {
        Debug.LogWarning("FEEDING STATE!");
        this.animalStateMachine = machine.AnimalStateMachine();
        this.animalStateMachine.SetIdentifier(StateIdentifier.EATING);
        this.toEatPlant.StartConsume();
    }

    public override void UpdateState(StateMachine machine)
    {
        EatNutrient();
    }

    public override void LeaveState(StateMachine machine)
    {
        
    }

    private void EatNutrient(){
        if(this.toEatPlant.Nutrient > 0){
            if(elapsedTime >= consumeHardness * 1){
                this.toEatPlant.Consume();
                this.animalStateMachine.Feed();            
                elapsedTime = 0;
            }
            elapsedTime += Time.deltaTime;
        }else{

        }
    }
}


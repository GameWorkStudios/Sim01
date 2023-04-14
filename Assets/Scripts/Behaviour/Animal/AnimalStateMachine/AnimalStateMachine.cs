using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalStateMachine : Animal
{
    [SerializeField] private AnimalSettings animalSettings;
    [SerializeField] private LayerMask groundLayerMask;

    private float objectVerticalLength;

    #region States
    public IdleState idleState          = new IdleState();
    public ThirstState thirstState      = new ThirstState();
    public DrinkingState drinkingState = new DrinkingState();
    public HungerState hungerState      = new HungerState();
    public FeedingState feedingState; // -> this state has a constructor.
    public ChasingState chasingState    = new ChasingState();
    public MateState mateState          = new MateState();
    public MatingState matingState      = new MatingState();
    #endregion States

    protected Transform tr;
    protected StateIdentifier stateIdentifier;

    #region Getters
    public Transform Tr{
        get{
            return this.tr;
        }
    }
    public AnimalSettings GetAnimalSettings{
        get{
            return this.animalSettings;
        }
    }
    public LayerMask GroundLayerMask{
        get{
            return this.groundLayerMask;
        }
    }
    public float ObjectVerticalLength{
        get{
            return this.objectVerticalLength;
        }
    }
    #endregion Getters

    protected virtual void Start()
    {
        this.tr = transform;
        currentState = idleState;
        currentState.StartState(this);
        this.objectVerticalLength = GetComponent<Collider>().bounds.size.y;
    }

    protected virtual void Update()
    {
        currentState.UpdateState(this);
    }

    public override void ChangeState(State state)
    {
        if(base.currentState != null){
            base.currentState.LeaveState(this);
        }
        currentState = state;
        currentState.StartState(this);
    }

    public void SetIdentifier(StateIdentifier stateIdentifier){
        this.stateIdentifier = stateIdentifier;
    }

    public override void Die()
    {

    }

    #region PolyMorphismFunctions
    public override void Drink()
    {
        Debug.Log("This function must override!");
    }

    public override void Feed()
    {
        Debug.Log("This function must override!");
    }

    public override void Sleep()
    {
        Debug.Log("This function must override!");
    }

    public override void Mate()
    {
        Debug.Log("This function must override!");
    }

    protected override void Chase()
    {
        Debug.Log("This function must override!");
    }

    protected override void Escape()
    {
        Debug.Log("This function must override!");
    }
    #endregion PolyMorphismFunctions
}

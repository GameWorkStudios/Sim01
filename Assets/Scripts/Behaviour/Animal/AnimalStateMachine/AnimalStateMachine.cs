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
    public DrinkingState drinkingState  = new DrinkingState();
    public HungerState hungerState      = new HungerState();
    public FeedingState feedingState    = new FeedingState();
    public ChasingState chasingState    = new ChasingState();
    public MateState mateState          = new MateState();
    public MatingState matingState      = new MatingState();
    #endregion States

    protected Transform tr;

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

    public override void Die()
    {

    }

    protected override void Sleep()
    {
    }

    protected override void Feed()
    {
    }

    protected override void FindFood()
    {
    }

    protected override void Mate()
    {
    }

    protected override void FindPartner()
    {
    }

    protected override void Chase()
    {
    }

    protected override void Escape()
    {
    }
}

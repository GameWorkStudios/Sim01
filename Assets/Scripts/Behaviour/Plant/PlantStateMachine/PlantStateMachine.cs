using UnityEngine;

public class PlantStateMachine : Entity
{

    [SerializeField] private PlantSettings  plantSettings;
    [SerializeField] private GameObject     saplingObject;
    [SerializeField] private GameObject     plantObject;

    #region Getters
    public PlantSettings PlantSettings{
        get{
            return this.plantSettings;
        }
    }

    public GameObject PlantObject{
        get{
            return this.plantObject;
        }
    }

    public GameObject SaplingObject{
        get{
            return this.saplingObject;
        }
    }
    #endregion Getters

    #region States
    public SaplingState saplingState    = new SaplingState();
    public GrowingState growingState    = new GrowingState();
    public GrownState grownState        = new GrownState();
    #endregion States

    protected virtual void Start()
    {        
        base.currentState = saplingState;
        base.currentState.StartState(this);
    }

    protected virtual void Update()
    {
        base.currentState.UpdateState(this);
    }

    public override void ChangeState(State state)
    {
        if(base.currentState != null){
            base.currentState.LeaveState(this);
        }
        base.currentState = state;
        base.currentState.StartState(this);
    }

    /// <summary>
    /// This function is must override in child class.
    /// This function increasing nutrient count.
    /// </summary>
    public virtual void CreateNutrient(){}

    public override void Die()
    {
        gameObject.SetActive(false);
    }
}

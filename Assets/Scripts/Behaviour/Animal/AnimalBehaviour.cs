using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalBehaviour : AnimalStateMachine
{

    #region Events
    [Header("Events")]
    [SerializeField] private VoidEvent OnTimerTick;
    [SerializeField] private IntEvent OnAnimalClicked;
    [SerializeField] private AnimalProgressInformationEvent OnAnimalProgressInformationGathered;
    #endregion Events

    #region ProgressVariables
    private float hungerProgress = 0;
    private float thirstProgress = 0;
    private float mateProgress = 0;
    private float tirednessProgress = 0;    
    #endregion ProgressVariables

    private float tirednessMinutes = 0;
    private float hungerMinutes = 0;
    private float thirstMinutes = 0;
    private float mateMinutes = 0;

    private AnimalProgressInformation animalProgressInformation;

    #region ProgressMultiplier
    [Header("Progress Multiplier")]
    [SerializeField] [Range(0.1f,10f)] private float hungerMultiplier = 1f;
    [SerializeField] [Range(0.1f,10f)] private float thirstMultiplier = 1f;
    [SerializeField] [Range(1f,10f)] private float mateMultiplier = 1f;
    [SerializeField] [Range(1f,10f)] private float tirednessMultiplier = 1f;    
    #endregion ProgressMultiplier    

    #region Getters
    protected float HungerProgress{
        get{
            return this.hungerProgress;
        }
    }
    protected float ThirstProgress{
        get{
            return this.thirstProgress;
        }
    }
    protected float MateProgress{
        get{
            return this.mateProgress;
        }
    }
    protected float TirednessProgress{
        get{
            return this.tirednessProgress;
        }
    }
    #endregion Getters

    private float stateChooseTime = 10f; // !! İmportant!
    private float timeAfterLastStateChange = 0;

    private void OnEnable() {
        this.OnTimerTick.AddListener(Tick);
        this.OnAnimalClicked.AddListener(AnimalClicked);
    }

    private void OnDisable() {
        this.OnTimerTick.RemoveListener(Tick);
        this.OnAnimalClicked.RemoveListener(AnimalClicked);
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update() {
        base.Update();
        timeAfterLastStateChange += Time.deltaTime;
        if(timeAfterLastStateChange >= stateChooseTime) {
            ChooseNextState();
            timeAfterLastStateChange = 0;
        }
    }

    private void ChooseNextState(){
        bool switchable = base.stateIdentifier != StateIdentifier.EATING && base.stateIdentifier != StateIdentifier.DRINKING && base.stateIdentifier != StateIdentifier.MATING && base.stateIdentifier != StateIdentifier.CHASING;
        if(switchable){
            if((mateProgress > Random.Range(0.6f, 0.75f)) && (hungerProgress < 0.4f) && (thirstProgress < 0.4f)){
                // Mate!                    
                if(base.stateIdentifier != StateIdentifier.MATE){
                    base.ChangeState(base.mateState);
                }
            }else if(thirstProgress >= hungerProgress){
                if(thirstProgress > Random.Range(0.5f, 0.6f)){
                    // SU İÇ!                    
                    if(base.stateIdentifier != StateIdentifier.THIRST){
                        base.ChangeState(base.thirstState);
                    }
                }                
            }else if(thirstProgress < hungerProgress){
                if(hungerProgress > Random.Range(0.25f, 0.4f)){
                    // Git Yemek YE!
                    if(base.stateIdentifier != StateIdentifier.HUNGER){
                        base.ChangeState(base.hungerState);
                    }                    
                }
            }
        }
    }

    public override void Feed()
    {
        hungerProgress -= 0.1f;
        float rnd = Random.Range(0.15f, 0.25f);
        if(hungerProgress <= rnd || hungerProgress < 0){
            base.ChangeState(base.idleState);
            hungerMinutes = GetAnimalSettings.FeedingPerios * this.hungerProgress;
        }
    }

    public override void Drink()
    {
        thirstProgress -= 0.01f;
        float rnd = Random.Range(0.5f, 0.15f);
        if(thirstProgress <= rnd || thirstProgress < 0){
            base.ChangeState(base.idleState);
            thirstMinutes = GetAnimalSettings.ThirstPeriod * this.thirstProgress;
        }
    }


    /// <summary>
    /// This function is calling from MatingCallState and with sendMessage function. 
    /// Therefore this function only can call from female animals.
    /// if the male animal is not mate state then this function is not working.
    /// else if this function is called then male state is changing to the ClosingForMateState 
    /// with target mateCall position.
    /// TODO : GameObject type of paremeter will change!
    /// </summary>
    /// <param name="toMateFemale">self of female animal.</param>
    public void ResponseToMatingCall(GameObject toMateFemale){
        //if(base.stateIdentifier != StateIdentifier.MATE) return;

        //toMateFemale.SendMessage("AcceptForMatingCall", gameObject); 
        base.ChangeState(new ClosingForMateState(toMateFemale.GetComponent<AnimalBehaviour>(),toMateFemale.transform.position));        
    }

    /// <summary>
    /// This function is called from male animals for sending mating request.
    /// If the mating criterias suitable then female trigger another function!
    /// In addition if the criterias suitable then female state changes to the 
    /// matingState!
    /// </summary>
    public void RequestForMatingCall(AnimalBehaviour male){
        // TODO : Check here Gene classses.
        base.ChangeState(base.matingState);
        male.AcceptForMatingCall(this);
    }

    /// <summary>
    /// This function called from female animals. 
    /// When the this function is called then animals goes to the 
    /// matingState!
    /// </summary>
    public void AcceptForMatingCall(AnimalBehaviour female){
        base.ChangeState(base.matingState);
        female.TransferGenes(base.genes); // Male tranfering genes here.
    }

    /// <summary>
    /// This function is using from male animal to transfer genes itsef to the female animal.
    /// In this function both side are MatingState.
    /// </summary>
    /// <param name="genes"></param>
    public void TransferGenes(Genes maleGenes){
        //Gene Posibilities process.
        Debug.Log("POPULATE !");
    }

    


    #region ProgressCalculations

    private void AnimalClicked(int objectId){
        if(objectId != gameObject.GetHashCode())
            return;
        
        animalProgressInformation = new AnimalProgressInformation(){
            animalName = gameObject.name,
            animalGener = GenderOfAnimal.ToString(),
            currentState = base.stateIdentifier.ToString(),
            hungerProgress = this.hungerProgress,
            thirstProgress = this.thirstProgress,
            mateProgress = this.mateProgress,
            tirednessProgress = this.tirednessProgress
        };
        OnAnimalProgressInformationGathered.Raise(this.animalProgressInformation);
    }

    private void Tick(NullObjectType n){
        tirednessMinutes++;
        mateMinutes++;
        thirstMinutes++;
        hungerMinutes++;    
        mateMinutes *= mateMultiplier;    
        thirstMinutes *= thirstMultiplier;
        hungerMinutes *= hungerMultiplier;
        CalculateTirednessProgress();
        CalculateThirstProgress();
        CalculateMateProgress();
        CalculateHungerProgress();
    }

    private void CalculateTirednessProgress(){
        this.tirednessProgress = (tirednessMinutes/GetAnimalSettings.TiredPeriod);
        if(this.tirednessProgress >= 1f){
            this.tirednessProgress = 0;
            this.tirednessMinutes = 0;
        }
    }

    private void CalculateThirstProgress(){
        this.thirstProgress = (thirstMinutes/GetAnimalSettings.ThirstPeriod);
        if(this.thirstProgress >= 1f){
            this.thirstProgress = 0;
            this.thirstMinutes = 0;
        }
    }

    private void CalculateMateProgress(){
        this.mateProgress = (mateMinutes/GetAnimalSettings.MatingPeriods);
        if(this.mateProgress >= 1f){
            this.mateProgress = 1f;
            //this.mateMinutes = 0;
        }
    }

    private void CalculateHungerProgress(){
        if(base.stateIdentifier == StateIdentifier.EATING) return;        
        this.hungerProgress = (hungerMinutes/GetAnimalSettings.FeedingPerios);
        if(this.hungerProgress >= 1f){
            this.hungerProgress = 0;
            this.hungerMinutes = 0;
        }
    }

    private float Percentage(float a, float b){
        return (a/b)*100f;
    }

    #endregion ProgressCalculations

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Animal : Entity
{

    protected Genes genes;

    [SerializeField] private int geenderr = 2; // TODO : we will remove this variable!

    protected virtual void Start(){
        genes = new Genes();
        /*DEBUG!*/
        switch(genes.gender){
            case Gender.Female:
                geenderr = 0;
            break;
            case Gender.Male:
                geenderr = 1;
            break;
        }
        /*DEBUG!*/
    }

    #region Getters
    public Genes GetGenes{
        get{
            return this.genes;
        }
    }

    public Gender GenderOfAnimal{
        get{
            return this.genes.gender;
        }
    }
    #endregion Getters

    public abstract void Sleep();
    public abstract void Drink();
    public abstract void Feed();
    public abstract void Mate();

    protected abstract void Chase();
    protected abstract void Escape();
}

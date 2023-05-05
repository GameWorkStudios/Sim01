using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Animal : Entity
{

    protected Genes genes;

    [SerializeField] private bool useStaticGender = false;
    [SerializeField] private Gender geenderr = Gender.UNDEFINED; // TODO : we will remove this variable!

    protected virtual void Start(){
        genes = new Genes();
        if(useStaticGender)return;
        this.geenderr = genes.gender;
    }

    #region Getters
    public Genes GetGenes{
        get{
            return this.genes;
        }
    }

    public Gender GenderOfAnimal{
        get{
            if(useStaticGender){
                return this.geenderr;
            }
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

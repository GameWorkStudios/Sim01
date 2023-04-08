using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Animal : Entity
{
    #region AnimalSpecs
    private Gender gender;   // NOTE : Can we change this to the interface for populating operations?
    private Vorous vorousType;
    private float sleepPeriod;
    private float feedingPerios;
    private float matingPeriods;
    private float socialPeriods;
    private float incubationPeriods = 0; // NOTE : what happens if gener male?
    #endregion AnimalSpecs

    #region PhysicallySpecs
    private float movenmentSpeed;
    private float chasingSpeed;
    private float MovenmentType;
    #endregion PhysicallySpecs

    protected abstract void Sleep();
    protected abstract void Feed();
    protected abstract void FindFood();
    protected abstract void Mate();
    protected abstract void FindPartner();
    protected abstract void Chase();
    protected abstract void Escape();

}

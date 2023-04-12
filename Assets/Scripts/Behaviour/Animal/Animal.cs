using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Animal : Entity
{
    protected abstract void Sleep();
    protected abstract void Feed();
    protected abstract void FindFood();
    protected abstract void Mate();
    protected abstract void FindPartner();
    protected abstract void Chase();
    protected abstract void Escape();

}

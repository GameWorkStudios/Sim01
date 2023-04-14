using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Animal : Entity
{
    public abstract void Sleep();
    public abstract void Drink();
    public abstract void Feed();
    public abstract void Mate();

    protected abstract void Chase();
    protected abstract void Escape();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animalnteraction : MouseInteractionWithRaycast
{

    [SerializeField] private LayerMask animalMask;
    [SerializeField] private IntEvent OnAnimalClicked;

    void Start()
    {
        base.maskForInteract = animalMask;        
    }

    protected override void InteractedObject(GameObject interactedObject)
    {
        OnAnimalClicked.Raise(interactedObject.GetHashCode());
    }

}

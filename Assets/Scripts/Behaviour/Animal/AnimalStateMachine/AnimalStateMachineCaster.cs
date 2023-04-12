using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnimalStateMachineCaster
{
    public static AnimalStateMachine AnimalStateMachine(this StateMachine machine){
        return (AnimalStateMachine)machine;
    }
}
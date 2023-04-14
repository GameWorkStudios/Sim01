using System;
using UnityEngine;
///<Summary> 
/// TODO : Change the summary text.
/// 
/// </Summary>
///<see cref="GameEvent"/>
[CreateAssetMenu(fileName = "NewVoidEvent", menuName = "GameAssets/GameEvents/VoidEvent")]
public class VoidEvent: GameEvent<NullObjectType>
{
    public override void AddListener(Action<NullObjectType> action)
    {
        base.gameEvent += action;
    }

    public override void RemoveListener(Action<NullObjectType> action)
    {
        base.gameEvent -= action;
    }

    public override void Raise(NullObjectType value)
    {
        base.gameEvent?.Invoke(value);
    }

}

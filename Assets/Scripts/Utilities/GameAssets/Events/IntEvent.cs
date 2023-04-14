using System;
using UnityEngine;
///<Summary> 
/// TODO : Change the summary text.
/// </Summary>
///<see cref="GameEvent"/>
[CreateAssetMenu(fileName = "NewIntEvent", menuName = "GameAssets/GameEvents/IntEvent")]
public class IntEvent: GameEvent<int>
{
    public override void AddListener(Action<int> action)
    {
        base.gameEvent += action;
    }

    public override void RemoveListener(Action<int> action)
    {
        base.gameEvent -= action;
    }

    public override void Raise(int value)
    {
        base.gameEvent?.Invoke(value);
    }

}

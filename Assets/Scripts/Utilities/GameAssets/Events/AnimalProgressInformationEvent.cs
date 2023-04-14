using System;
using UnityEngine;
///<Summary> 
/// TODO : Change the summary text.
/// </Summary>
///<see cref="GameEvent"/>
[CreateAssetMenu(fileName = "NewAnimalProgressInformationEvent", menuName = "GameAssets/GameEvents/AnimalProgressInformationEvent")]
public class AnimalProgressInformationEvent: GameEvent<AnimalProgressInformation>
{
    public override void AddListener(Action<AnimalProgressInformation> action)
    {
        base.gameEvent += action;
    }

    public override void RemoveListener(Action<AnimalProgressInformation> action)
    {
        base.gameEvent -= action;
    }

    public override void Raise(AnimalProgressInformation value)
    {
        base.gameEvent?.Invoke(value);
    }

}

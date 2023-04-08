/// <summary>
/// Machine Caster
/// </summary>
public static class PlantStateMachineCaster
{
    public static PlantStateMachine PlantStateMachine(this StateMachine machine){
        return (PlantStateMachine)machine;
    }
}


using UnityEngine;

public abstract class Entity : StateMachine
{
    [SerializeField] private Vorous vorousTarget; // NOTE : Beni otçullar yiyebilir, beni etçiller yiyebilir.

    public Vorous VorousTarget{
        get{
            return this.vorousTarget;
        }
    }

    public abstract void Die(); 
}

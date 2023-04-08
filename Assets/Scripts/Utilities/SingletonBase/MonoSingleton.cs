using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T: MonoSingleton<T>
{
    public static volatile T instance = null;
    
    public static T Instance {
        get{
            if (instance == null){
                instance = new GameObject(System.Guid.NewGuid().ToString()).AddComponent<T>();
            }
            return instance;
        }
    }

    protected virtual void Awake() {
        instance = this as T;
    }
}
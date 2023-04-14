
#nullable enable
///<Summary> 
/// This is the base of all GameEvent Assets. 
/// Game events are using for Observable Pattern. 
/// This classes are problematic actualy. Because 
/// we must seperate because of T type generics.  
/// </Summary>
///<see cref="GameAsset"/>
public abstract class GameEvent<T> : GameAsset {
        protected System.Action<T>? gameEvent; 
        public abstract void AddListener(System.Action<T>? action);
        public abstract void RemoveListener(System.Action<T>? action);
        public abstract void Raise(T? value);
 }

/// <summary>
/// This class is just an token for void (or null) events.
/// For example, if you want to create an event without parameter, 
/// then you can use this class for null. 
/// This class is inherited from object because Object can carry null value.
/// </summary>
 public class NullObjectType : System.Object{}
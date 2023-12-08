
using UnityEngine;
using UnityEngine.Events;
public class EventManager {


    public static event UnityAction onGoalReached;
    public static void OnGoalReached() => onGoalReached?.Invoke();
    public static event UnityAction onRestartLevel;
    public static void OnRestartLevel() => onRestartLevel?.Invoke();
    public static event UnityAction onPlayerDeath;
    public static void OnPlayerDeath() => onPlayerDeath?.Invoke();
    
    
}


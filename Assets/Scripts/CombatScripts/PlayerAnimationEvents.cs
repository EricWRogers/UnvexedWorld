using UnityEngine;
using UnityEngine.Events;

public class PlayerAnimationEvents : MonoBehaviour
{
    /// <summary>
    /// Attack prefab spawns events
    /// </summary>
    public UnityEvent<int> spawnMeleeEvent;
    
    public UnityEvent<int> spawnRangeEvent;
    
    public UnityEvent<int> spawnSuperEvent;
    
    /// <summary>
    /// event for resetting energy
    /// </summary>
    public UnityEvent resetEnergyEvent;

    /// <summary>
    /// events for locking and unlocking the player
    /// </summary>
    public UnityEvent cancelLockupEvent;
    public UnityEvent lockupEvent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// calling events to be used for calling the instantiate functions
    /// </summary>

    public void Melee(int index)
    {
        spawnMeleeEvent.Invoke(index);
    }
    
    public void Range(int index)
    {
        spawnRangeEvent.Invoke(index);
    }

    public void Super(int index)
    {
        spawnSuperEvent.Invoke(index);
    }

    public void ManaReset()
    {
        resetEnergyEvent.Invoke();
    }

    public void Lock()
    {
        lockupEvent.Invoke();
    }

    public void Unlock()
    {
        cancelLockupEvent.Invoke();
    }
    
}

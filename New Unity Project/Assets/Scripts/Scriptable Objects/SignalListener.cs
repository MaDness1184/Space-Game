using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; // Using Unity's Event System

public class SignalListener : MonoBehaviour
{
    [Tooltip("Event to register with.")]
    public SignalSender signal;

    [Tooltip("Response to invoke when Event is raised.")]
    public UnityEvent signalEvent;

    private void OnEnable() // Create a listener
    {
        signal.RegisterListener(this);
    }

    private void OnDisable() // Destroy a listener
    {
        signal.DeregisterListener(this);
    }

    public void OnSignalRaised()
    {
        signalEvent.Invoke(); // Call the event
    }
}

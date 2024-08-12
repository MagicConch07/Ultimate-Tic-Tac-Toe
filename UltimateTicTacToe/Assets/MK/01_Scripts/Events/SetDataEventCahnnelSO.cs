using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "SO/Event/SetDataEvent")]
public class SetDataEventCahnnelSO : ScriptableObject
{
    public UnityEvent<DataEvent> OnRaiseEvent;
    public void RaiseEvent(DataEvent evt)
    {
        OnRaiseEvent?.Invoke(evt);
    }
}
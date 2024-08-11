using UnityEngine;
using UnityEngine.Events;

namespace Mk_JWT
{
    [CreateAssetMenu(menuName = "SO/Event/NetworkEvent")]
    public class NetworkEventChannelSO : ScriptableObject
    {
        public UnityEvent<NetworkEvent> OnRaiseEvent;

        public void RaiseEvent(NetworkEvent evt)
        {
            OnRaiseEvent?.Invoke(evt);
        }
    }
}

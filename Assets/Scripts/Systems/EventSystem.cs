using System;
using UnityEngine;

namespace XPACE {
    public enum EventType {
        PlaceStructure
    }
    public class EventSystem : MonoBehaviour {
        public static EventSystem instance;

        public event Action onPlayerJoin;        

        private void Start() {
            if(instance == null)
                instance = this;
        }

        public void OnPlayerJoin() {
            onPlayerJoin?.Invoke();
        }

        public void RegisterEvent(EventType eventType) {
            print("EventSystem just registered an event: " + eventType); 
        }

    }
}
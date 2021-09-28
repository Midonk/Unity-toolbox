using UnityEngine;

namespace ScriptableVariables
{
    public interface IGameEventListener
    {
        void OnEventRaised();
    }
}
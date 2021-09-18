using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DebugMenu
{
    public class DebugMenuSample : MonoBehaviour
    {
        [SerializeField]
        private string message;

        [SerializeField]
        private bool _debugMode;

        private void OnGUI() 
        {
            if(!_debugMode) return;
            if(GUILayout.Button("Invoke"))
            {
                DebugAttributeRegistry.Initialize();    
                DebugAttributeRegistry.InvokeMethod("blabla/machin"); 
            }   
        }

        [DebugMenu("Game/Others", true)]
        public void DebugMenuTestingMethod()
        {
            Debug.Log(message);
        }
        
        [DebugMenu("Player/Stats/Yei")]
        public void DebugMenuTestingMethod2()
        {
            Debug.Log(message);
        }
        
        [DebugMenu("Player/Stats/Stats", true)]
        public void DebugMenuTestingMethod3()
        {
            Debug.Log(message);
        }
        [DebugMenu("Player/Stats/Much/Machin")]
        public void DebugMenuTestingMethod4()
        {
            Debug.Log(message);
        }
        [DebugMenu("Player/Stats/Much/Bidule")]
        public void DebugMenuTestingMethod5()
        {
            Debug.Log(message);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DebugMenu.CustomAttribute.Runtime
{
    public class DebugMenuSample : MonoBehaviour
    {
        private void OnGUI() 
        {
            if(GUILayout.Button("Invoke"))
            {
                DebugAttributeRegistry.ValidateMethods();    
                DebugAttributeRegistry.InvokeMethod("blabla/machin"); 
            }   
        }

        [DebugMenu("blabla/machin")]
        public void DebugMenuTestingMethod()
        {
            Debug.Log("POUET");
        }
    }
}
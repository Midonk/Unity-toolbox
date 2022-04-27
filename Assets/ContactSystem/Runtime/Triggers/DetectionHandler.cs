using UnityEngine;

namespace ContactSystem
{
    [RequireComponent(typeof(Collider))]
    public class DetectionHandler : ContactTriggerBase
    {
        private void OnTriggerEnter(Collider other) 
        {
            if(!_filter.Pass(other.gameObject)) return;

            DetectEntrance();
        }
        
        private void OntriggerExit(Collider other) 
        {
            if(!_filter.Pass(other.gameObject)) return;

            DetectExit();
        }
    }
}
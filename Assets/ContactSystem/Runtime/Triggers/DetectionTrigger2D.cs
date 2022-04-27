using UnityEngine;

namespace ContactSystem
{
    [RequireComponent(typeof(Collider2D))]
    public class DetectionTrigger2D : ContactTriggerBase
    {
        private void OnTriggerEnter2D(Collider2D other) 
        {
            if(!_filter.Pass(other.gameObject)) return;

            DetectEntrance();
        }
        
        private void OntriggerExit2D(Collider2D other) 
        {
            if(!_filter.Pass(other.gameObject)) return;

            DetectExit();
        }
    }
}
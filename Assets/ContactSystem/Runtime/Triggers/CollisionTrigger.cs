using UnityEngine;

namespace ContactSystem
{
    [RequireComponent(typeof(Collider))]
    public class CollisionTrigger : ContactTriggerBase
    {
        #region Unity API

        private void OnCollisionEnter(Collision other) 
        {
            if(!_filter.Pass(other.gameObject)) return;

            DetectEntrance();
        }

        private void OnCollisionExit(Collision other) 
        {
            if(!_filter.Pass(other.gameObject)) return;

            DetectExit();
        }

        #endregion
    }
}
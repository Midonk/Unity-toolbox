using UnityEngine;

namespace ContactSystem
{
    [RequireComponent(typeof(Collider2D))]
    public class CollisionTrigger2D : ContactTriggerBase
    {
        #region Unity API

        private void OnCollisionEnter2D(Collision2D other) 
        {
            if(!_filter.Pass(other.gameObject)) return;

            DetectEntrance();
        }

        private void OnCollisionExit2D(Collision2D other) 
        {
            if(!_filter.Pass(other.gameObject)) return;

            DetectExit();
        }

        #endregion
    }
}
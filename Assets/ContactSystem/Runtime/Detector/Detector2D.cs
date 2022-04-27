using UnityEngine;

namespace ContactSystem
{
    [RequireComponent(typeof(Collider2D))]
    public class Detector2D : DetectorBase
    {
        #region Unity API

        private void OnTriggerEnter2D(Collider2D other) 
        {
            var gameObject = other.gameObject;
            if(!_filter.Pass(gameObject)) return;

            TryAdd(gameObject);    
        }
        
        private void OnTriggerExit2D(Collider2D other) 
        {
            var gameObject = other.gameObject;
            if(!_filter.Pass(gameObject)) return;

            TryRemove(gameObject);    
        }

        #endregion
    }
}
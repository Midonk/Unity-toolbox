using UnityEngine;

namespace ContactSystem
{
    public abstract class ContactBase : MonoBehaviour
    {   
        #region Exposed

        [SerializeField]
        protected FilterType _filterType;

        [SerializeField]
        protected string[] _contactTags;
        
        [SerializeField]
        protected LayerMask _contactLayer;
             
        #endregion


        #region Utils

        /// <summary>
        ///     Determines if an element will trigger the behaviour
        /// </summary>
        /// <param name="element">Element to be filtered</param>
        protected bool PassFilter(GameObject element)
        {
            if (_filterType == FilterType.Tag) 
            return FilterTag(element);
            

            else if(_filterType == FilterType.Layer)
            return FilterLayer(element);

            else
            {
                if(FilterLayer(element) && FilterTag(element)) return true;
            }

            return false;
        }

        private bool FilterTag(GameObject element)
        {
            foreach (var tag in _contactTags)
            {
                if(element.CompareTag(tag)) return true;
            }

            return false;
        }

        private bool FilterLayer(GameObject element)
        {
            if(_contactLayer == (_contactLayer | 1 << element.gameObject.layer)) return true;

            else return false;
        }
             
        #endregion


        protected enum FilterType
        {
            Both,
            Tag,
            Layer
        }
    }
}
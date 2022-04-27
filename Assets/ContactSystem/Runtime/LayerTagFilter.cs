using UnityEngine;
using TF.Utils;

namespace ContactSystem
{
    [System.Serializable]
    public class LayerTagFilter
    {
        #region Exposed

        [SerializeField] private FilterType Type;
        [SerializeField] private TagMask Tags;
        [SerializeField] private LayerMask Layers;
            
        #endregion


        #region Main

        /// <summary>
        ///     Determines if an element pass through the filter
        /// </summary>
        /// <param name="element">Element to be filtered</param>
        public bool Pass(GameObject element) => Type switch
        {
            FilterType.None  => true,
            FilterType.Tag   => FilterTag(element),
            FilterType.Layer => FilterLayer(element),
            FilterType.Both  => FilterLayer(element) && FilterTag(element),
            _                => throw new System.NotImplementedException()
        };
            
        #endregion


        #region Plumbery

        private bool FilterTag(GameObject element)
        {
            var tags = Tags.SelectedTags;
            for (int i = 0; i < tags.Length; i++)
            {
                if(element.CompareTag(tags[i])) return true;
            }

            return false;
        }

        private bool FilterLayer(GameObject element)
        {
            if(Layers == (Layers | 1 << element.gameObject.layer)) return true;

            else return false;
        }
            
        #endregion

     
        public enum FilterType
        {
            None,
            Tag,
            Layer,
            Both,
        }
    }
}
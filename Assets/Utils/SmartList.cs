using UnityEngine;
using System.Collections.Generic;

namespace Utils
{
    [System.Serializable]
    public class SmartList<T> : List<T>
    {
        public SmartList()
        {
            _list = new List<T>();
        }

        public new void Add(T element)
        {
            if(_list.Contains(element)) return;

            _list.Add(element);
        }

        public new void Remove(T element)
        {
            if(!_list.Contains(element)) return;

            _list.Remove(element);
        }

        [SerializeField] private List<T> _list;
    }
}
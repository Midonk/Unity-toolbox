using System.Collections.Generic;

namespace Utils
{
    public class SmartList<T>
    {
        public SmartList()
        {
            _list = new List<T>();
        }

        public T this[int index] 
        {
            get => _list[index]; 
            set => _list[index] = value; 
        }

        public int Count => _list.Count;

        public void Add(T element)
        {
            if(_list.Contains(element)) return;

            _list.Add(element);
        }

        public void Remove(T element)
        {
            if(!_list.Contains(element)) return;

            _list.Remove(element);
        }

        private List<T> _list;
    }
}
using System.Collections.Generic;
using UnityEngine;

namespace PoolSystem
{
    public class Pool<T> where T : PooldItem
	{
		public Pool(T model) : this(model, 0){}
		public Pool(T model, int basePopulation)
		{
			_availables = new Queue<T>();
			_model = model;
			Populate(basePopulation);
		}


		#region Main

		public void Populate(int itemCount)
		{
			for (int i = 0; i < itemCount; i++)
			{
				var newItem = GameObject.Instantiate(_model);
				newItem.gameObject.SetActive(false);
				newItem.onDestroy += (self) => _availables.Enqueue(self as T);
				_availables.Enqueue(newItem);
			}
		}

		public virtual T TryInstantiate(Vector3 position, Quaternion rotation)
		{
			if(_availables.Count == 0) return null;
			
			var item = _availables.Dequeue();
			var itemTransform = item.transform;
			itemTransform.position = position;
			itemTransform.rotation = rotation;

			return item;
		}
			
		#endregion


		#region Private Fields

		protected Queue<T> _availables;
		private T _model;
			
		#endregion
	}

    public class PooldItem : MonoBehaviour
    {
		public event System.Action<PooldItem> onDestroy;

		protected void ReturnToPool()
		{
			gameObject.SetActive(false);
			onDestroy?.Invoke(this);
		}
    }
}
using UnityEngine;

namespace PoolSystem
{
    public class DynamicPool<T> : Pool<T> where T : PooldItem
    {
        public DynamicPool(T model) : base(model){}
        public DynamicPool(T model, int basePopulation) : base(model, basePopulation){}

        public override T TryInstantiate(Vector3 position, Quaternion rotation)
		{
			if(_availables.Count == 0)
            {
                Populate(1);
            }
			
			var item = _availables.Dequeue();
			var itemTransform = item.transform;
			itemTransform.position = position;
			itemTransform.rotation = rotation;

			return item;
		}
    }
}
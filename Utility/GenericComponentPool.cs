using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityUtilities
{

    /// <summary>
    /// Generic wrapper for a GameObject that has a component of the given type.
    /// 
    /// Avoids a common GetComponent call after a GameObject is reused from the pool.
    /// 
    /// GetComponent will be called once when a new object is added to the pool.
    /// </summary>
    public class ComponentWrapper<T>
    {
        // The fields are lower case to match the lower case gameObject field in MonoBehaviours
        public T component;
        public GameObject gameObject;
    }

    /// <summary>
    /// A generic pool for GameObjects with the given MonoBehaviour generic type T.
    /// </summary>
    public class GenericComponentPool<T> : MonoBehaviour
    {
        public GameObject PooledObjectPrefab;
        public bool WillGrow;

        [HideInInspector]
        public List<ComponentWrapper<T>> Pool;

        public GameObject Container;

        protected virtual void Awake()
        {
            Pool = new List<ComponentWrapper<T>>();
        }

        /// <summary>
        /// Get a GameObject from the pool. Reuses an inactive object or instantiates a new one.
        /// </summary>
        /// <returns>
        /// Returns a wrapper object containing the GameObject and the component reference.
        /// 
        /// Returns `null` if `WillGrow` is false and there are no inactive objects in the pool.
        /// </returns>
        public ComponentWrapper<T> GetPooledObject()
        {
            for (var i = 0; i < Pool.Count; i++)
            {
                var pooledObject = Pool[i];

                if (pooledObject == null)
                {
                    var wrapper = InstantiateNew();
                    Pool[i] = wrapper;
                    return wrapper;
                }

                if (!pooledObject.gameObject.activeInHierarchy)
                {
                    pooledObject.gameObject.SetActive(true);
                    return pooledObject;
                }
            }

            if (WillGrow)
            {
                var wrapper = InstantiateNew();
                Pool.Add(wrapper);
                return wrapper;
            }

            return null;
        }

        private ComponentWrapper<T> InstantiateNew()
        {
            GameObject obj;
            if (Container != null)
            {
                obj = Instantiate(PooledObjectPrefab, Container.transform);
            }
            else
            {
                obj = Instantiate(PooledObjectPrefab);
            }

            var wrapper = new ComponentWrapper<T>
            {
                component = obj.GetComponent<T>(),
                gameObject = obj
            };

            return wrapper;
        }
    }
}

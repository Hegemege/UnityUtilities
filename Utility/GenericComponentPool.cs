using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generic wrapper for a GameObject that has a component of the given type.
/// Avoids a common GetComponent<T> call after a GameObject is reused from the pool.
/// GetComponent<T> will be called once when a new object is added to the pool.
/// </summary>
/// <typeparam name="T"></typeparam>
public class ComponentWrapper<T>
{
    // The fields are lower case to match the lower case gameObject field in MonoBehaviours
    public T component;
    public GameObject gameObject;
}

/// <summary>
/// A generic pool for GameObjects with the given MonoBehaviour generic type T.
/// </summary>
/// <typeparam name="T"></typeparam>
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
    /// Returns an instance of the prefab the pool is used to handle.
    /// 
    /// Returns `null` if `WillGrow` is false and there are no inactive objects in the pool.
    /// </summary>
    /// <returns></returns>
    public ComponentWrapper<T> GetPooledObject()
    {
        for (int i = 0; i < Pool.Count; i++)
        {
            var pooledObject = Pool[i];

            if (pooledObject == null)
            {
                ComponentWrapper<T> wrapper = InstantiateNew();
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
            ComponentWrapper<T> comp = InstantiateNew();
            Pool.Add(comp);
            return comp;
        }

        return null;
    }

    private ComponentWrapper<T> InstantiateNew()
    {
        var obj = (GameObject) Instantiate(PooledObjectPrefab);

        if (Container != null)
        {
            obj.transform.parent = Container.transform;
        }

        var wrapper = new ComponentWrapper<T>();
        wrapper.component = obj.GetComponent<T>();
        wrapper.gameObject = obj;

        return wrapper;
    }
}

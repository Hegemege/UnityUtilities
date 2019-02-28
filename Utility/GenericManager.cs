using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericManager<T> : MonoBehaviour
{
    // Singleton setup
    public static T Instance { get; protected set; }

    protected virtual void InitializeSingleton(T parent)
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        Instance = parent;
    }
}

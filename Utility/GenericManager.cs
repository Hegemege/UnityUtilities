using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericManager : MonoBehaviour
{
    // Singleton setup
    public static GenericManager Instance { get; private set; }

    protected virtual void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Init();
    }

    protected abstract void Init();
}

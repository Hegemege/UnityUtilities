using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityUtilities
{
    public abstract class GenericManager<T> : MonoBehaviour
    {
        // Singleton setup
        public static T Instance { get; protected set; }

        protected virtual bool InitializeSingleton(T parent)
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return false;
            }

            DontDestroyOnLoad(gameObject);
            Instance = parent;

            return true;
        }
    }
}

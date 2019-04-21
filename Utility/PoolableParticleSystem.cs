using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityUtilities
{
    public class PoolableParticleSystem : MonoBehaviour
    {
        private ParticleSystem _ps;

        void Awake()
        {
            _ps = GetComponentInChildren<ParticleSystem>();
        }

        void Update()
        {
            if (!_ps.IsAlive())
            {
                _ps.Clear();
                gameObject.SetActive(false);
                return;
            }

            if (!_ps.isPlaying)
            {
                _ps.Play();
            }
        }
    }
}

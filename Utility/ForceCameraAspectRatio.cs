using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnityUtilities
{
    [ExecuteAlways]
    public class ForceCameraAspectRatio : MonoBehaviour
    {
        [Header("Aspect ratio")]
        public float Width;
        public float Height;

        private Camera _cameraRef;
        private float _setWidth;
        private float _setHeight;
        private float _screenWidth;
        private float _screenHeight;

        void Awake()
        {
            _cameraRef = GetComponent<Camera>();
        }

        void Update()
        {
            // Early exits
            if (!_cameraRef) return;
            if (_setWidth == Width && _setHeight == Height && _screenWidth == (float) Screen.width && _screenHeight == (float) Screen.height) return;

            _setWidth = Width;
            _setHeight = Height;
            _screenWidth = (float) Screen.width;
            _screenHeight = (float) Screen.height;

            // Force camera aspect ratio to the given ratio
            var targetAspect = Width / Height;
            var screenAspect = _screenWidth / _screenHeight;

            var scaleHeight = screenAspect / targetAspect;

            // Letterbox
            if (scaleHeight < 1.0f)
            {
                var rect = _cameraRef.rect;

                rect.width = 1.0f;
                rect.height = scaleHeight;
                rect.x = 0;
                rect.y = (1.0f - scaleHeight) / 2.0f;

                _cameraRef.rect = rect;
            }
            // Pillarbox
            else
            {
                var scalewidth = 1.0f / scaleHeight;

                var rect = _cameraRef.rect;

                rect.width = scalewidth;
                rect.height = 1.0f;
                rect.x = (1.0f - scalewidth) / 2.0f;
                rect.y = 0;

                _cameraRef.rect = rect;
            }
        }
    }
}

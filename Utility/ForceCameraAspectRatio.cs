using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class ForceCameraAspectRatio : MonoBehaviour
{
    [Header("Aspect ratio")]
    public float Width;
    public float Height;

    private Camera _cameraRef;
    private bool _set;
    private float _setWidth;
    private float _setHeight;

    void Awake()
    {
        _cameraRef = GetComponent<Camera>();
    }

    void Update()
    {
        // Early exits
        if (!_cameraRef) return;
        if (_set && _setWidth == Width && _setHeight == Height) return;

        _set = true;
        _setWidth = Width;
        _setHeight = Height;

        // Force camera aspect ratio to the given ratio
        var targetAspect = Width / Height;
        var windowAspect = (float) Screen.width / (float) Screen.height;

        var scaleHeight = windowAspect / targetAspect;

        // Letterbox
        if (scaleHeight < 1.0f)
        {
            var rect = _cameraRef.rect;

            rect.width = 1.0f;
            rect.height = scaleHeight;
            //rect.x = 0;
            //rect.y = (1.0f - scaleHeight) / 2.0f;

            _cameraRef.rect = rect;
        }
        // Pillarbox
        else
        {
            var scalewidth = 1.0f / scaleHeight;

            var rect = _cameraRef.rect;

            rect.width = scalewidth;
            rect.height = 1.0f;
            //rect.x = (1.0f - scalewidth) / 2.0f;
            //rect.y = 0;

            _cameraRef.rect = rect;
        }
    }
}

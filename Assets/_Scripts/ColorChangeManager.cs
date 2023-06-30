using System.Collections.Generic;
using UnityEngine;

internal class ColorChangeManager : MonoBehaviour
{
    [SerializeField]
    private ColorChangingObject[] colorChangingObjects;

    private readonly Dictionary<Collider, ColorChangingObject> _objects = new();
    private Camera _mainCam;

    private void Awake()
    {
        foreach (var colorChangingObject in colorChangingObjects)
        {
            _objects.Add(colorChangingObject.ModelCollider, colorChangingObject);
        }
    }

    private void Start()
    {
        _mainCam = Camera.main;
    }

    private void Update()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            var touch = Input.GetTouch(i);
            if (touch.phase != TouchPhase.Began)
            {
                continue;
            }
            
            var touchRay = _mainCam.ScreenPointToRay(touch.position);
            if (!Physics.Raycast(touchRay, out var touchHitInfo))
            {
                continue;
            }

            if (!_objects.TryGetValue(touchHitInfo.collider, out var colorChangingObject))
            {
                continue;
            }

            colorChangingObject.ChangeColors();
            break;
        }
    }
}
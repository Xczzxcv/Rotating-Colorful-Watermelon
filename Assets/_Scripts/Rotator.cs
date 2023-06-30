using UnityEngine;

internal class Rotator : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Transform _tr;

    private void Awake()
    {
        _tr = transform;
    }

    private void Update()
    {
        var angle = Time.deltaTime * speed;
        _tr.Rotate(Vector3.up, angle);
    }
}

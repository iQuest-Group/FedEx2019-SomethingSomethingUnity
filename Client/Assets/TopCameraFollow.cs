using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopCameraFollow : MonoBehaviour
{

    [SerializeField] private Transform Target;
    [SerializeField] private float Speed = 1f;

    private Vector3 offset;
    private new Camera camera;

    void Start()
    {
        camera = GetComponent<Camera>();
        if (Target != null)
        {
            offset = transform.position - Target.position;
        }
    }

    void Update()
    {
        if (Target == null)
        {
            return;
        }
        var targetPos = Target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * Speed);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopCameraFollow : MonoBehaviour
{

    [SerializeField] private GameObject Target;
    [SerializeField] private float Speed = 1f;

    [SerializeField] private Vector3 offset;
    private new Camera camera;

    void Start()
    {
        camera = GetComponent<Camera>();
    }

    void Update()
    {
        if (Target == null)
        {
            Target = GameObject.FindGameObjectWithTag("Player");
            return;
        }
        var targetPos = Target.transform.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * Speed);
    }
}

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
            var players = GameObject.FindGameObjectsWithTag("Player");
            foreach(var player in players)
            {
                if (player.GetComponent<PlayerManager>().currentPlayer)
                {
                    Target = player;
                }
            }
            
            return;
        }
        var targetPos = Target.transform.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * Speed);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ChaserMP : NetworkBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private GameObject objectToChase = null;
    private float step;

    [Server]
    void Update()
    {
        step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, objectToChase.transform.position, step);
    }
}

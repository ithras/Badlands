using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private Transform objectToChase = null;
    private float step;

    void Start()
    {
        if (objectToChase == null)
            objectToChase = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, objectToChase.position, step);
    }
}

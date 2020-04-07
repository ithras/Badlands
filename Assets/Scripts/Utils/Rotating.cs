using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotating : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 15) * Time.deltaTime);
    }

    //https://www.youtube.com/watch?v=1o-Gawy3D48
    //ay chismosin el labarca
    //marico el que lea
    //https://www.youtube.com/watch?v=XAC8U9-dTZU
    //deje de revisar mi codigo y pongase a trabajar
}

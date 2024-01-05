using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMovement : MonoBehaviour
{
    public Transform positionA;
    public Transform positionB;
    public float velocidad = 2.0f; // Velocidad de movimiento
    private float tiempo = 0f; // Variable para seguir el progreso del movimiento


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.Equals(positionA))
        {
            // Calcula la posición actual utilizando Lerp entre A y B
            transform.position = Vector3.Lerp(positionA.position, positionB.position, Mathf.PingPong(tiempo * velocidad, 1.0f));

        }
        else
        {
            transform.position = Vector3.Lerp(positionB.position, positionA.position, Mathf.PingPong(tiempo * velocidad, 1.0f));

        }

        // Incrementa el tiempo basado en el tiempo transcurrido
        tiempo += Time.deltaTime;
    }
}

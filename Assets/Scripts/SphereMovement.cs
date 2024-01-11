using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class SphereMovement : NetworkBehaviour
{
    private float SpawnInterval = 2f;
    [SerializeField] 
    private GameObject[] waypoints;
    private int current = 0;
    public float velocidad = 8.0f; 
   
    private System.Random random = new System.Random();
    private float minDistance = 0.1f;

    public static SphereMovement instance;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, waypoints[current].transform.position) < minDistance)
        {
            current++;
            if (current >= waypoints.Length) 
            {
                current = 0;
            }

        }

        transform.position = Vector3.MoveTowards(this.transform.position, waypoints[current].transform.position, Mathf.PingPong(Time.deltaTime * velocidad, 1.0f));
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag.Equals("Player"))
        {
            Debug.Log(isLocalPlayer);
            if (!isLocalPlayer)
            {
                PlayerHitSphere(2);
            }
            else 
            {
                PlayerHitSphere(1);
            }
        }
    }

    public event Action<int> onPlayerHitSphere;

    public void PlayerHitSphere(int id)
    {
        if (onPlayerHitSphere != null)
        {
            onPlayerHitSphere(id);
            this.gameObject.SetActive(false);

            int number = random.Next(0, waypoints.Length);
            this.gameObject.transform.position = waypoints[number].transform.position;

            Invoke("SpawnSphere", SpawnInterval);
        }
    }

    private void SpawnSphere() 
    {
        this.gameObject.SetActive(true);
    }
}

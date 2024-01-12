using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class SphereMovement : NetworkBehaviour
{
    private float SpawnInterval = 2f;
    [SerializeField] 
    private GameObject[] waypoints;
    private int current = 0;

    public Transform positionA;
    public Transform positionB;
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
        if (!IsOwner) return;

        if (other.tag.Equals("Player"))
        {
            PlayerHitSphere(other.GetComponent<BulletManager>().playerID);
        }
    }

    public event Action<int> onPlayerHitSphere;

    public void PlayerHitSphere(int id)
    {
        if (onPlayerHitSphere != null)
        {
            onPlayerHitSphere(id);

            DespawnSpherClientRpc();

            int number = random.Next(0, waypoints.Length);
            this.gameObject.transform.position = waypoints[number].transform.position;

            Invoke("SpawnSphereClientRpc", SpawnInterval);
        }
    }

    [ClientRpc]
    void DespawnSpherClientRpc() 
    {
        this.gameObject.SetActive(false);
    }
    [ClientRpc]
    private void SpawnSphereClientRpc() 
    {
        this.gameObject.SetActive(true);
        this.GetComponent<NetworkObject>().gameObject.SetActive(true);
    }
}

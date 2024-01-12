using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class BulletManager : NetworkBehaviour
{
    public int playerID;
    private float bulletTime = 3f;

    void Start()
    {
        bulletTime += Time.time;
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.time > bulletTime)
        {
            DestroyObjectServerRpc();
        }
    }

    [ServerRpc(RequireOwnership = false)]
    void DestroyObjectServerRpc()
    {
        this.GetComponent<NetworkObject>().Despawn(true);
        Destroy(this.gameObject);
    }
}

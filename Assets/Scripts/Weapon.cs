using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Weapon : NetworkBehaviour
{
    private float weaponCooldownTime;
    public GameObject weaponBullet;
    public Transform weaponFirePosition;
    public float weaponSpeed = 15.0f;
    public float weaponLife = 3f;
    private float weaponTime;
    private AudioSource audioSource;
    private GameObject bullet;

    public int playerID;

    public override void OnNetworkSpawn() 
    {
        if (IsLocalPlayer)
        {
            playerID = 0;
        }
        else 
        {
            playerID = 1;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        weaponCooldownTime = 1f;
        audioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;

        if (Input.GetKeyDown(KeyCode.Mouse0)) //Fire1 is mouse 1st click
        {
            if (Time.time > weaponTime && Time.timeScale != 0f)
            {
                weaponTime = Time.time + this.weaponCooldownTime;
                RpcFireWeaponServerRpc();
            }
        }
    }
    [ServerRpc]
    void RpcFireWeaponServerRpc()
    {
        audioSource.Play();
        bullet = Instantiate(this.weaponBullet, this.weaponFirePosition.position, this.weaponFirePosition.rotation);
        bullet.GetComponent<BulletManager>().playerID = this.playerID;
        bullet.GetComponent<NetworkObject>().Spawn();
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * this.weaponSpeed;
        //StartCoroutine(DestroyObjectServerRpc(bullet));
    }

    
    IEnumerator DestroyObject(GameObject bullet) 
    {
        yield return new WaitForSeconds(this.weaponLife);

        bullet.GetComponent<NetworkObject>().Despawn(true);
        Destroy(bullet);
    }
}

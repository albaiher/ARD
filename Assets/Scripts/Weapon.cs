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
        if (!IsOwner) return;

        audioSource.Play();
        bullet = Instantiate(this.weaponBullet, this.weaponFirePosition.position, this.weaponFirePosition.rotation);
        bullet.GetComponent<NetworkObject>().Spawn(true);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * this.weaponSpeed;
        StartCoroutine(DestroyObject(bullet));
    }

    IEnumerator DestroyObject(GameObject bullet) 
    {
        yield return new WaitForSeconds(this.weaponLife);

        bullet.GetComponent<NetworkObject>().Despawn(true);
        Destroy(bullet);
    }
}

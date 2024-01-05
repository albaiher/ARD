using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private float weaponCooldownTime;
    public GameObject weaponBullet;
    public Transform weaponFirePosition;
    public float weaponSpeed = 15.0f;
    public float weaponLife = 3f;
    private float weaponTime;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        weaponCooldownTime = 1f;
        audioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) //Fire1 is mouse 1st click
        {
            if (Time.time > weaponTime)
            {
                weaponTime = Time.time + this.weaponCooldownTime;
                CmdShootRay();
            }
        }
    }
    void CmdShootRay()
    {
        RpcFireWeapon();
    }

    void RpcFireWeapon()
    {
        audioSource.Play();
        GameObject bullet = Instantiate(this.weaponBullet, this.weaponFirePosition.position, this.weaponFirePosition.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * this.weaponSpeed;
        Destroy(bullet, this.weaponLife);
    }
}

using Mirror;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerScript : NetworkBehaviour
{
    public TextMesh playerNameText;
    public GameObject floatingInfo;

    private Material playerMaterialClone;
    private SceneScript sceneScript;

    public GameObject weaponBullet;
    public Transform weaponFirePosition;

    private float weaponCooldown = 1.0f;
    public float weaponSpeed = 15.0f;
    public float weaponLife = 3f;

    public float weaponCooldownTime ;
    private AudioSource audioSource;

    [SyncVar(hook = nameof(OnNameChanged))]
    public string playerName;

    [SyncVar(hook = nameof(OnColorChanged))]
    public Color playerColor = Color.white;

    void Awake()
    {
        //allow all players to run this
        sceneScript = GameObject.FindObjectOfType<SceneScript>();
    }

    void OnNameChanged(string _Old, string _New)
    {
        playerNameText.text = playerName;
    }

    void OnColorChanged(Color _Old, Color _New)
    {
        playerNameText.color = _New;
        playerMaterialClone = new Material(GetComponent<Renderer>().material);
        playerMaterialClone.color = _New;
        GetComponent<Renderer>().material = playerMaterialClone;
    }

    public override void OnStartLocalPlayer()
    {
        audioSource = this.GetComponent<AudioSource>();

        sceneScript.playerScript = this;

        Camera.main.transform.SetParent(transform);
        Camera.main.transform.localPosition = new Vector3(-0.09f, 0.31f, 0.2f);

        floatingInfo.transform.localPosition = new Vector3(-0.09f, -0.07f, 0.6f);
        floatingInfo.transform.localScale = new Vector3(0.2f, 0.20f, 0.2f);

        string name = "Player" + Random.Range(100, 999);
        Color color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        CmdSetupPlayer(name, color);
    }

    [Command]
    public void CmdSendPlayerMessage()
    {
        if (sceneScript)
            sceneScript.statusText = $"{playerName} says hello {Random.Range(10, 99)}";
    }

    [Command]
    public void CmdSetupPlayer(string _name, Color _col)
    {
        // player info sent to server, then server updates sync vars which handles it on all clients
        playerName = _name;
        playerColor = _col;
        sceneScript.statusText = $"{playerName} joined.";
    }

    void Update()
    {
        if (!isLocalPlayer)
        {
            // make non-local players run this
            floatingInfo.transform.LookAt(Camera.main.transform);
            return;
        }

        float moveX = Input.GetAxis("Horizontal") * Time.deltaTime * 110.0f;
        float moveZ = Input.GetAxis("Vertical") * Time.deltaTime * 4f;

        transform.Rotate(0, moveX, 0);
        transform.Translate(0, 0, moveZ);

        if (Input.GetButtonDown("Fire1")) //Fire1 is mouse 1st click
        {
            if (Time.time > weaponCooldownTime)
            {
                weaponCooldownTime = Time.time + this.weaponCooldown;
                CmdShootRay();
            }
        }
    }

    [Command]
    void CmdShootRay()
    {
        RpcFireWeapon();
    }

    [ClientRpc]
    void RpcFireWeapon()
    {
        //audioSource.Play();
        GameObject bullet = Instantiate(this.weaponBullet, this.weaponFirePosition.position, this.weaponFirePosition.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * this.weaponSpeed;
        Destroy(bullet, this.weaponLife);
    }

}

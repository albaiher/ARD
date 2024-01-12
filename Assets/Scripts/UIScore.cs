using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class UIScore : NetworkBehaviour
{
    public int playerID;
    private NetworkVariable<int> score = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone);
    private Text UI;

    public NetworkVariable<int> Score { 
        get {return score;} 
        set {score = value;} 
    }

    // Start is called before the first frame update
    void Start()
    {
        SphereMovement.instance.onPlayerHitSphere += PlayerHitSphere;
        UI = this.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        UI.text = this.score.Value.ToString();
    }

    void PlayerHitSphere(int id) 
    {
        if (id == this.playerID) 
        { 
            this.score.Value++;
        }
        
    }
}

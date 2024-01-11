using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScore : MonoBehaviour
{
    public int playerID;
    private int score = 0;
    private Text UI;
    private bool once = false;

    public int Score { 
        get {return score;} 
        set {score = value;} 
    }

    // Start is called before the first frame update
    void Start()
    {
        if (SphereMovement.instance != null) 
        {
            once = true;
            SphereMovement.instance.onPlayerHitSphere += PlayerHitSphere;
        }
        UI = this.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SphereMovement.instance != null && !once)
        {
            once = true;
            SphereMovement.instance.onPlayerHitSphere += PlayerHitSphere;
        }
    }

    void PlayerHitSphere(int id) 
    {
        if (id == playerID) 
        {
            this.score++;
            UI.text = this.score.ToString();
        }
    }
}

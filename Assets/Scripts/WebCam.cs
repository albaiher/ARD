using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebCam : MonoBehaviour
{
    private WebCamTexture webCamTexture;
    [SerializeField] private GameObject webcamTV;
    
    void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        if (devices.Length > 0) 
        {
            webCamTexture = new WebCamTexture(devices[0].name);
            webcamTV.gameObject.GetComponent<Renderer>().material.mainTexture = webCamTexture;
            webCamTexture.Play();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Webcam : MonoBehaviour
{

    public string CameraName = "USB2.0 PC CAMERA";

    void Start()
    {
        WebCamTexture texture = new WebCamTexture(CameraName);
        Debug.Log("Available: " + string.Join(", ", WebCamTexture.devices.ToList().ConvertAll(device => device.name)));
        Debug.Log("Chosen: " + texture.deviceName); // USB2.0 PC CAMERA // USB2.0 PC CAMERA 1

        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = texture;
        texture.Play();
        Debug.Log("Playing " + texture.deviceName + " on " + gameObject.name);
    }
}

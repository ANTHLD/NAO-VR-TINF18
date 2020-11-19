using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTest : MonoBehaviour
{
    private Camera leftEye = new Camera();
    private Camera rightEye = new Camera();
    private Material renderTextureLeft;
    private Material renderTextureRight;

    // Use this for initialization
    void Start()
    {
        WebCamTexture leftCamera = new WebCamTexture("USB2.0 PC CAMERA");
        WebCamTexture rightCamera = new WebCamTexture("USB2.0 PC CAMERA 1");

        renderTextureLeft = new Material(Shader.Find("Specular"));

        leftCamera.Play();
        rightCamera.Play();

        //Left Test
        renderTextureLeft.mainTexture = leftCamera;
        leftEye.targetTexture = (RenderTexture)renderTextureLeft.mainTexture;
        leftEye.stereoTargetEye = StereoTargetEyeMask.Left;

        //Right Eye
        renderTextureRight.mainTexture = rightCamera;
        rightEye.targetTexture = (RenderTexture)renderTextureRight.mainTexture;
        rightEye.stereoTargetEye = StereoTargetEyeMask.Right;

    }
}

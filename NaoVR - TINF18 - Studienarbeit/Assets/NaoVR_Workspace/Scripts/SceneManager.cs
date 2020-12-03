using NaoApi.Behavior;
using RosSharp.RosBridgeClient;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class SceneManager : StateListener
{
    public Text InfoText, StatusText;
    public GameObject InfoCanvas, ImageCanvas, FloorMarker, LeftGripMarker, RightGripMarker, LeftHandMarker, RightHandMarker, LeftDisplay, RightDisplay, NaoMirror, Nao;
    public SteamVR_RenderModel LeftModel, RightModel;
    public JointStatePublisher publisher;
    public BehaviorController behaviorController;

    private Vector3 _previousRobotVector;

    private SteamVR_Action_Boolean grabStuff = SteamVR_Actions._default.CloseHand;

    void Start()
    {
        Register();
    }

    public override void StateChanged(StateManager.State newState)
    {
        switch (newState)
        {
            case StateManager.State.init:
                break;
            case StateManager.State.positioned:
                InfoText.text = "Great!\nNow look straight forward and press your right grip button to calibrate your height.";
                RightGripMarker.SetActive(true);
                break;
            case StateManager.State.calibrated:
                InfoText.text = "Place your hands inside the marked areas and press your left grip button to Arm / Disarm. When disarmed, move the Nao with the left Touchpad as shown above.";
                StatusText.text = "Calibrated";
                StatusText.color = Color.green;

                ChangeLayerRecursive(Nao, 0);
                LeftModel.SetMeshRendererState(false);
                RightModel.SetMeshRendererState(false);

                ImageCanvas.SetActive(true);
                RightGripMarker.SetActive(false);
                //LeftGripMarker.SetActive(true);
                RightHandMarker.SetActive(true);
                LeftHandMarker.SetActive(true);
                FloorMarker.SetActive(false);
                break;
            case StateManager.State.disarmed:
                StatusText.text = "Disarmed";
                StatusText.color = new Color(1, 0.5f, 0.2f);
                publisher.DoPublish = false;

                RightGripMarker.SetActive(false);
                RightHandMarker.SetActive(true);
                LeftHandMarker.SetActive(true);

                // Roboter anzeigen
                Nao.transform.localScale = _previousRobotVector;
                break;
            case StateManager.State.armed:
                InfoCanvas.SetActive(false);
                StatusText.text = "Armed";
                StatusText.color = Color.red;
                publisher.DoPublish = true;
                OpenHand("RHand");
                OpenHand("LHand");

                ImageCanvas.SetActive(false);
                //ChangeLayerRecursive(NaoMirror, 0);
                LeftDisplay.SetActive(true);
                RightDisplay.SetActive(true);
                LeftGripMarker.SetActive(false);
                RightHandMarker.SetActive(false);
                LeftHandMarker.SetActive(false);

                // Speichern der bisherigen Skalierung des Roboters
                // Der Roboter wird ausgeblendet, sodass die Arme erkannt werden können (wichtig fürs greifen)
                _previousRobotVector = Nao.transform.localScale;
                Nao.transform.localScale = Vector3.zero;
                break;
        }
    }

    private void OpenHand(string handName)
    {
        RosSharp.RosBridgeClient.Messages.DennisMessage dm = new RosSharp.RosBridgeClient.Messages.DennisMessage();
        dm.joint_names[0] = handName;
        dm.joint_angles[0] = 1;
        dm.speed = 1f;
        publisher.PublishMessage(dm);
    }

    private void CloseHand(string handName)
    {
        RosSharp.RosBridgeClient.Messages.DennisMessage dm = new RosSharp.RosBridgeClient.Messages.DennisMessage();
        dm.joint_names[0] = handName;
        dm.joint_angles[0] = 0.01f;
        dm.speed = 1f;
        publisher.PublishMessage(dm);
    }

    void Update()
    {
        if (state == StateManager.State.armed)
        {
            if (grabStuff.GetStateDown(SteamVR_Input_Sources.RightHand))
            {
                Debug.Log("closeRightHand");
                CloseHand("RHand");
            }

            if (grabStuff.GetStateUp(SteamVR_Input_Sources.RightHand))
            {
                Debug.Log("openRightHand");
                OpenHand("RHand");
            }

            if (grabStuff.GetStateDown(SteamVR_Input_Sources.LeftHand))
            {
                Debug.Log("closeLeftHand");
                CloseHand("LHand");
            }

            if (grabStuff.GetStateUp(SteamVR_Input_Sources.LeftHand))
            {
                Debug.Log("openLeftHand");
                OpenHand("LHand");
            }
        }
    }

    private void ChangeLayerRecursive(GameObject obj, int layer)
    {
        obj.layer = layer;
        foreach (Transform child in obj.transform)
        {
            ChangeLayerRecursive(child.gameObject, layer);
        }
    }
}

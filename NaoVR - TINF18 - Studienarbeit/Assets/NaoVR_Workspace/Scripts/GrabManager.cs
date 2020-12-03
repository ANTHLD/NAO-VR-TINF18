using NaoApi.Behavior;
using RosSharp;
using RosSharp.RosBridgeClient;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class GrabManager : StateListener
{
    public GameObject leftHand, rightHand;
    private JointStateWriter leftWriter, rightWriter;
    public BehaviorController behaviorController;

    private SteamVR_Action_Boolean grabStuff = SteamVR_Actions._default.CloseHand;

    void Start()
    {
        Register();

        leftWriter = leftHand.GetComponent<JointStateWriter>();
        if (leftWriter == null)
            Debug.Log("No Writer Component found in Module " + transform.parent.name);

        rightWriter = rightHand.GetComponent<JointStateWriter>();
        if (rightWriter == null)
            Debug.Log("No Writer Component found in Module " + transform.parent.name);
    }

    void Update()
    {
        
    }
}

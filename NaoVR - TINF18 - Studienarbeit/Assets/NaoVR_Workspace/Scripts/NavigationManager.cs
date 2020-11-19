using NaoApi.Behavior;
using NaoApi.Pose;
using NaoApi.Walker;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class NavigationManager : StateListener
{
    private Dictionary<SteamVR_Action_Boolean, Action> actionDownMap;
    private Dictionary<SteamVR_Action_Boolean, Action> actionUpMap;
    public PoseController poseController;
    public WalkerController walkerController;
    private bool isStanding = true;

    void Start()
    {
        actionDownMap = new Dictionary<SteamVR_Action_Boolean, Action>()
        {
            { SteamVR_Actions._default.WalkForward, walkerController.walkAhead },
            { SteamVR_Actions._default.TurnLeft, walkerController.turnLeft },
            { SteamVR_Actions._default.TurnRight, walkerController.turnRight },
            { SteamVR_Actions._default.Crouch, Crouch },
            { SteamVR_Actions._default.Stand, Stand }
        };
        actionUpMap = new Dictionary<SteamVR_Action_Boolean, Action>()
        {
            { SteamVR_Actions._default.WalkForward, walkerController.stopWalking },
            { SteamVR_Actions._default.TurnLeft, walkerController.stopWalking },
            { SteamVR_Actions._default.TurnRight, walkerController.stopWalking }
        };

        Register();
    }


    void Update()
    {
        if (state == StateManager.State.disarmed)
        {
            foreach (KeyValuePair<SteamVR_Action_Boolean, Action> pair in actionDownMap)
            {
                if (pair.Key.GetStateDown(SteamVR_Input_Sources.Any))
                    pair.Value.Invoke();
            }
            foreach (KeyValuePair<SteamVR_Action_Boolean, Action> pair in actionUpMap)
            {
                if (pair.Key.GetStateUp(SteamVR_Input_Sources.Any))
                    pair.Value.Invoke();
            }
        }
    }

    public void Crouch()
    {
        poseController.runPose("Crouch");
    }

    public void Stand()
    {
        poseController.runPose("StandZero");
    }

    public void StopMoving()
    {
        walkerController.stopWalking();
        Stand();
    }
}

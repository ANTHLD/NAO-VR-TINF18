using NaoApi.Behavior;
using NaoApi.Pose;
using NaoApi.Walker;
using NaoApi.Speech;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class NavigationManager : StateListener
{
    private Dictionary<SteamVR_Action_Boolean, Action> actionDownMap;
    public PoseController poseController;
    public WalkerController walkerController;

    void Start()
    {
        actionDownMap = new Dictionary<SteamVR_Action_Boolean, Action>()
        {
            { SteamVR_Actions._default.WalkForward, walkerController.stiffnessController.speech.StartOrStopReadMode },
            { SteamVR_Actions._default.Crouch, Crouch }
            //{ SteamVR_Actions._default.TurnLeft, walkerController.turnLeft },
            //{ SteamVR_Actions._default.TurnRight, walkerController.turnRight },
            //{ SteamVR_Actions._default.Stand, Stand }
        };

        Register();
    }


    void Update()
    {
        if (state == StateManager.State.disarmed || state == StateManager.State.armed)
        {
            foreach (KeyValuePair<SteamVR_Action_Boolean, Action> pair in actionDownMap)
            {
                if (pair.Key.GetStateDown(SteamVR_Input_Sources.Any))
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
}

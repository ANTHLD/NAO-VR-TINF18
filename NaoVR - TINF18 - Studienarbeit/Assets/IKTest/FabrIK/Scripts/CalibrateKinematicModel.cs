using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class CalibrateKinematicModel : StateListener
{
    public GameObject CalibrateObject, CalibrationReference, CalibrationLink;

    void Start()
    {
        Register();
    }
    public override void StateChanged(StateManager.State newState)
    {
        if (newState == StateManager.State.calibrated)
            Calibrate();
    }

    private void Calibrate()
    {
        float scaleFactor = CalibrationReference.transform.position.y / CalibrationLink.transform.position.y;
        Debug.Log($"Calibrating... Reference is at {CalibrationReference.transform.position.y}, Link is at {CalibrationLink.transform.position.y}. Resulting scale is {scaleFactor}");
        CalibrateObject.transform.localScale = scaleFactor * CalibrateObject.transform.localScale;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKHook : MonoBehaviour
{
    public GameObject Hook;
    public bool HookRotation, ForceRotationOnSegment;

    public void UpdateHook()
    {
        gameObject.transform.position = Hook.transform.position;
        if (HookRotation)
            gameObject.transform.rotation = Hook.transform.rotation;
        if (ForceRotationOnSegment)
        {
            NodeData data = gameObject.GetComponent<NodeData>();
            data.Parent.transform.position = gameObject.transform.position + gameObject.transform.TransformDirection(Vector3.back) * data.GetDistance();
        }
    }
}

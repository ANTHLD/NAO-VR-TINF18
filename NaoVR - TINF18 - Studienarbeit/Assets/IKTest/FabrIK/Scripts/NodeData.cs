using RosSharp.RosBridgeClient;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeData : MonoBehaviour
{
    public GameObject Parent;
    public Text DebugText;
    public JointStateWriter pitchWriter, rollWriter;
    private float pitch, roll;
    public float pitchOffset = 0, rollOffset = 0;
    public float pitchScale = 1, rollScale = 1;

    private float distance;

    public float GetDistance()
    {
        return distance;
    }

    void Start()
    {
        if (Parent != null)
        {
            distance = Vector3.Distance(Parent.transform.position, gameObject.transform.position);
        }
    }

    public void SetRotationRaw(float _pitch, float _roll)
    {
        pitch = _pitch * pitchScale + pitchOffset;
        roll = _roll * rollScale + rollOffset;
    }

    public void WriteJointData(GameObject child)
    {
        pitchWriter?.Write(pitch * Mathf.Deg2Rad);
        rollWriter?.Write(roll * Mathf.Deg2Rad);
    }
}

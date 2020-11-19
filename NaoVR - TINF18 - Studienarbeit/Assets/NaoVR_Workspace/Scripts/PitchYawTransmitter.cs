using RosSharp.RosBridgeClient;
using NaoApi.Stiffness;
using UnityEngine;

public class PitchYawTransmitter : StateListener
{
    public GameObject pitch, yaw;
    private JointStateWriter yawWriter, pitchWriter;

    void Start()
    {
        Register();

        yawWriter = yaw.GetComponent<JointStateWriter>();
        if (yawWriter == null)
            Debug.Log("No Writer Component found in Module " + transform.parent.name);

        pitchWriter = pitch.GetComponent<JointStateWriter>();
        if (pitchWriter == null)
            Debug.Log("No Writer Component found in Module " + transform.parent.name);
    }
    void Update()
    {
        if (state == StateManager.State.armed)
        {
            yawWriter?.Write(-transform.eulerAngles.y * Mathf.Deg2Rad);
            pitchWriter?.Write(transform.eulerAngles.x * Mathf.Deg2Rad);
        }
    }
}

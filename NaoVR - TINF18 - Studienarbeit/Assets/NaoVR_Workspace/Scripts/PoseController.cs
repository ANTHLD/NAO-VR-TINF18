using RosSharp.RosBridgeClient;
using UnityEngine;
using msgs = RosSharp.RosBridgeClient.Messages;

namespace NaoApi.Pose
{
    public class PoseController : MonoBehaviour
    {
        public RosSocket socket;
        public PoseActionClient actionClient;
        void Start()
        {
            GameObject Connector = GameObject.FindWithTag("Connector");
            socket = Connector.GetComponent<RosConnector>()?.RosSocket;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F2))
            {
                runPose("StandZero");
            }

            if (Input.GetKeyDown(KeyCode.F3))
            {
                runPose("Crounch");
            }
        }

        public void runPose(string poseName)
        {
            actionClient.poseName = poseName;
            actionClient.SendGoal();

        }
    }
}
using RosSharp.RosBridgeClient;
using UnityEngine;
using msgs = RosSharp.RosBridgeClient.Messages;

namespace NaoApi.Behavior
{
    public class BehaviorController : MonoBehaviour
    {
        public RosSocket socket;
        public BehaviorActionClient actionClient;
        void Start()
        {
            GameObject Connector = GameObject.FindWithTag("Connector");
            socket = Connector.GetComponent<RosConnector>()?.RosSocket;
        }
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                runBehavior("handcontroller-dc72e7/closeRightHand");
            }

            if (Input.GetKeyUp(KeyCode.X))
            {
                runBehavior("handcontroller-dc72e7/openRightHand");
            }

            if (Input.GetKeyDown(KeyCode.Y))
            {
                runBehavior("handcontroller-dc72e7/closeLeftHand");
            }

            if (Input.GetKeyUp(KeyCode.Y))
            {
                runBehavior("handcontroller-dc72e7/openLeftHand");
            }
        }

        public void runBehavior(string behaviorName)
        {
            actionClient.behaviorName = behaviorName;
            actionClient.SendGoal();
        }
    }

}
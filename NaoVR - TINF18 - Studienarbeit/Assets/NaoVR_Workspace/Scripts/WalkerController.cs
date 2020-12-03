using NaoApi.Stiffness;
using RosSharp.RosBridgeClient;
using RosSharp.RosBridgeClient.Services;
using UnityEngine;
using msgs = RosSharp.RosBridgeClient.Messages;
using System.Linq;

namespace NaoApi.Walker
{
    public class WalkerController : MonoBehaviour
    {
        public RosSocket socket;
        public StiffnessController stiffnessController;

        private string publication_id;
        private Valve.VR.SteamVR_TrackedObject _leftLeg;
        private Valve.VR.SteamVR_TrackedObject _rightLeg;

        private Vector3 _previousLeftPosition;
        private Vector3 _previousRightPosition;

        void Start()
        {
            GameObject Connector = GameObject.FindWithTag("Connector");
            socket = Connector.GetComponent<RosConnector>()?.RosSocket;
            publication_id = socket.Advertise<msgs.Geometry.Twist>("/cmd_vel");

            var trackedObjects = FindObjectsOfType<Valve.VR.SteamVR_TrackedObject>();
            _leftLeg = trackedObjects.FirstOrDefault(u => u.index == Valve.VR.SteamVR_TrackedObject.EIndex.Device5);
            _rightLeg = trackedObjects.FirstOrDefault(u => u.index == Valve.VR.SteamVR_TrackedObject.EIndex.Device6);
        }

        private void Update()
        {
            if (_previousLeftPosition == null)
                _previousLeftPosition = _leftLeg.transform.position;
            if (_previousRightPosition == null)
                _previousRightPosition = _rightLeg.transform.position;

            if (Input.GetKeyUp(KeyCode.UpArrow)|| Input.GetKeyUp(KeyCode.RightArrow)|| Input.GetKeyUp(KeyCode.LeftArrow))
            {
                stopWalking();
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                walkAhead();
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                turnLeft();
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                turnRight();
            }
        }

        public void walkAhead()
        {
            msgs.Geometry.Vector3 linear = new msgs.Geometry.Vector3();
            msgs.Geometry.Vector3 angular = new msgs.Geometry.Vector3();
            msgs.Geometry.Twist message = new msgs.Geometry.Twist();
            linear.x = 1.0f;
            linear.y = 0.0f;
            linear.z = 0.0f;
            angular.x = 0.0f;
            angular.y = 0.0f;
            angular.z = 0.0f;
            message.linear = linear;
            message.angular = angular;
            socket.Publish(publication_id, message);
        }

        public void turnLeft()
        {
            msgs.Geometry.Vector3 linear = new msgs.Geometry.Vector3();
            msgs.Geometry.Vector3 angular = new msgs.Geometry.Vector3();
            msgs.Geometry.Twist message = new msgs.Geometry.Twist();
            linear.x = 0.0f;
            linear.y = 0.0f;
            linear.z = 0.0f;
            angular.x = 0.0f;
            angular.y = 0.0f;
            angular.z = 0.5f;
            message.linear = linear;
            message.angular = angular;
            socket.Publish(publication_id, message);
        }

        public void turnRight()
        {
            msgs.Geometry.Vector3 linear = new msgs.Geometry.Vector3();
            msgs.Geometry.Vector3 angular = new msgs.Geometry.Vector3();
            msgs.Geometry.Twist message = new msgs.Geometry.Twist();
            linear.x = 0.0f;
            linear.y = 0.0f;
            linear.z = 0.0f;
            angular.x = 0.0f;
            angular.y = 0.0f;
            angular.z = -0.5f;
            message.linear = linear;
            message.angular = angular;
            socket.Publish(publication_id, message);
        }
        public void stopWalking()
        {
            msgs.Geometry.Vector3 linear = new msgs.Geometry.Vector3();
            msgs.Geometry.Vector3 angular = new msgs.Geometry.Vector3();
            msgs.Geometry.Twist message = new msgs.Geometry.Twist();
            linear.x = 0.0f;
            linear.y = 0.0f;
            linear.z = 0.0f;
            angular.x = 0.0f;
            angular.y = 0.0f;
            angular.z = 0.0f;
            message.linear = linear;
            message.angular = angular;
            socket.Publish(publication_id, message);
        }
    }
}
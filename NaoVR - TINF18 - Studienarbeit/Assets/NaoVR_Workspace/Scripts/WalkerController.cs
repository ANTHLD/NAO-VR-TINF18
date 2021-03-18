using NaoApi.Stiffness;
using RosSharp.RosBridgeClient;
using RosSharp.RosBridgeClient.Services;
using UnityEngine;
using msgs = RosSharp.RosBridgeClient.Messages;
using System.Linq;
using System;

namespace NaoApi.Walker
{
    public class WalkerController : MonoBehaviour
    {
        public RosSocket socket;
        public StiffnessController stiffnessController;

        private string publication_id;
        private Valve.VR.SteamVR_TrackedObject _walkTracker;
        private Valve.VR.SteamVR_TrackedObject _turnTracker;

        private Vector3 _previousWalkPosition;
        private Vector3 _previousTurnPosition;
        private bool _walking, _turning;

        void Start()
        {
            GameObject Connector = GameObject.FindWithTag("Connector");
            socket = Connector.GetComponent<RosConnector>()?.RosSocket;
            publication_id = socket.Advertise<msgs.Geometry.Twist>("/cmd_vel");

            var trackedObjects = FindObjectsOfType<Valve.VR.SteamVR_TrackedObject>();
            _walkTracker = trackedObjects.FirstOrDefault(u => u.index == Valve.VR.SteamVR_TrackedObject.EIndex.Device5);
            _turnTracker = trackedObjects.FirstOrDefault(u => u.index == Valve.VR.SteamVR_TrackedObject.EIndex.Device6);
        }

        private void Update()
        {
            if (_previousWalkPosition == Vector3.zero)
                _previousWalkPosition = _walkTracker.transform.position;
            if (_previousTurnPosition == Vector3.zero)
                _previousTurnPosition = _turnTracker.transform.eulerAngles;

            int differenceY = Convert.ToInt32(_walkTracker.transform.position.y - _previousWalkPosition.y);
            if (differenceY > 0 && !_walking)
            {
                _walking = true;
                walkAhead();
                System.Threading.Thread.Sleep(1000);
                stopMoving();
                _walking = false;
            }

            int yDifference = Convert.ToInt32(_turnTracker.transform.eulerAngles.y - _previousTurnPosition.y);
            if (Convert.ToInt32(_turnTracker.transform.eulerAngles.y) > 0 && Math.Abs(yDifference) > 100 && !_turning)
            {
                _turning = true;
                if (_turnTracker.transform.eulerAngles.y < 100)
                    turnRight();
                else
                    turnLeft();
                System.Threading.Thread.Sleep(1000);
                stopMoving();
                _previousTurnPosition = _turnTracker.transform.eulerAngles;
                _turning = false;
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
            angular.z = 1.0f;
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
            angular.z = -1.0f;
            message.linear = linear;
            message.angular = angular;
            socket.Publish(publication_id, message);
        }
        public void stopMoving()
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
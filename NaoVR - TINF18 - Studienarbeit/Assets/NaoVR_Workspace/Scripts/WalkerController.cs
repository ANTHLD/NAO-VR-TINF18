using NaoApi.Stiffness;
using RosSharp.RosBridgeClient;
using RosSharp.RosBridgeClient.Services;
using UnityEngine;
using msgs = RosSharp.RosBridgeClient.Messages;
using System.Linq;
using System;
using Valve.VR;

namespace NaoApi.Walker
{
    public class WalkerController : MonoBehaviour
    {
        public RosSocket socket;
        public StiffnessController stiffnessController;

        private string publication_id;
        private Valve.VR.SteamVR_TrackedObject _walkTracker;
        private Valve.VR.SteamVR_TrackedObject _turnTracker;

        private Vector3 _currentTurnPosition;
        private bool _walking, _turning;
        private bool _crouched;

        void Start()
        {
            GameObject Connector = GameObject.FindWithTag("Connector");
            socket = Connector.GetComponent<RosConnector>()?.RosSocket;
            publication_id = socket.Advertise<msgs.Geometry.Twist>("/cmd_vel");

            var trackedObjects = FindObjectsOfType<Valve.VR.SteamVR_TrackedObject>();
            _walkTracker = trackedObjects.FirstOrDefault(u => u.index == Valve.VR.SteamVR_TrackedObject.EIndex.Device2);
            _turnTracker = trackedObjects.FirstOrDefault(u => u.index == Valve.VR.SteamVR_TrackedObject.EIndex.Device1);
        }

        private void Update()
        {
            if (_currentTurnPosition == Vector3.zero)
                _currentTurnPosition = _turnTracker.transform.eulerAngles;

            // WALK
            var walkPosition = _walkTracker.transform.position.y;
            if (walkPosition > 0.75)
            {
                _walking = true;
                walkAhead();
                System.Threading.Thread.Sleep(1250);
                stopMoving();
                _walking = false;
            }

            // CROUCH
            var crouchPosition = _turnTracker.transform.position.y;
            if (crouchPosition < 0.7 && !_crouched)
            {
                _crouched = true;
                stiffnessController.speech.Pose(stiffnessController.speech.CROUCH);
            }
            else if (crouchPosition > 2.5 && _crouched)
            {
                _crouched = false;
                stiffnessController.speech.Pose(stiffnessController.speech.STAND_INIT);
            }

            // TURN
            var turnPosition = _turnTracker.transform.eulerAngles.y;
            var items = GetRotateDirection(_currentTurnPosition.y, turnPosition);
            if (!_turning)
            {
                if (items.Item1)
                    TurnRobot(items.Item2, turnRight);
                else
                    TurnRobot(items.Item3, turnLeft);
            }
        }

        private void TurnRobot(float degrees, Action turnMethod)
        {
            if (degrees >= 85 && degrees <= 90)
            {
                _turning = true;
                turnMethod();
                System.Threading.Thread.Sleep(2100);
                stopMoving();
                _currentTurnPosition = _turnTracker.transform.eulerAngles;
                _turning = false;
            }
        }

        private Tuple<bool, float, float> GetRotateDirection(float from, float to)
        {
            float clockWise = 0f;
            float counterClockWise = 0f;

            if (from <= to)
            {
                clockWise = to - from;
                counterClockWise = from + (360 - to);
            }
            else
            {
                clockWise = (360 - from) + to;
                counterClockWise = from - to;
            }
            return new Tuple<bool, float, float>((clockWise <= counterClockWise), clockWise, counterClockWise);
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
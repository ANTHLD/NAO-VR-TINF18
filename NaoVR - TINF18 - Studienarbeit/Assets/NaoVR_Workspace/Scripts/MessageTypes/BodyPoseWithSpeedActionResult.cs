/*
This message class is generated automatically with 'SimpleMessageGenerator' of ROS#
*/ 

using Newtonsoft.Json;
using RosSharp.RosBridgeClient.Messages.Geometry;
using RosSharp.RosBridgeClient.Messages.Navigation;
using RosSharp.RosBridgeClient.Messages.Sensor;
using RosSharp.RosBridgeClient.Messages.Standard;
using RosSharp.RosBridgeClient.Messages.Actionlib;

namespace RosSharp.RosBridgeClient.Messages
{
public class BodyPoseWithSpeedActionResult : Message
{
[JsonIgnore]
public const string RosMessageName = "naoqi_bridge_msgs/BodyPoseWithSpeedActionResult";

public Header header;
public GoalStatus status;
public BodyPoseWithSpeedResult result;

public BodyPoseWithSpeedActionResult()
{
header = new Header();
status = new GoalStatus();
result = new BodyPoseWithSpeedResult();
}
}
}


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
public class BodyPoseActionResult : Message
{
[JsonIgnore]
public const string RosMessageName = "naoqi_bridge_msgs/BodyPoseActionResult";

public Header header;
public GoalStatus status;
public BodyPoseResult result;

public BodyPoseActionResult()
{
header = new Header();
status = new GoalStatus();
result = new BodyPoseResult();
}
}
}


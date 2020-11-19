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
public class RunBehaviorActionFeedback : Message
{
[JsonIgnore]
public const string RosMessageName = "naoqi_bridge_msgs/RunBehaviorActionFeedback";

public Header header;
public GoalStatus status;
public RunBehaviorFeedback feedback;

public RunBehaviorActionFeedback()
{
header = new Header();
status = new GoalStatus();
feedback = new RunBehaviorFeedback();
}
}
}


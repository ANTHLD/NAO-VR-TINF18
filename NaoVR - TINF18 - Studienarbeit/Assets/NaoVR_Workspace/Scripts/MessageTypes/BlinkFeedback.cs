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
public class BlinkFeedback : Message
{
[JsonIgnore]
public const string RosMessageName = "naoqi_bridge_msgs/BlinkFeedback";

public ColorRGBA last_color;

public BlinkFeedback()
{
last_color = new ColorRGBA();
}
}
}


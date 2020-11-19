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
public class BlinkGoal : Message
{
[JsonIgnore]
public const string RosMessageName = "naoqi_bridge_msgs/BlinkGoal";

public ColorRGBA[] colors;
public ColorRGBA bg_color;
public float blink_duration;
public float blink_rate_mean;
public float blink_rate_sd;

public BlinkGoal()
{
colors = new ColorRGBA[0];
bg_color = new ColorRGBA();
blink_duration = new float();
blink_rate_mean = new float();
blink_rate_sd = new float();
}
}
}


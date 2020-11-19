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
public class ColorRGBA : Message
{
[JsonIgnore]
public const string RosMessageName = "std_msgs/ColorRGBA";

public float r;
public float g;
public float b;
public float a;

public ColorRGBA()
{
r = new float();
g = new float();
b = new float();
a = new float();
}
}
}


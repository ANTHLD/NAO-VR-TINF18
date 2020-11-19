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
public class BlinkActionGoal : Message
{
[JsonIgnore]
public const string RosMessageName = "naoqi_bridge_msgs/BlinkActionGoal";

public Header header;
public GoalID goal_id;
public BlinkGoal goal;

public BlinkActionGoal()
{
header = new Header();
goal_id = new GoalID();
goal = new BlinkGoal();
}
}
}


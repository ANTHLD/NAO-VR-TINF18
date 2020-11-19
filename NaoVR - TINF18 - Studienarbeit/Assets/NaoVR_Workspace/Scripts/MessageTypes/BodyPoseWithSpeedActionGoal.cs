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
public class BodyPoseWithSpeedActionGoal : Message
{
[JsonIgnore]
public const string RosMessageName = "naoqi_bridge_msgs/BodyPoseWithSpeedActionGoal";

public Header header;
public GoalID goal_id;
public BodyPoseWithSpeedGoal goal;

public BodyPoseWithSpeedActionGoal()
{
header = new Header();
goal_id = new GoalID();
goal = new BodyPoseWithSpeedGoal();
}
}
}


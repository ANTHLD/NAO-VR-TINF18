/*
This message class is generated automatically with 'SimpleMessageGenerator' of ROS#
*/ 

using Newtonsoft.Json;
using RosSharp.RosBridgeClient.Messages.Standard;

namespace RosSharp.RosBridgeClient.Messages
{
public class KeyValue : Message
{
[JsonIgnore]
public const string RosMessageName = "diadnostic_msgs/KeyValue";

public String key;
public String value;

public KeyValue()
{
key = new String();
value = new String();
}
}
}


/*
This message class is generated automatically with 'SimpleMessageGenerator' of ROS#
*/ 

using Newtonsoft.Json;
using RosSharp.RosBridgeClient.Messages.Standard;

namespace RosSharp.RosBridgeClient.Messages
{
public class DiagnosticStatus : Message
{
[JsonIgnore]
public const string RosMessageName = "diagnostic_msgs/DiagnosticStatus";

public byte level;
public String name;
public String message;
public String hardware_id;
public KeyValue[] values;

public DiagnosticStatus()
{
level = new int();
name = new String();
message = new String();
hardware_id = new String();
values = new KeyValue[0];
}
}
}


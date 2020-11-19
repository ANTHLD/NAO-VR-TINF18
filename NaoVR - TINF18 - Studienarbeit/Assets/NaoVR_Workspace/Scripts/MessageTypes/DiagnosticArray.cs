/*
This message class is generated automatically with 'SimpleMessageGenerator' of ROS#
*/ 

using Newtonsoft.Json;
using RosSharp.RosBridgeClient.Messages.Standard;

namespace RosSharp.RosBridgeClient.Messages
{
public class DiagnosticArray : Message
{
[JsonIgnore]
public const string RosMessageName = "diagnostic_msgs/DiagnosticArray";

public Header header;
public DiagnosticStatus[] status;

public DiagnosticArray()
{
header = new Header();
status = new DiagnosticStatus[0];
}
}
}


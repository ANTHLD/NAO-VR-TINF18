/*
This message class is generated automatically with 'ServiceMessageGenerator' of ROS#
*/

using Newtonsoft.Json;

namespace RosSharp.RosBridgeClient.Services
{
    public class wakeupRequest : Message
    {
        [JsonIgnore]
        public const string RosMessageName = "/nao_robot/pose/wakeup/wakeup";
        public wakeupRequest()
        {
        }
    }

    public class wakeupResponse : Message
    {
        [JsonIgnore]
        public const string RosMessageName = "/nao_robot/pose/wakeup/wakeup";

    }
}


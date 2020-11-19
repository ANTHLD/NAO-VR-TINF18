/*
This message class is generated automatically with 'ServiceMessageGenerator' of ROS#
*/

using Newtonsoft.Json;

namespace RosSharp.RosBridgeClient.Services
{
    public class enableStiffnessRequest : Message
    {
        [JsonIgnore]
        public const string RosMessageName = "/nao_robot/pose/body_stiffness/enable";

        public enableStiffnessRequest()
        {
        }
    }

    public class enableStiffnessResponse : Message
    {
        [JsonIgnore]
        public const string RosMessageName = "/nao_robot/pose/body_stiffness/enable";

    }
}


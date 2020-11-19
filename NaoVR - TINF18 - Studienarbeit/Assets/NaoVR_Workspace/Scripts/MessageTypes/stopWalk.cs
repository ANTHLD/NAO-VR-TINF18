/*
This message class is generated automatically with 'ServiceMessageGenerator' of ROS#
*/

using Newtonsoft.Json;

namespace RosSharp.RosBridgeClient.Services
{
    public class stopWalk_Request : Message
    {
        [JsonIgnore]
        public const string RosMessageName = "/stop_walk_srv";

        public stopWalk_Request()
        {
        }
    }
    public class stopWalk_Response : Message
    {
        [JsonIgnore]
        public const string RosMessageName = "/stop_walk_srv";

    }
}

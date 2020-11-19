using UnityEngine;
using RosSharp.RosBridgeClient;
using msgs = RosSharp.RosBridgeClient.Messages;


namespace NaoApi.Pose
{
    public class PoseActionClient : ActionClient<msgs.BodyPoseWithSpeedActionGoal , msgs.BodyPoseWithSpeedActionFeedback, msgs.BodyPoseWithSpeedActionResult>
    {
        public string poseName;

        protected override void Start()
        {
            ActionName = "/body_pose_naoqi";
            TimeStep = 0.1f;
            base.Start();
        }

        public override msgs.BodyPoseWithSpeedActionGoal GetGoal()
        {
            msgs.BodyPoseWithSpeedActionGoal message = new msgs.BodyPoseWithSpeedActionGoal();
            msgs.BodyPoseWithSpeedGoal message_content = new msgs.BodyPoseWithSpeedGoal();
            message_content.posture_name = poseName;
            message.goal = message_content;
            return message;
        }
    }
}
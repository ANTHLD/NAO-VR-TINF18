using UnityEngine;
using RosSharp.RosBridgeClient;
using msgs = RosSharp.RosBridgeClient.Messages;


namespace NaoApi.Behavior
{
    public class BehaviorActionClient : ActionClient<msgs.RunBehaviorActionGoal, msgs.RunBehaviorActionFeedback, msgs.RunBehaviorActionResult>
    {
        public string behaviorName;

        protected override void Start()
        {
            ActionName = "run_behavior";
            TimeStep = 0.1f;
            base.Start();
        }

        public override msgs.RunBehaviorActionGoal GetGoal()
        {
            msgs.RunBehaviorActionGoal message = new msgs.RunBehaviorActionGoal();
            msgs.RunBehaviorGoal message_content = new msgs.RunBehaviorGoal();
            message_content.behavior = behaviorName;
            message.goal = message_content;
            return message;
        }
    }
}


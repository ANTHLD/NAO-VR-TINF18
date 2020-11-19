using UnityEngine;
using RosSharp.RosBridgeClient;
using msgs = RosSharp.RosBridgeClient.Messages;


namespace NaoApi.LED
{
    public class LEDActionClient : ActionClient<msgs.BlinkActionGoal, msgs.BlinkActionFeedback, msgs.BlinkActionResult>
    {
        public string behaviorName;
        public msgs.ColorRGBA bg_color;
        public msgs.ColorRGBA[] colors;
        public float blink_duration;
        public float blink_rate_mean;
        public float blink_rate_sd;

        protected override void Start()
        {
            ActionName = "blink";
            TimeStep = 0.1f;
            base.Start();
        }

        public override msgs.BlinkActionGoal GetGoal()
        {
            msgs.BlinkActionGoal message = new msgs.BlinkActionGoal();
            msgs.BlinkGoal message_content = new msgs.BlinkGoal();
            message_content.bg_color = bg_color;
            message_content.colors = colors;
            message_content.blink_duration = blink_duration;
            message_content.blink_rate_mean = blink_rate_mean;
            message_content.blink_rate_sd = blink_rate_sd;
            message.goal = message_content;
            return message;
        }
    }
}

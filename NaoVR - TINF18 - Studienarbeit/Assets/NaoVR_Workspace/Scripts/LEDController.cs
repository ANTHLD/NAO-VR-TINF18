using RosSharp.RosBridgeClient;
using RosSharp.RosBridgeClient.Messages;
using UnityEngine;
using msgs = RosSharp.RosBridgeClient.Messages;

namespace NaoApi.LED
{
    public class LEDController : MonoBehaviour
    {
        public RosSocket socket;
        public LEDActionClient actionClient;
        void Start()
        {
            GameObject Connector = GameObject.FindWithTag("Connector");
            socket = Connector.GetComponent<RosConnector>()?.RosSocket;
        }

        public void blink(ColorRGBA[] used_colors, ColorRGBA init_color, float duration, float mean, float sd)
        {

            actionClient.colors = used_colors;
            actionClient.bg_color = init_color;
            actionClient.blink_duration = duration;
            actionClient.blink_rate_mean = mean;
            actionClient.blink_rate_sd = sd;
            actionClient.SendGoal();
        }
    }
}
using RosSharp.RosBridgeClient;
using UnityEngine;
using std_msgs = RosSharp.RosBridgeClient.Messages.Standard;

namespace NaoApi.Speech
{
    public class SpeechControler : MonoBehaviour
    {
        private RosSocket socket;
        private string publication_id;
        public std_msgs.String message;
        void Start()
        {
            GameObject Connector = GameObject.FindWithTag("Connector");
            socket = Connector.GetComponent<RosConnector>()?.RosSocket;
            publication_id = socket.Advertise<std_msgs.String>("/speech");
            message = new std_msgs.String();
            //say("Hallo, ich bin Mino. Du bist erfolgreich verbunden.");
            say("Hallo, i bims, Dennis. Du bist erfolgreich verbunden.");
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                say("Los geht's.");
            }
        }
        public void say(string text)
        {
            message.data = text;
            socket.Publish(publication_id, message);
        }
    }
}
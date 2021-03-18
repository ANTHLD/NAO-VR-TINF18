using RosSharp.RosBridgeClient;
using UnityEngine;
using std_msgs = RosSharp.RosBridgeClient.Messages.Standard;
using UnityEngine.Windows.Speech;
using System.Collections.Generic;
using System;
using System.Linq;

namespace NaoApi.Speech
{
    public class SpeechControler : MonoBehaviour
    {
        private RosSocket socket;
        private string publication_id;
        private DictationRecognizer dictationRecognizer;
        private bool _readMode;
        private bool _firstStart = true;
        private string _textToRead = String.Empty;

        public std_msgs.String message;
        void Start()
        {
            GameObject Connector = GameObject.FindWithTag("Connector");
            socket = Connector.GetComponent<RosConnector>()?.RosSocket;
            publication_id = socket.Advertise<std_msgs.String>("/speech");
            message = new std_msgs.String();
            InitializeSpeechEngine();
            say("Ich bin bereit.");
        }

        private void InitializeSpeechEngine()
        {
            dictationRecognizer = new DictationRecognizer();
            dictationRecognizer.DictationError += DictationRecognizer_DictationError;
            dictationRecognizer.DictationResult += DictationRecognizer_DictationResult;
        }

        private void DictationRecognizer_DictationError(string error, int hresult)
        {
            Debug.Log(error);
        }

        private void DictationRecognizer_DictationResult(string text, ConfidenceLevel confidence)
        {
            if (_firstStart)
                return;

            if (_readMode)
                _textToRead += text;
        }

        void Update()
        {
            if (dictationRecognizer != null && dictationRecognizer.Status == SpeechSystemStatus.Stopped)
                dictationRecognizer.Start();
        }

        public void StartOrStopReadMode()
        {
            _readMode = !_readMode;
            _firstStart = false;

            if (_readMode)
                say("Ich verstehe dich");

            if (!_readMode && !String.IsNullOrEmpty(_textToRead))
            {
                say(_textToRead);
                _textToRead = String.Empty;
            }
        }

        private void OnApplicationQuit()
        {
            if (dictationRecognizer != null)
            {
                dictationRecognizer.DictationError -= DictationRecognizer_DictationError;
                dictationRecognizer.DictationResult -= DictationRecognizer_DictationResult;
                dictationRecognizer.Stop();
                dictationRecognizer.Dispose();
            }
        }

        public void say(string text)
        {
            message.data = text;
            socket.Publish(publication_id, message);
        }
    }
}
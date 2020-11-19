/*
© Siemens AG, 2017-2019
Author: Dr. Martin Bischoff (martin.bischoff@siemens.com)

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at
<http://www.apache.org/licenses/LICENSE-2.0>.
Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using System.Collections.Generic;

namespace RosSharp.RosBridgeClient
{
    public class JointStatePublisher : Publisher<Messages.DennisMessage>
    {
        public bool DoPublish = false;
        public List<JointStateReader> JointStateReaders;
        public string FrameId = "Unity";

        private Messages.DennisMessage message;

        protected override void Start()
        {
            base.Start();
            InitializeMessage();
        }

        private void FixedUpdate()
        {
            UpdateMessage();
        }

        private void InitializeMessage()
        {
            int jointStateLength = JointStateReaders.Count;
            message = new Messages.DennisMessage
            {
                header = new Messages.Standard.Header { frame_id = FrameId },
                joint_names = new string[jointStateLength],
                joint_angles = new float[jointStateLength],
                speed = 0.0f
            };
        }

        private void UpdateMessage()
        {
            message.header.Update();
            for (int i = 0; i < JointStateReaders.Count; i++)
                UpdateJointState(i);

            PublishMessage(message);
        }

        public void PublishMessage(Messages.DennisMessage messageToPublish)
        {
            if (DoPublish)
                Publish(messageToPublish);
        }

        private void UpdateJointState(int i)
        {

            JointStateReaders[i].Read(
                out message.joint_names[i],
                out float position,
                out float velocity,
                out float effort);

            message.joint_angles[i] = position;
            message.speed = 0.2f;
        }


    }
}

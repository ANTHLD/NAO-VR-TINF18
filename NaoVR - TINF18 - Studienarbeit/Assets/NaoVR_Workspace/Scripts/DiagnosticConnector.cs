using RosSharp.RosBridgeClient;
using System;
using UnityEngine;
using msgs = RosSharp.RosBridgeClient.Messages;

public class DiagnosticConnector : MonoBehaviour
{
    public RosSocket socket;
    void Start()
    {
        GameObject Connector = GameObject.FindWithTag("Connector");
        socket = Connector.GetComponent<RosConnector>()?.RosSocket;
        string subscription_id = socket.Subscribe<msgs.DiagnosticArray>("/diagnostics", SubscriptionHandler);
    }

    private static void SubscriptionHandler(msgs.DiagnosticArray message)
    {
        Debug.Log(message.status);
    }
}

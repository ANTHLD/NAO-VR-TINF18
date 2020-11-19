using Assets.IKTest.FabrIK.Scripts;
using NaoApi.Stiffness;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Valve.VR;

public class NaoIK : StateListener
{
    public GameObject SegmentPrefab;
    public bool SendJoints = false, RenderDebug = false;

    private GameObject[] nodeInstances, segmentInstances;
    private List<List<GameObject>> hookedNodeChains = new List<List<GameObject>>();

    public int maxIterations = 10;
    public float acceptableDistance = 0.02f;
    private float minStep;

    void Start()
    {
        Register();
        minStep = acceptableDistance / 10;

        NodeData[] nodeData = gameObject.GetComponentsInChildren<NodeData>();
        nodeInstances = new GameObject[nodeData.Length];
        segmentInstances = new GameObject[nodeData.Length];

        for (int i = 0; i < nodeData.Length; i++)
        {
            nodeInstances[i] = nodeData[i].gameObject;
            if (!RenderDebug)
                nodeInstances[i].GetComponent<MeshRenderer>().enabled = false;
            if (nodeInstances[i].TryGetComponent<IKHook>(out _))
            {
                List<GameObject> nodeChain = new List<GameObject>();
                nodeChain.Add(nodeInstances[i]);
                while (nodeChain.Last().GetComponent<NodeData>().Parent != null)
                {
                    nodeChain.Add(nodeChain.Last().GetComponent<NodeData>().Parent);
                }
                //Remove shoulderbase, it is just for initially orienting the shoulder to calculate angles
                nodeChain.RemoveAt(nodeChain.Count - 1);
                hookedNodeChains.Add(nodeChain);
            }
            if (RenderDebug && nodeData[i].Parent != null)
            {
                segmentInstances[i] = Instantiate(SegmentPrefab, new Vector3(0, 0, 0), Quaternion.identity, gameObject.transform);
                SegmentData data = segmentInstances[i].GetComponent<SegmentData>();
                data.StartNode = nodeData[i].Parent;
                data.EndNode = nodeData[i].gameObject;
                segmentInstances[i].transform.localScale = new Vector3(1, 1, (data.EndNode.transform.position - data.StartNode.transform.position).magnitude);
            }
        }
        UpdateSegments();
    }

    void Update()
    {
        if (state >= StateManager.State.calibrated)
        {
            hookedNodeChains.ForEach(ApplyFabrIK);
            hookedNodeChains.ForEach(SendJointAngles);
            UpdateSegments();
        }
    }

    void ApplyFabrIK(List<GameObject> nodes)
    {
        GameObject effector = nodes[0];
        int cycle = 0;
        effector.GetComponent<IKHook>().UpdateHook();
        Vector3 startPos = effector.transform.position;
        //Quaternion startRot = effector.transform.rotation;
        do
        {
            Vector3 before = effector.transform.position;
            effector.transform.position = startPos;
            //effector.transform.rotation = startRot;
            DoFabrIKCycle(nodes);
            if (Vector3.Distance(before, effector.transform.position) < minStep)
                break;
            cycle++;
        } while (cycle < maxIterations
            && (Vector3.Distance(effector.transform.position, startPos)) > acceptableDistance);
        RotateNodesCorrectly(nodes);
    }

    void DoFabrIKCycle(List<GameObject> nodes)
    {
        Vector3 basePosition = nodes.Last().transform.position;
        //Starts at elbow because hand position gets set externally
        //Ends at elbow because shoulder is fixed point
        ApplyFabrIKToNodes(nodes, 0, nodes.Count - 1, 1);
        nodes.Last().transform.position = basePosition;
        ApplyFabrIKToNodes(nodes, nodes.Count - 1, -1, -1);
    }

    void ApplyFabrIKToNodes(List<GameObject> nodes, int start, int end, int delta)
    {
        for (int i = start + delta; i != end; i += delta)
        {
            float distance = delta == 1 ? nodes[i - 1].GetComponent<NodeData>().GetDistance() : nodes[i].GetComponent<NodeData>().GetDistance();
            Vector3 last = nodes[i - delta].transform.position;
            nodes[i].transform.position = last + (nodes[i].transform.position - last).normalized * distance;
        }
    }

    void RotateNodesCorrectly(List<GameObject> nodes)
    {
        for (int i = nodes.Count - 1; i >= 0; i--)
        {

            NodeData nodeData = nodes[i].GetComponent<NodeData>();

            GameObject parent = nodeData.Parent;
            GameObject current = nodes[i];
            GameObject child = i > 0 ? nodes[i - 1] : null;

            Vector3 parentForward = parent.transform.TransformDirection(Vector3.forward);
            Vector3 parentUp = parent.transform.TransformDirection(Vector3.up);

            float roll = 0, pitch = 0;
            if (child != null)
            {

                Vector3 childDirection = (child.transform.position - current.transform.position).normalized;

                current.transform.rotation = parent.transform.rotation;

                roll = parentUp.GetAngleOnAxis(childDirection, parentForward);
                current.transform.Rotate(Vector3.forward, roll);

                pitch = parentForward.GetAngleOnAxis(childDirection, current.transform.TransformDirection(Vector3.right));
                current.transform.Rotate(Vector3.right, pitch);

            }
            else
            {
                //special case for wrists
                roll = parentUp.GetAngleOnAxis(current.transform.up, parentForward);
            }

            nodeData.SetRotationRaw(pitch, roll);

            if (RenderDebug && nodeData.DebugText != null)
            {
                nodeData.DebugText.text = $"Pitch: {pitch.ToString("F1")}\nRoll: {roll.ToString("F1")}";
            }
        }
    }

    void SendJointAngles(List<GameObject> nodes)
    {
        for (int i = nodes.Count - 1; i >= 0; i--)
        {
            nodes[i].GetComponent<NodeData>().WriteJointData(i > 0 ? nodes[i - 1] : null);
        }
    }

    void UpdateSegments()
    {
        if (!RenderDebug)
            return;
        foreach (GameObject segment in segmentInstances)
        {
            if (segment == null)
                continue;
            SegmentData data = segment.GetComponent<SegmentData>();
            segment.transform.position = data.StartNode.transform.position;
            segment.transform.rotation = Quaternion.LookRotation(data.EndNode.transform.position - data.StartNode.transform.position);
        }
    }
}

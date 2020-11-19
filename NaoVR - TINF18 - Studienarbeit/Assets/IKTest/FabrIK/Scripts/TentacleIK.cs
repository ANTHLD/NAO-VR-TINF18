using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TentacleIK : MonoBehaviour
{
    public GameObject nodeObject, segmentObject, marker;

    public int segmentCount = 6;
    public float segmentLength = 0.2f;

    public int maxIterations = 10;
    public float acceptableDistance = 0.02f;
    private float minStep;

    private Vector3[] nodes;
    private GameObject[] nodeInstances, segmentInstances;

    // Start is called before the first frame update
    void Start()
    {
        minStep = acceptableDistance / 10;
        nodes = new Vector3[segmentCount + 1];
        nodeInstances = new GameObject[segmentCount];
        segmentInstances = new GameObject[segmentCount];
        nodes[0] = transform.position;
        for (int i = 0; i < segmentCount; i++)
        {
            nodeInstances[i] = Instantiate(nodeObject, new Vector3(0, 0, 0), Quaternion.identity);
            segmentInstances[i] = Instantiate(segmentObject, new Vector3(0, 0, 0), Quaternion.identity);
            segmentInstances[i].transform.localScale = new Vector3(1, 1, segmentLength);
            nodes[i + 1] = nodes[i] + Vector3.up * segmentLength;
        }
        SynchronizeObjectsToData();
    }

    // Update is called once per frame
    void Update()
    {
        int cycle = 0;

        do
        {
            Vector3 before = nodes[segmentCount];
            DoFabrIK(segmentCount, -1, marker.transform.position);
            DoFabrIK(0, 1, transform.position);
            if (Vector3.Distance(before, nodes[segmentCount]) < minStep)
                break;
            cycle++;
        } while (cycle < maxIterations
            && (Vector3.Distance(nodes[segmentCount], marker.transform.position)) > acceptableDistance);
        SynchronizeObjectsToData();
    }

    void DoFabrIK(int startNode, int direction, Vector3 goal)
    {
        nodes[startNode] = goal;
        for (int i = startNode + direction; i <= segmentCount && i >= 0; i += direction)
        {
            Vector3 last = nodes[i - direction];
            nodes[i] = last + (nodes[i] - last).normalized * segmentLength;
        }
    }

    /// <summary>
    /// Updates the Transforms of all the Segment and Node Objects
    /// </summary>
    void SynchronizeObjectsToData()
    {
        for (int i = 0; i < segmentCount; i++)
        {
            nodeInstances[i].transform.position = nodes[i + 1];
            segmentInstances[i].transform.position = nodes[i];
            segmentInstances[i].transform.rotation = Quaternion.LookRotation(nodes[i + 1] - nodes[i]);
        }
    }
}

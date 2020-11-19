using RosSharp.RosBridgeClient;
using UnityEngine;

public class RotationTestScript : MonoBehaviour
{
    float value = 0;
    JointStateWriter writer;
    // Start is called before the first frame update
    void Start()
    {
        writer = GetComponentInParent<JointStateWriter>();
        if (writer == null)
            Debug.Log("No Writer Component found in Module " + transform.parent.name);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            value += 0.1f;
            WriteValue();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            value -= 0.1f;
            WriteValue();
        }
    }

    void WriteValue()
    {
        writer?.Write(value);
        Debug.Log("Float now has Value: " + value);
    }
}
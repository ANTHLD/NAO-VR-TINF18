using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceCheckpoint : Predicate
{
    public GameObject Target;
    public Vector3 CheckAxis = new Vector3(1, 1, 1);
    public float distance = 0.4f;

    public override bool CriteriaMet()
    {
        Vector3 pos1 = RemoveAxis(gameObject.transform.position);
        Vector3 pos2 = RemoveAxis(Target.transform.position);
        return Vector3.Distance(pos1, pos2) < distance;
    }

    private Vector3 RemoveAxis(Vector3 vector)
    {
        return new Vector3(vector.x * CheckAxis.x, vector.y * CheckAxis.y, vector.z * CheckAxis.z);
    }
}

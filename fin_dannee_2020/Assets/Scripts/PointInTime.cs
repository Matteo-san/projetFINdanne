using UnityEngine;

public class PointInTime
{
    public Vector3 position;
    public Quaternion rotation;

    public PointInTime (Vector3 pos, Quaternion rot)
    {
        position = pos;
        rotation = rot;
    }
}

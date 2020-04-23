using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindTime : MonoBehaviour
{
    List<PointInTime> PointsInTime;

    Rigidbody objectRigid;

    bool isRewinding;

    public float RecordTime;

    void Start()
    {
        isRewinding = false;
        PointsInTime = new List<PointInTime>();
        objectRigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown("i"))
        {
            StartRewind();
        }

        if (Input.GetKeyUp("i"))
        {
            StopRewind();
        }
    }

    void FixedUpdate()
    {
        if (isRewinding)
        {
            Rewind();
        } else
        {
            Record();
        }
    }

    void StartRewind()
    {
        isRewinding = true;
        objectRigid.isKinematic = true;
    }

    void Rewind()
    {
        if (PointsInTime.Count > 0)
        {
            PointInTime pointInTime = PointsInTime[0];
            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;
            PointsInTime.RemoveAt(0);
        } else
        {
            StopRewind();
        }
    }

    void StopRewind()
    {
        isRewinding = false;
        objectRigid.isKinematic = false;
    }

    void Record()
    {
        if (PointsInTime.Count > Mathf.Round(RecordTime / Time.fixedDeltaTime))
        {
            PointsInTime.RemoveAt(PointsInTime.Count - 1);
        }

        PointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));
    }
}

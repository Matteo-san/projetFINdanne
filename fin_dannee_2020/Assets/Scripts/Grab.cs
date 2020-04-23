using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Doors
{
    public float doorPickupRange = 2f;
    public float doorMaxDistanceGrab = 3f;
    public float doorDistance = 2f;
}

[System.Serializable]
public class Items
{
    public float itemPickupRange = 3f;
    public float itemMaxDistanceGrab = 4f;
    public float itemDistance = 2f;
}

public class Grab : MonoBehaviour
{
    private Camera playerCam;

    private float PickupRange = 3f;
    private float maxDistanceGrab = 4f;
    private float distance = 3f;

    private GameObject objectHeld;
    private bool objectIsHeld;
    private bool isPickingUp;

    public Doors doorClass = new Doors();
    public Items itemClass = new Items();

    void Start()
    {
        playerCam = Camera.main;
        objectHeld = null;
    }

    void FixedUpdate()
    {
        if (Input.GetButton("Grab"))
        {
            if (!objectIsHeld)
            {
                PickObject();
                isPickingUp = true;
            }
            else
            {
                HoldObject();
            }
        }
        else if (objectIsHeld)
        {
            DropObject();
        }
    }

    void PickObject()
    {
        Ray playerAim = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(playerAim, out hit, PickupRange))
        {
            objectHeld = hit.collider.gameObject;

            if (hit.collider.tag.Equals("Door") && isPickingUp)
            {
                objectIsHeld = true;

                PickupRange = doorClass.doorPickupRange;
                distance = doorClass.doorDistance;
                maxDistanceGrab = doorClass.doorMaxDistanceGrab;
            }

            if (hit.collider.tag.Equals("Grabbable") && isPickingUp)
            {
                objectIsHeld = true;
                objectHeld.GetComponent<Rigidbody>().useGravity = false;
                objectHeld.GetComponent<Rigidbody>().freezeRotation = true;

                PickupRange = itemClass.itemPickupRange;
                distance = itemClass.itemDistance;
                maxDistanceGrab = itemClass.itemMaxDistanceGrab;
            }
        }
    }

    void HoldObject()
    {
        Ray playerAim = playerCam.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        Vector3 nextPos = transform.position + playerAim.direction * distance;
        Vector3 currentPos = objectHeld.transform.position;

        objectHeld.GetComponent<Rigidbody>().velocity = (nextPos - currentPos) * 10;

        if (Vector3.Distance(objectHeld.transform.position, transform.position) > maxDistanceGrab)
        {
            DropObject();
        }
    }

    void DropObject()
    {
        objectIsHeld = false;
        objectHeld.GetComponent<Rigidbody>().useGravity = true;
        objectHeld.GetComponent<Rigidbody>().freezeRotation = false;
        objectHeld = null;
    }
}

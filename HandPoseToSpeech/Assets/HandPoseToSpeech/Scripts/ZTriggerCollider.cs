using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZTriggerCollider : MonoBehaviour
{
    [SerializeField]
    private float scale = 1.0f;  // Scales how large the 'Z' pattern should be
    [SerializeField]
    private Transform fingerTipTransform;  // Holds the position of the fingertip

    private Transform colliderTransform;  // Holds the position of the sphere collider
    private int phase = 0;  // Identifies which phase we are at. Currently requires a full pass to use 'Z'.

    void Start()
    {
        colliderTransform = gameObject.transform;
    }

    public void StartZ()
    {
        if(phase == 0)
        {
            phase += 1;
            colliderTransform.position = new Vector3(fingerTipTransform.position.x + 0.1f, fingerTipTransform.position.y, fingerTipTransform.position.z);
        }
    }

    public void Z1()
    {
        if (phase == 1)
        {
            phase += 1;
            colliderTransform.position = new Vector3(colliderTransform.position.x - 0.1f, colliderTransform.position.y - 0.1f, colliderTransform.position.z);
        }
    }

    public void Z2()
    {
        if (phase == 2)
        {
            phase += 1;
            colliderTransform.position = new Vector3(colliderTransform.position.x + 0.1f, colliderTransform.position.y, colliderTransform.position.z);
        }
    }

    public void Reset()
    {
        phase = 0;
    }
}

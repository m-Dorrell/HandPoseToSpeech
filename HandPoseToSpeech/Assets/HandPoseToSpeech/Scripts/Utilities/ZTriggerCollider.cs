using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Moves the Sphere Collider used to trigger the 'Z' pattern
/// </summary>
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

    /// <summary>
    /// Moves the collider to the upper right
    /// </summary>
    public void StartZ()
    {
        if (phase == 0)
        {
            phase += 1;
            colliderTransform.position = new Vector3(fingerTipTransform.position.x + (0.1f * scale), fingerTipTransform.position.y, fingerTipTransform.position.z);
        }
    }

    /// <summary>
    /// Moves the collider to the lower left
    /// </summary>
    public void Z1()
    {
        if (phase == 1)
        {
            phase += 1;
            colliderTransform.position = new Vector3(colliderTransform.position.x - (0.1f * scale), colliderTransform.position.y - (0.1f * scale), colliderTransform.position.z);
        }
    }

    /// <summary>
    /// Moves the collider to the lower right
    /// </summary>
    public void Z2()
    {
        if (phase == 2)
        {
            phase += 1;
            colliderTransform.position = new Vector3(colliderTransform.position.x + (0.1f * scale), colliderTransform.position.y, colliderTransform.position.z);
        }
    }

    /// <summary>
    /// Resets the pattern
    /// </summary>
    public void Reset()
    {
        phase = 0;
    }
}

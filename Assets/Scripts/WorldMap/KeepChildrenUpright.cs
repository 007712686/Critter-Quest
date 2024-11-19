using UnityEngine;

public class KeepChildrenUpright : MonoBehaviour
{
    private Quaternion[] originalChildRotations;

    void Start()
    {
        // Store the original world rotations of all child objects
        int childCount = transform.childCount;
        originalChildRotations = new Quaternion[childCount];

        for (int i = 0; i < childCount; i++)
        {
            originalChildRotations[i] = transform.GetChild(i).rotation;
        }
    }

    void LateUpdate()
    {
        // Apply the original world rotations to the child objects
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            child.rotation = originalChildRotations[i];
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using IStreamYouScream;

[ExecuteInEditMode]
public class PolylineToMovementPath : MonoBehaviour
{
    Polyline polyline;
    MovementPath movementPath;

    void Awake()
    {
        polyline = gameObject.GetComponent<Polyline>();
        movementPath = gameObject.GetComponent<MovementPath>();
    }

    private void Start()
    {
        Debug.Log(polyline.nodes.Count);
        movementPath.PathSequence = new List<Vector3>(polyline.nodes).ToArray();
    }
}

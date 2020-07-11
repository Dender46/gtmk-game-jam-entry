using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptGizmo : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnDrawGizmos() {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Waypoint : MonoBehaviour
{
    public Waypoint Next;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Draw the waypoint icon
    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "waypoint", true);
    }

}

[CustomEditor(typeof(Waypoint))]
[InitializeOnLoad]
public class WaypointEditor : Editor
{
    [DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.NotInSelectionHierarchy)]
    static void DrawHandles(Waypoint waypoint, GizmoType gizmoType)
    {
        if (waypoint.Next != null)
        {
            var pos = waypoint.transform.position;
            var nextPos = waypoint.Next.transform.position;
            var direction = nextPos - pos;
            float distance = direction.magnitude;
            direction.Normalize();

            // Scale the arrow head for very short distances
            float size = Mathf.Min(distance*0.5f, 0.2f);

            // Offset the head and the end of the line to account for cone size
            var headPos = pos + direction * (distance - 0.75f * size);
            var endPoint = headPos - direction * (0.5f*size);

            Handles.color = new Color(1, 0.6f, 0.15f, 1);
            Handles.ConeHandleCap(0, headPos, Quaternion.LookRotation(direction), size, EventType.Repaint);
            Handles.DrawLine(pos, endPoint, size*10);
        }
    }
}
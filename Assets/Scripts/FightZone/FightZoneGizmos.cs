using UnityEngine;

[ExecuteAlways]
public class FightZoneGizmo : MonoBehaviour
{
    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        if (!Application.isPlaying) 
        {
            BoxCollider collider = GetComponent<BoxCollider>();
            if (collider != null)
            {
                Gizmos.color = Color.red;
                Matrix4x4 oldMatrix = Gizmos.matrix;
                Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);

                Gizmos.DrawWireCube(collider.center, collider.size);

                Gizmos.matrix = oldMatrix;
            }
        }
#endif
    }
}

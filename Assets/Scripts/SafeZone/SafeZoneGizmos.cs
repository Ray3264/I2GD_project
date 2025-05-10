using UnityEngine;

[ExecuteAlways]
public class SafeZoneGizmos : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        if (Application.isPlaying) return; // Не рисовать во время игры

        Gizmos.color = Color.green;

        BoxCollider collider = GetComponent<BoxCollider>();
        if (collider != null)
        {
            Vector3 scaledSize = Vector3.Scale(collider.size, transform.localScale);
            Vector3 worldCenter = transform.position + transform.rotation * Vector3.Scale(collider.center, transform.localScale);
            Gizmos.DrawWireCube(worldCenter, scaledSize);
        }
    }
}



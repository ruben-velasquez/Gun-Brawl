using UnityEngine;
using UnityEngine.EventSystems;

public class Tilemap : MonoBehaviour {
    [SerializeField]
    public float offset;
    [SerializeField]
    public int columns;
    [SerializeField]
    public int rows;
    [SerializeField]
    public Color color;

    private void OnDrawGizmosSelected() {
        Gizmos.color = color;
        for(int x = -columns; x < columns; x++) {
            Gizmos.DrawLine(new Vector3(-rows + offset, x + offset, 0), new Vector3(rows, x + offset, 0));
        }
        
        for(int y = -rows; y < rows; y++) {
            Gizmos.DrawLine(new Vector3(y + offset, -columns + offset, 0), new Vector3(y + offset, columns + offset, 0));
        }
    }
}
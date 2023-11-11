using UnityEngine;
using System.Collections.Generic;

public class MatchCamera : MonoBehaviour
{
    [SerializeField]
    private float smoothTime = 0.5f;
    [SerializeField]
    private float maxSpeed = 1;
    [SerializeField]
    private Vector2 initialMaxOffset;
    [SerializeField]
    private Vector2 initialMinOffset;
    [SerializeField]
    private float padding;
    [SerializeField]
    private float minSize;
    [SerializeField]
    private float maxSize;

    private Vector3 velocity;
    private Camera _camera;
    private List<Transform> players;

    void Start()
    {
        _camera = Camera.main;
    }

    void LateUpdate()
    {
        if(GameManager.Instance == null) return;
        
        players = PlayerUtility.GetPlayerTransforms();

        float maxDistance = GetMaxDistance();

        float desiredSize = (maxDistance / 2) + padding;

        _camera.orthographicSize = Mathf.Clamp(desiredSize, minSize, maxSize);

        Vector3 centerPoint = GetCenterPoint();
        Vector3 newPos = new Vector3(centerPoint.x, centerPoint.y, transform.position.z);

        newPos = Vector3.SmoothDamp(transform.position, newPos, ref velocity, smoothTime, maxSpeed);
    
        Vector2 minOffset = GetMinOffset();
        Vector2 maxOffset = GetMaxOffset();

        float x = Mathf.Clamp(newPos.x, minOffset.x, maxOffset.x);
        float y = Mathf.Clamp(newPos.y, minOffset.y, maxOffset.y);

        transform.position = new Vector3(x, y, transform.position.z);
    }

    Vector3 GetCenterPoint() {
        if(players.Count == 0) return Vector3.zero;
        if(players.Count == 1) return players[0].position;

        Bounds bounds = new Bounds(players[0].position, Vector3.zero);

        foreach (Transform player in players)
        {
            bounds.Encapsulate(player.position);
        }

        return bounds.center;
    }

    float GetMaxDistance() {
        float maxDistance = 0f;

        foreach (Transform player1 in players)
        {
            foreach (Transform player2 in players)
            {
                float distance = Vector2.Distance(player1.position, player2.position);

                if(distance > maxDistance) 
                    maxDistance = distance;
            }
        }

        return maxDistance;
    }

    Vector2 GetMinOffset() {
        float y = initialMinOffset.y + ((_camera.orthographicSize - 5) * 1f);
        float x = initialMinOffset.x + ((_camera.orthographicSize - 5) * 1.6f);

        return new Vector2(x, y);
    }
    
    Vector2 GetMaxOffset() {
        float y = initialMaxOffset.y - ((_camera.orthographicSize - 5) * 1f);
        float x = initialMaxOffset.x - ((_camera.orthographicSize - 5) * 1.6f);

        return new Vector2(x, y);
    }

    void OnDrawGizmosSelected()
    {
        _camera = Camera.main;
        
        // Obtener los offsets
        Vector2 minOffset = GetMinOffset();
        Vector2 maxOffset = GetMaxOffset();

        // Calcular las esquinas del rectángulo
        Vector3 topLeft = new Vector3(minOffset.x, maxOffset.y, 0);
        Vector3 topRight = new Vector3(maxOffset.x, maxOffset.y, 0);
        Vector3 bottomRight = new Vector3(maxOffset.x, minOffset.y, 0);
        Vector3 bottomLeft = new Vector3(minOffset.x, minOffset.y, 0);

        // Dibujar el rectángulo
        Gizmos.color = Color.green;
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
        Gizmos.DrawLine(bottomLeft, topLeft);
    }
}
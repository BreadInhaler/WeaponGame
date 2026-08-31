using UnityEngine;
using System.Collections.Generic;
class CameraController : MonoBehaviour{
    Camera cam;
    CameraShake shake; // ADD THIS
    public List<Transform> players = new List<Transform>();
    public float minZoom = 5f;
    public float maxZoom = 12f;
    public float padding = 2f;
    public float smoothTime = 0.3f;
    public Vector3 offset = new Vector3(0, 0, -10);
    private Vector3 velocity;
    private float zoomVelocity;

    void Start(){
        cam = Camera.main;
        shake = GetComponent<CameraShake>(); // ADD THIS
    }

    void LateUpdate(){
        if (players.Count == 0) return;
        if(GameManager.Instance.IsPaused()) return;

        Vector3 center = GetCenterPoint();

        Vector3 targetPos = center + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);

        // ADD THIS BLOCK — apply shake on top of the follow position
        if (shake != null)
            transform.position += shake.GetShakeOffset();

        float targetZoom = GetRequiredZoom();
        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, targetZoom, ref zoomVelocity, smoothTime);
    }

    Vector3 GetCenterPoint(){
        if (players.Count == 1) return players[0].position;
        Bounds bounds = new Bounds(players[0].position, Vector3.zero);
        foreach (Transform p in players) bounds.Encapsulate(p.position);
        return bounds.center;
    }

    float GetRequiredZoom(){
        Bounds bounds = new Bounds(players[0].position, Vector3.zero);
        foreach (Transform p in players) bounds.Encapsulate(p.position);
        float distance = Mathf.Max(bounds.size.x / cam.aspect, bounds.size.y) / 2f;
        float zoom = distance + padding;
        return Mathf.Clamp(zoom, minZoom, maxZoom);
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [Header("Joystick params")]
    [SerializeField]
    float minReleaseDistance = 5f;
    [SerializeField]
    float maxReleaseDistance = 50f;
    [SerializeField]
    float maxReleaseSpeed = 0.2f;
    [SerializeField]
    float linearFriction = 5f;
    [SerializeField]
    float angularFriction = 5f;
    [SerializeField]
    float min = 10f;
    [SerializeField]
    LayerMask raycastLayer, ballRayCastLayer;

    Vector3 A, B;
    bool casting = false, canCast = true;

    [SerializeField]
    bool swapDirection = true;

    float timer = 0f;
    [SerializeField]
    float stopThreshold = 1f;

    [SerializeField]
    Rigidbody rb;

    [SerializeField]
    Camera mainCamera;

    [SerializeField]
    ArrowIndicator arrow;

    [Header("Level Spawners")]
    [SerializeField]
    List<Transform> LevelPlayerSpawners = default;
    [SerializeField]
    List<Transform> LevelCameraSpawners = default;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (swapDirection)
            {
                swapDirection = false;
            }
            else
            {
                swapDirection = true;
            }
        }
        if (rb.linearVelocity.magnitude > 0.1f)
        {
            timer = 0f;
            return;
        }
        timer += Time.deltaTime;
        if (timer >= stopThreshold)
        {
            canCast = true;
        }
        else
        {
            canCast = false;
        }
        ///PREVIOUS
        /*if (Input.GetMouseButtonDown(0))
        {
            A = CastRay();
            casting = true;
        }
        else if (Input.GetMouseButton(0) && casting && canCast)
        {
            arrow.ShowArrow();
            B = CastRay();
            float magnitude;
            Vector3 d = GetForce(out magnitude);
            arrow.UpdateArrow(d.normalized, magnitude / maxReleaseDistance);
        }
        else if (Input.GetMouseButtonUp(0) && casting && canCast)
        {
            casting = false;
            Release();
            arrow.HideArrow();
        }
        else
        {
            casting = false;
        }*/

        ///NEW
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, ballRayCastLayer))
            {
                A = transform.position;
                casting = true;
            }
        }
        else if (Input.GetMouseButton(0) && casting && canCast)
        {
            arrow.ShowArrow();
            B = CastRay();
            float magnitude;
            Vector3 d = GetForce(out magnitude);
            arrow.UpdateArrow(d.normalized, magnitude / maxReleaseDistance);
        }
        else if (Input.GetMouseButtonUp(0) && casting && canCast)
        {
            casting = false;
            Release();
            arrow.HideArrow();
        }
        else
        {
            casting = false;
        }

    }

    private void FixedUpdate()
    {
        rb.linearVelocity -= rb.linearVelocity.normalized * Time.fixedDeltaTime * linearFriction;
        rb.angularVelocity -= rb.angularVelocity.normalized * Time.fixedDeltaTime * angularFriction;
        Vector3 normal = Vector3.up;
        float dotVelPerpToGround = Vector3.Dot(rb.linearVelocity, normal);
        if (dotVelPerpToGround > 0f)
        {
            rb.linearVelocity -= dotVelPerpToGround * normal;
        }
    }
    Vector3 CastRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, raycastLayer))
        {
            //Debug.Log(hitInfo.point);
            return hitInfo.point;
        }
        else
        {
            return Vector3.zero;
        }
    }

    Vector3 GetForce()
    {
        Vector3 d;
        if (swapDirection)
        {
            d = A - B;
        }
        else
        {
            d = B - A;
        }
        d.y = 0f;
        float magnitude = d.magnitude;
        magnitude = Math.Clamp(magnitude, minReleaseDistance, maxReleaseDistance);
        return d;
    }

    Vector3 GetForce(out float magnitude)
    {
        Vector3 d;
        if (swapDirection)
        {
            d = A - B;
        }
        else
        {
            d = B - A;
        }
        d.y = 0f;
        magnitude = d.magnitude;
        magnitude = Math.Clamp(magnitude, minReleaseDistance, maxReleaseDistance);
        return d;
    }

    float GetMagnitude()
    {
        Vector3 d;
        if (swapDirection)
        {
            d = A - B;
        }
        else
        {
            d = B - A;
        }
        d.y = 0f;
        float magnitude = d.magnitude;
        magnitude = Math.Clamp(magnitude, minReleaseDistance, maxReleaseDistance);
        return magnitude;
    }

    void Release()
    {
        float magnitude;
        Vector3 d = GetForce(out magnitude);
        rb.linearVelocity = magnitude * maxReleaseSpeed * d.normalized;
        Debug.DrawLine(A, A + maxReleaseDistance * d.normalized, Color.red, 1f);
    }

    public void StartLevel(int id)
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = LevelPlayerSpawners[id-1].position;
        transform.rotation = Quaternion.identity;
        mainCamera.transform.position = LevelCameraSpawners[id - 1].position;
        mainCamera.transform.rotation = LevelCameraSpawners[id - 1].rotation;
    }

    public void StartLevel(Transform ballPos, Transform cameraPos)
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = ballPos.position;
        transform.rotation = Quaternion.identity;
        mainCamera.transform.position = cameraPos.position;
        mainCamera.transform.rotation = cameraPos.rotation;
        arrow.HideArrow();
        canCast = true;
        casting = false;
    }
}

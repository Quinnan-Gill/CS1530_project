using System;
using UnityEngine;

public class Camera2DFollow : MonoBehaviour
{
    public Transform target;
    public float damping = 1;
    public float lookAheadFactor = 3;
    public float lookAheadReturnSpeed = 0.5f;
    public float lookAheadMoveThreshold = 0.1f;

    private float m_OffsetZ;
    private Vector3 m_LastTargetPosition;
    private Vector3 m_CurrentVelocity;
    private Vector3 m_LookAheadPos;

    public Transform cameraCanvas;
    private float cam_height;
    private float cam_width;
    private float canvas_height;
    private float canvas_width;

    // Use this for initialization
    private void Start()
    {
        m_LastTargetPosition = target.position;
        m_OffsetZ = (transform.position - target.position).z;
        transform.parent = null;

        Camera cam = gameObject.GetComponent<Camera>();
        cam_height = 2f * cam.orthographicSize;
        cam_width = cam_height * cam.aspect;

        canvas_height = cameraCanvas.GetComponent<RectTransform>().rect.height;
        canvas_width  = cameraCanvas.GetComponent<RectTransform>().rect.width;
    }


    // Update is called once per frame
    private void Update()
    {
        if (target == null)
        {
            return;
        }

        // only update lookahead pos if accelerating or changed direction
        float xMoveDelta = (target.position - m_LastTargetPosition).x;

        bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

        if (updateLookAheadTarget)
        {
            m_LookAheadPos = lookAheadFactor*Vector3.right*Mathf.Sign(xMoveDelta);
        }
        else
        {
            m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime*lookAheadReturnSpeed);
        }

        Vector3 aheadTargetPos = target.position + m_LookAheadPos + Vector3.forward*m_OffsetZ;
        Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);

        if (cameraCanvas != null)
        {
            transform.position = new Vector3(
                Mathf.Clamp(
                    newPos.x,
                    cameraCanvas.position.x - canvas_width/2 + cam_width/2,
                    cameraCanvas.position.x + canvas_width/2 - cam_width/2
                ),
                // newPos.x,
                Mathf.Clamp(
                    newPos.y,
                    cameraCanvas.position.y - canvas_height/2 + cam_height/2,
                    cameraCanvas.position.y + canvas_height/2 - cam_height/2
                ),
                newPos.z
            );
        }
        else
        {
            transform.position = newPos;
        }

        m_LastTargetPosition = target.position;
    }
}

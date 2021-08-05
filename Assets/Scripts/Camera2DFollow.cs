using System;
using System.Collections.Generic;
using UnityEngine;

public class Camera2DFollow : MonoBehaviour
{
    public class Box
    {
        public float Width;
        public float Height;
        public float X;
        public float Y;

        public float Right;
        public float Left;
        public float Top;
        public float Bottom;

        public Box()
        {
            Right  = float.MinValue;
            Left   = float.MaxValue;
            Top    = float.MinValue;
            Bottom = float.MaxValue;
        }

        public Box(float b_x, float b_y, float b_width, float b_height)
        {
            X = b_x;
            Y = b_y;
            Width = b_width;
            Height = b_height;

            Right = X + Width/2;
            Left = X - Width/2;
            Top = Y + Height/2;
            Bottom = Y - Height/2;
        }

        public bool InBox(Vector2 pos)
        {
            if (
                pos.x >= Left  &&
                pos.x <= Right &&
                pos.y <= Top  &&
                pos.y >= Bottom
            )
            {
                return true;
            }
            return false;
        }

        public void Maximize(Box other)
        {
            if (other.Right > Right)
            {
                Right = other.Right;
            }
            if (other.Left < Left)
            {
                Left = other.Left;
            }
            if (other.Top > Top)
            {
                Top = other.Top;
            }
            if (other.Bottom < Bottom)
            {
                Bottom = other.Bottom;
            }
        }

        public string ToString()
        {
            return "Right: " + Right + " Left: " + Left + " Top: " + Top + " Bottom: " + Bottom;
        }
    }

    public Transform target;
    public List<Transform> canvasList;
    private Box[] canvas_sizes;

    public float damping = 1;
    public float lookAheadFactor = 3;
    public float lookAheadReturnSpeed = 0.5f;
    public float lookAheadMoveThreshold = 0.1f;

    private float m_OffsetZ;
    private Vector3 m_LastTargetPosition;
    private Vector3 m_CurrentVelocity;
    private Vector3 m_LookAheadPos;
    private float nextTimeToSearch = 0;

    public Transform cameraCanvas;
    private float cam_height;
    private float cam_width;

    // Use this for initialization
    private void Start()
    {
        m_LastTargetPosition = target.position;
        m_OffsetZ = (transform.position - target.position).z;
        transform.parent = null;

        Camera cam = gameObject.GetComponent<Camera>();
        cam_height = 2f * cam.orthographicSize;
        cam_width = cam_height * cam.aspect;

        // if (cameraCanvas != null)
        // {
        // }

        canvas_sizes = new Box[canvasList.Count];
        for(int i=0; i < canvasList.Count; i++)
        {
            float canvas_height = canvasList[i].GetComponent<RectTransform>().rect.height;
            float canvas_width  = canvasList[i].GetComponent<RectTransform>().rect.width;
            canvas_sizes[i] = new Box(
                canvasList[i].position.x,
                canvasList[i].position.y,
                canvas_width,
                canvas_height
            );

            Debug.Log(canvas_sizes[i].ToString());
        }
    }


    // Update is called once per frame
    private void Update()
    {
        if (target == null)
        {
            FindPlayer();
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

        Box maxBox = new Box();
        Vector2 newPosFlat = new Vector2(newPos.x, newPos.y);
        bool inBox = false;
        int count = 0;

        for(int i=0; i < canvas_sizes.Length; i++)
        {
            if (canvas_sizes[i].InBox(newPosFlat))
            {
                inBox = true;
                maxBox.Maximize(canvas_sizes[i]);
            }
        }
        Debug.Log("Max " + maxBox.ToString());
        Debug.Log("Pos " + newPosFlat);
        Debug.Log("Low Lim " + (maxBox.Left + cam_height/2));
        Debug.Log("High Lim " + (maxBox.Right - cam_height/2));
        Debug.Log(
            "Clamp " + 
            Mathf.Clamp(
                newPosFlat.x,
                maxBox.Left + cam_width/2,
                maxBox.Right - cam_width/2
            )
        );

        if (inBox)
        {
            transform.position = new Vector3(
                Mathf.Clamp(
                    newPos.x,
                    maxBox.Left  + cam_width/2,
                    maxBox.Right - cam_width/2
                ),
                // newPos.x,
                Mathf.Clamp(
                    newPos.y,
                    maxBox.Top    + cam_height/2,
                    maxBox.Bottom - cam_height/2
                ),
                newPos.z
            );
        }
        // else
        // {
        //     
        // }
        transform.position = newPos;
        m_LastTargetPosition = target.position;
    }

    void FindPlayer()
    {
        if (nextTimeToSearch <= Time.time)
        {
            GameObject searchResult = GameObject.FindGameObjectWithTag("Player");
            if (searchResult != null)
            {
                target = searchResult.transform;
            }
            nextTimeToSearch = Time.time + 0.5f;
        }
    }
}

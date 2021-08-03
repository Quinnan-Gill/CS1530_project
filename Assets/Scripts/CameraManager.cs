using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject[] cam_transforms;
    public Transform player;

    private Transform[] canvas_list;

    // Start is called before the first frame update
    void Awake()
    {
        canvas_list = new Transform[cam_transforms.Length];

        // For loop that gets all the cameras canvases
        for(int i=0; i < canvas_list.Length; i++)
        {
            canvas_list[i] = cam_transforms[i].GetComponent<Camera2DFollow>().cameraCanvas;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            return;
        }
        
        for(int i=0; i < canvas_list.Length; i++)
        {
            if (WithinCanvas(canvas_list[i]))
            {
                cam_transforms[i].GetComponent<Camera>().enabled = true;
            }
            else
            {
                cam_transforms[i].GetComponent<Camera>().enabled = false;
            }
        }
    }

    private bool WithinCanvas(Transform canvas)
    {
        float canvas_height = canvas.GetComponent<RectTransform>().rect.height;
        float canvas_width  = canvas.GetComponent<RectTransform>().rect.width;

        if (
            (player.position.x >= canvas.position.x - canvas_width/2)  &&
            (player.position.x <  canvas.position.x + canvas_width/2)  &&
            (player.position.y >= canvas.position.y - canvas_height/2) &&
            (player.position.y <  canvas.position.y + canvas_height/2)
        )
        {
            return true;
        }
        
        return false;
    }
}

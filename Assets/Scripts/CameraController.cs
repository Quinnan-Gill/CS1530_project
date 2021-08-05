using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject[] cam_transforms;

    // private Transform[] playerLinks;
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
        for(int i=0; i < canvas_list.Length; i++)
        {
            Transform player = cam_transforms[i].GetComponent<Camera2DFollow>().target;
            
            if (player == null)
            {
                continue;
            }
            else if (WithinCanvas(i, player))
            {
                cam_transforms[i].GetComponent<Camera>().enabled = true;
            }
            else
            {
                cam_transforms[i].GetComponent<Camera>().enabled = false;
            }
        }
    }

    private bool WithinCanvas(int i, Transform player)
    {
        float canvas_height = canvas_list[i].GetComponent<RectTransform>().rect.height;
        float canvas_width  = canvas_list[i].GetComponent<RectTransform>().rect.width;

        if (
            (player.position.x >= canvas_list[i].position.x - canvas_width/2)  &&
            (player.position.x <  canvas_list[i].position.x + canvas_width/2)  &&
            (player.position.y >= canvas_list[i].position.y - canvas_height/2) &&
            (player.position.y <  canvas_list[i].position.y + canvas_height/2)
        )
        {
            return true;
        }
        
        return false;
    }
}

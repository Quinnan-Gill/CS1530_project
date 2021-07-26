using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject[] cam_transforms;
    public Transform player;

    private Canvas[] canvas_list;

    // Start is called before the first frame update
    void Awake()
    {
        canvas_list = new Canvas[cam_transforms.Length];
        // For loop that gets all the cameras canvases
        // for(int i=0; i < canvas_list.Length; i++)
        // {
        //     canvas_list[i] = cam_transforms[i].GetComponent<NewScript>().cameraCanvas;
        // }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool WithinCanvas()
    {
        return false;
    }
}

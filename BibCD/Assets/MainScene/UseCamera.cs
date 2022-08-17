using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseCamera : MonoBehaviour
{
    private bool camAvailable;
    private WebCamTexture backCam;
    private WebCamTexture frontCam;
    private Texture defaultBackground;
    private bool back;

    public RawImage background;
    public AspectRatioFitter fit;

    private void Start()
    {
        defaultBackground = background.texture;
        WebCamDevice[] devices = WebCamTexture.devices;

        if(devices.Length == 0)
        {
            Debug.Log("No camera detected.");
            camAvailable = false;
            return;
        }

        for (int i = 0; i < devices.Length; i++)
        {
            //backcamera found
            if (!devices[i].isFrontFacing)
            {
                Debug.Log("BackCam.");
                backCam = new WebCamTexture(devices[i].name, Screen.width, Screen.height);
            }
            //frontcamera found
            else if (devices[i].isFrontFacing)
            {
                Debug.Log("FrontCam.");
                frontCam = new WebCamTexture(devices[i].name, Screen.width, Screen.height);
            }
        }
        //if no backcam is found, use frontcam
        if (backCam == null)
        {
            Debug.Log("No backCam.");

            if (frontCam == null)
            {
                Debug.Log("Unable to find camera.");
                return;
            }
            frontCam.Play();
            background.texture = frontCam;
            camAvailable = true;
            back = false;
        }
        backCam.Play();
        background.texture = backCam;
        camAvailable = true;
        back = true;

       
    }

    private void Update()
    {
        if (!camAvailable)
        {
            return;
        }
        //at first just for backCam
        if (camAvailable)
        {
        
        if (back == true)
        {
            float ratio = (float)backCam.width / (float)backCam.height;
            fit.aspectRatio = ratio;

            float scaleY = backCam.videoVerticallyMirrored ? -1f : 1f;
            background.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

            int orient = -backCam.videoRotationAngle;
            background.rectTransform.localEulerAngles = new Vector3(0, 0, orient);
        }
        else
        {
            float ratio = (float)frontCam.width / (float)frontCam.height;
            fit.aspectRatio = ratio;

            float scaleY = frontCam.videoVerticallyMirrored ? -1f : 1f;
            background.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

            int orient = -frontCam.videoRotationAngle;
            background.rectTransform.localEulerAngles = new Vector3(0, 0, orient);
        }
        }
    }
}

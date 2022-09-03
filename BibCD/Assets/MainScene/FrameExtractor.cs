using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;
using static NativeGallery;

public class FrameExtractor : MonoBehaviour
{
    //public Canvas canvas;
    public RawImage transparentBackground;
    //public RawImage cameraBackground;

    // Start is called before the first frame update
    void Start()
    {
        //float h = canvas.GetComponent<RectTransform>().rect.height;
        //float w = canvas.GetComponent<RectTransform>().rect.width;
        //transparentBackground.GetComponent<RectTransform>().sizeDelta = new Vector2(w, h);
        //cameraBackground.GetComponent<RectTransform>().sizeDelta = new Vector2(w, h);
        transparentBackground.texture = VideoSelector.videoFrame;

    }
    void Update()
    {
        Debug.Log(transparentBackground.texture);
    }
}
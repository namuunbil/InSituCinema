using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using static NativeGallery;

public class VideoSelector : MonoBehaviour
{
    public RenderTexture projection;
    public static Texture2D videoFrame;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void PickVideo()
    {
        NativeGallery.Permission permission = NativeGallery.GetVideoFromGallery((path) =>
        {
            Debug.Log("Video path: " + path);
            if (path != null)
            {
                // Play the selected video
                VideoPlayer source = GameObject.Find("Source").GetComponent(typeof(VideoPlayer)) as VideoPlayer;
                source.url = path;
            }
        }, "Select a video");
        //VideoPlayer source = GameObject.Find("Source").GetComponent(typeof(VideoPlayer)) as VideoPlayer;
       
        Debug.Log("Permission result: " + permission);
    }

    public void ToCalibrate()
    {
        videoFrame = new Texture2D(500, 500);
        projection = Resources.Load("VideoProjection", typeof(RenderTexture)) as RenderTexture;
        Debug.Log(projection);
        if (videoFrame.width != projection.width || videoFrame.height != projection.height)
        {
            videoFrame.Reinitialize(projection.width, projection.height);
        }
        RenderTexture.active = projection;
        videoFrame.ReadPixels(new Rect(0, 0, projection.width, projection.height), 0, 0);
        videoFrame.Apply();
        RenderTexture.active = null;

        //Set videoFirstFrame as texture of the Background Raw Image
        Color[] pixels = videoFrame.GetPixels(0);
        for (int i = 0; i < pixels.Length; i++)
        {
            var color = pixels[i];
            color.a = 0.7F;
            pixels[i] = color;
        }
        videoFrame.SetPixels(pixels);
        videoFrame.Apply();
        Debug.Log("ToCalibrate");
        SceneManager.LoadScene("Calibrate");
    }
    public void DoneCalibrating()
    {
        SceneManager.LoadScene("Decoder");
    }

    public void StopVideo()
    {
        VideoPlayer source = GameObject.Find("Source").GetComponent(typeof(VideoPlayer)) as VideoPlayer;
        source.Stop();
    }

    public void PauseVideo()
    {
        VideoPlayer source = GameObject.Find("Source").GetComponent(typeof(VideoPlayer)) as VideoPlayer;
        source.Pause();
    }
    public void ResumeVideo()
    {
        VideoPlayer source = GameObject.Find("Source").GetComponent(typeof(VideoPlayer)) as VideoPlayer;
        source.Prepare();
        Debug.Log(source.isPrepared);
        while (!source.isPrepared)
        {
            StartCoroutine(waitForPrep(source));
        }
        Debug.Log(source.isPrepared);
        source.Play();

    }

    IEnumerator waitForPrep(VideoPlayer source)
    {
        while (!source.isPrepared)
        {
            yield return new WaitForSeconds(0.001f);
        }
    }
}
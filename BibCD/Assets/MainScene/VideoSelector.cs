using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using static NativeGallery;

public class VideoSelector : MonoBehaviour
{
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

        Debug.Log("Permission result: " + permission);
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
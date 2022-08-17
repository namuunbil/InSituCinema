using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using Bibcam.Decoder;
using Bibcam.Encoder;
using Avfi;

sealed class BibcamController : MonoBehaviour
{
    #region Scene object references

    [Space]
    [SerializeField] BibcamEncoder _encoder = null;
    [SerializeField] Camera _camera = null;
    [Space]
    [SerializeField] BibcamMetadataDecoder _decoder = null;
    [SerializeField] BibcamTextureDemuxer _demuxer = null;
    [Space]
    [SerializeField] Slider _depthSlider = null;
    [SerializeField] Text _depthLabel = null;
    [SerializeField] Text _recordLabel = null;
    [SerializeField] GameObject _recordSign = null;

    #endregion

    #region Private members

    VideoRecorder Recorder => GetComponent<VideoRecorder>();

    BibcamFrameFeeder _feeder;

    #endregion

    #region Public members (exposed for UI)

    public void OnRecordButton()
    {
        if (Recorder.IsRecording)
        {
            // Stop recording
            Recorder.EndRecording();
            _recordLabel.text = "Record";
            _recordLabel.color = Color.white;
            _recordSign.SetActive(false);
        }
        else
        {
            // Reset the camera position.
            _camera.transform.parent.position
              = -_camera.transform.localPosition;

            // Start recording
            Recorder.StartRecording();
            _recordLabel.text = "Stop";
            _recordLabel.color = Color.red;
            _recordSign.SetActive(true);
        }
    }

    #endregion

    #region MonoBehaviour implementation

    void Start()
    {
        // Recorder setup
        Recorder.source = (RenderTexture)_encoder.EncodedTexture;

        // Instant decoder setup
        _feeder = new BibcamFrameFeeder(_decoder, _demuxer);

        // FPS cap preference
        var fpsCap = PlayerPrefs.GetInt("fps_cap_preference") != 0;
        FindObjectOfType<ARSession>().matchFrameRateRequested = !fpsCap;

        // UI setup
        _depthSlider.value = PlayerPrefs.GetFloat("DepthSlider", 5);
    }

    void OnDestroy()
    {
        _feeder?.Dispose();
        _feeder = null;
    }

    void OnApplicationPause(bool paused)
    {
        if (paused && Recorder.IsRecording) Recorder.EndRecording();
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus) SceneManager.LoadScene(0);
    }

    void Update()
    {
        // Depth range settings update
        var maxDepth = _depthSlider.value;
        var minDepth = maxDepth / 50;
        (_encoder.minDepth, _encoder.maxDepth) = (minDepth, maxDepth);

        // Monitor update
        _feeder.AddFrame(_encoder.EncodedTexture);
        _feeder.Update();

        // UI update
        _depthLabel.text = $"Depth Range: {minDepth:0.0}m - {maxDepth:0.0}m";
        PlayerPrefs.SetFloat("DepthSlider", maxDepth);
    }

    #endregion
}

using GoogleARCore;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

//using UnityEngine.v

public class AugmentedImageVisualizer : MonoBehaviour
{
    // Start is called before the first frame update


    [SerializeField] private VideoClip[] _videoClips;
    public AugmentedImage Image;
    private VideoPlayer _videoPlayer;

    void Start()
    {
        _videoPlayer = GetComponent<VideoPlayer>();
        _videoPlayer.loopPointReached += OnStop;
    }

    private void OnStop(VideoPlayer source)
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Image == null || Image.TrackingState != TrackingState.Tracking)
        {
            return;
        }
        if (!_videoPlayer.isPlaying)
        {
            _videoPlayer.clip = _videoClips[Image.DatabaseIndex];
            _videoPlayer.Play();

        }
        transform.localScale = new Vector3(Image.ExtentX, Image.ExtentZ, 1);
    }
}

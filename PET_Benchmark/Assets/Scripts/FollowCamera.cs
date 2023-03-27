using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private float smoothFactor = 1f;
    [SerializeField] private float offsetRadius   = 0f;
    [SerializeField] private float distanceToHead = 2;
    [SerializeField] private GameObject _canvas;

    private Camera _camera;

    private void Start()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        // make the UI always face towards the camera
        _canvas.transform.rotation = _camera.transform.rotation;

        var cameraCenter = _camera.transform.position + _camera.transform.forward * distanceToHead;

        var currentPos = _canvas.transform.position;

        // in which direction from the center?
        var direction = currentPos - cameraCenter;

        // target is in the same direction but offsetRadius
        // from the center
        var targetPosition = cameraCenter + direction.normalized * offsetRadius;

        // finally interpolate towards this position
        _canvas.transform.position = Vector3.Lerp(currentPos, targetPosition, smoothFactor);
    }
}

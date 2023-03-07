using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAudio : MonoBehaviour
{
    private AudioSource source;
    private float distance;
    private GameObject participant;

    private float minDistance = 1f;
    private float maxDistance = 4f;
    private float closePitch = 2f;
    private float farPitch = 0;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        source.mute = false;
        participant = GameObject.Find("Player");
    }

    private void Update()
    {
        distance = Vector3.Distance(participant.transform.position, transform.position);

        float x = Mathf.Clamp(distance, minDistance, maxDistance);
        float pitch = ((farPitch - closePitch) * (x - minDistance) / (maxDistance - minDistance)) + closePitch; 
        source.pitch = pitch;
    }
}

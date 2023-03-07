using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAudioStepWise : MonoBehaviour
{
    private AudioSource source;
    private float distance;
    private GameObject participant;

    private float minDistance = 1f;
    private float maxDistance = 4f;
    [SerializeField] private float closeDelay;
    [SerializeField] private float middleDelay;
    [SerializeField] private float farDelay;
    private float delay;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        participant = GameObject.Find("Player");
        BeepWithDelay();
    }

    private void BeepWithDelay()
    {
        delay = farDelay;
        PlayOneStep();
    }

    private void PlayOneStep()
    {
        source.Play();
        
        distance = Vector3.Distance(participant.transform.position, transform.position);

        switch (distance)
        {
            case > 3f:
                delay = farDelay;
                break;
            case > 2f:
                delay = middleDelay;
                break;
            case > 1f:
                delay = closeDelay;
                break;
            default:
                delay = 0f;
                break;
        }

        Invoke("BeepWithDelay", delay);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Trigger_Snapshot : MonoBehaviour
{
   
    public float transitionTime = 2.0f;

    public List<AudioMixerSnapshot> snapShots;
    public int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            snapShots[index].TransitionTo(transitionTime);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            snapShots[0].TransitionTo(transitionTime);
        }

    }
}

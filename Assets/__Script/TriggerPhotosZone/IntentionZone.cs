using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class IntentionZone : MonoBehaviour
{
    public IntentionList PhotoIntention;
    private IntentionList.EntityType originalEntityType;

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
        if (other.gameObject.GetComponentInParent<PlayerController>() != null)
        {
            EvenementArea eventArea = gameObject.GetComponentInParent<EvenementArea>();
            originalEntityType = eventArea.AreaType.Type;
            eventArea.AreaType.Type = PhotoIntention.Type;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponentInParent<PlayerController>() != null)
        {
            EvenementArea eventArea = gameObject.GetComponentInParent<EvenementArea>();
            eventArea.AreaType.Type = originalEntityType;
        }
    }
}
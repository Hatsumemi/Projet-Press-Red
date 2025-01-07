using System.Collections;
using System.Collections.Generic;
using UnityEngine.Playables;
using UnityEngine;
using Unity.VisualScripting;

// Pensez à bien importer "UnityEngine.Playables" ! using UnityEngine.Playables;
public class PlayTimeline : MonoBehaviour
{
    // Glissez ici votre timeline
    public PlayableDirector playableDirector;
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            playableDirector.Play();
        }

    }
 

}

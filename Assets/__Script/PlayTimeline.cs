using System.Collections;
using System.Collections.Generic;
using UnityEngine.Playables;
using UnityEngine;
using Unity.VisualScripting;

// Pensez à bien importer "UnityEngine.Playables" ! using UnityEngine.Playables;
public class PlayTimeline : MonoBehaviour
{
    // Glissez ici votre timeline
    public GameObject MainCam;
    public GameObject CamCinematic;
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            MainCam.SetActive(false);
        }

    }

    IEnumerator WaitToChangCam()
    {
        yield return new WaitForSeconds(1);
        CamCinematic.SetActive(true);
    }
 

}

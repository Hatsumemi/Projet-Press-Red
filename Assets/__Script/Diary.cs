using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Diary : MonoBehaviour
{
    public List<Image> PicturesDev;

    

    public void CheckImages()
    {
        foreach (var image in PicturesDev)
        {
            if (image.sprite != null)
            {
                image.gameObject.SetActive(true);
            }
        }
    }

    public void ValidateObjective()
    {
        
    }
}

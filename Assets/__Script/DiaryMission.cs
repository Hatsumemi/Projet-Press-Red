using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiaryMission : MonoBehaviour
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
            else
                image.gameObject.SetActive(false);
        }
    }
    
}

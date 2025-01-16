using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiaryMission : MonoBehaviour
{
    public List<Image> PicturesDev;
    public Button ValidatingButton;
    public Image Objective;


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
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            OkCheck();
        }

    }

    public void OkCheck()
    {
        MainGame.Instance.Fading.enabled = true;
        MainGame.Instance.Fading.DOFade(1, 2);
        StartCoroutine(WaitToChangCam());
    }


    IEnumerator WaitToChangCam()
    {
        yield return new WaitForSeconds(2);
        MainGame.Instance.CamCinematic.SetActive(false);
        MainGame.Instance.Fading.enabled = false;
        MainGame.Instance.CamCinematic.SetActive(true);
        gameObject.SetActive(false);
    }
}

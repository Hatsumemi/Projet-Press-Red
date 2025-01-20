using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Diary : MonoBehaviour
{
    public List<GameObject> Pages;
    public Image Objective;
    public Button ValidatingButton;

    [SerializeField] GameObject PreviousArrow;
    [SerializeField] GameObject NextArrow;

    public int PageOn = 0;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            OkCheck();
        }

        if (PageOn == 0)
            PreviousArrow.SetActive(false);

        else if (PageOn == Pages.Count - 1)
            NextArrow.SetActive(false);

        else
        {
            PreviousArrow.SetActive(true);
            NextArrow.SetActive(true);
        }
    }


    public void NextPage()
    {
        Pages[PageOn].SetActive(false);
        PageOn++;
        Pages[PageOn].SetActive(true);
        Pages[PageOn].GetComponent<DiaryMission>().CheckImages();
    }

    public void PreviousPage()
    {
        Pages[PageOn].SetActive(false);
        PageOn--;
        Pages[PageOn].SetActive(true);
        Pages[PageOn].GetComponent<DiaryMission>().CheckImages();
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

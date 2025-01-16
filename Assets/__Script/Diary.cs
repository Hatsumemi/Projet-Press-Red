using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diary : MonoBehaviour
{
    public List<GameObject> Pages;

    [SerializeField] GameObject PreviousArrow;
    [SerializeField] GameObject NextArrow;

    public int PageOn = 0;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class MainGame : MonoBehaviour
{
    public static MainGame Instance;


    [Header("Component To Get")] public PlayerController m_PlayerController;
    public CameraManager m_CameraManager;
    public Photography m_Photography;
    public PhotoDevelopment m_PhotoDevelopment;
    public GizmosPlayerCam m_GizmosPlayerCam;
    public Timer m_Timer;
    public QTE m_QTE;
    public Diary m_Diary;
    public DiaryMission m_DiaryMission;

    [Header("Other Variables")]
    public TMP_Text TextPhoto;
    public TMP_Text TextDev;
    public GameObject MainCam;
    public GameObject CamCinematic;
    public Image Fading, FadingRed;
    [HideInInspector]public Vector3 RespawnPosition;
    [HideInInspector]public bool Triggered = false;
    [HideInInspector]public bool DeveloppmentIsActive = false;
    public GameObject DeveloppmentObj;    
    [HideInInspector]public bool DiaryIsActive = false;
    public GameObject Diary;

    private void Awake()
    {
        Instance = this;
        Fading.DOFade(0, 1);
        FadingRed.DOFade(0, 1);
        StartCoroutine(WaitToDisappear());
        MainCam.SetActive(true);
        CamCinematic.SetActive(false);
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            //Debug.Log("test");
            m_CameraManager.ChangeCam();
            TextPhoto.enabled = !MainGame.Instance.m_Photography.IsActive;
        }
        
        if (Input.GetKeyDown(KeyCode.J))
        {
            m_Diary.Pages[m_Diary.PageOn].GetComponent<DiaryMission>().CheckImages();
            DiaryIsActive = !DiaryIsActive;
            Diary.SetActive(DiaryIsActive);
            m_PlayerController.CanMove = !DiaryIsActive;
        }

        if (Triggered)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                DeveloppmentIsActive = !DeveloppmentIsActive;
                DeveloppmentObj.SetActive(DeveloppmentIsActive);
                m_PlayerController.CanMove = !DeveloppmentIsActive;
            }
        }
    }


    IEnumerator WaitToDisappear()
    {
        yield return new WaitForSeconds(1);
        Fading.enabled = false;
        FadingRed.enabled = false;
    }
}
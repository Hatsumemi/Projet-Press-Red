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
    public List<DiaryMission> m_DiaryMissions;

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
    private enum Types
    {
        informatives,
        emotive,
        sensationnelles
    }

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
        DeveloppmentObj.gameObject.SetActive(false);
        Diary.gameObject.SetActive(false);
        foreach (var item in m_DiaryMissions)
        {
            item.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            OkCheck();
        }
        
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

    public void OkCheck()
    {

        MainGame.Instance.Fading.enabled = true;
        MainGame.Instance.Fading.DOFade(1, 2);
        StartCoroutine(WaitToChangCam());
    }


    IEnumerator WaitToChangCam()
    {
        yield return new WaitForSeconds(2);
        CamCinematic.SetActive(false);
        Fading.enabled = false;
        CamCinematic.SetActive(true);
        m_Diary.ValidatingButton.gameObject.SetActive(false); 
    }
    
    IEnumerator WaitToDisappear()
    {
        yield return new WaitForSeconds(1);
        Fading.enabled = false;
        FadingRed.enabled = false;
    }
}
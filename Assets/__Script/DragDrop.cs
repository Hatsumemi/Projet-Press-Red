using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Vector3 originalPosition;
    private float _targetXMax;
    private float _targetXMin;
    private float _targetYMax;
    private float _targetYMin;

    private void Awake()
    {
        _targetXMax = MainGame.Instance.m_Diary.Pages[MainGame.Instance.m_Diary.PageOn].GetComponent<DiaryMission>()
            .Objective.transform.position.x + 192;
        _targetXMin = MainGame.Instance.m_Diary.Pages[MainGame.Instance.m_Diary.PageOn].GetComponent<DiaryMission>()
            .Objective.transform.position.x - 192;
        _targetYMax = MainGame.Instance.m_Diary.Pages[MainGame.Instance.m_Diary.PageOn].GetComponent<DiaryMission>()
            .Objective.transform.position.y + 108;
        _targetYMin = MainGame.Instance.m_Diary.Pages[MainGame.Instance.m_Diary.PageOn].GetComponent<DiaryMission>()
            .Objective.transform.position.y - 108;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        if (_targetXMin < transform.position.x && transform.position.x < _targetXMax &&
            _targetYMin < transform.position.y && transform.position.y < _targetYMax)
        {
            //transform.position = MainGame.Instance.m_Diary.Pages[MainGame.Instance.m_Diary.PageOn].GetComponent<DiaryMission>().Objective.transform.position;
            MainGame.Instance.m_DiaryMission.Objective.sprite = gameObject.GetComponent<Image>().sprite;
            if (gameObject.GetComponent<Picture>().HasObjectiveIn == true)
            {
                MainGame.Instance.m_Diary.Pages[MainGame.Instance.m_Diary.PageOn].GetComponent<DiaryMission>()
                    .ValidatingButton.gameObject.SetActive(true);
            }

            foreach (var image in MainGame.Instance.m_DiaryMission.PicturesDev)
            {
                if (image.sprite != null)
                    image.gameObject.SetActive(true);
            }

            gameObject.SetActive(false);
        }

        transform.position = originalPosition;
    }
}
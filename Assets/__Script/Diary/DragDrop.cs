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
        _targetXMax = MainGame.Instance.m_Diary.Objective.transform.position.x + 192;
        _targetXMin = MainGame.Instance.m_Diary.Objective.transform.position.x - 192;
        _targetYMax = MainGame.Instance.m_Diary.Objective.transform.position.y + 108;
        _targetYMin = MainGame.Instance.m_Diary.Objective.transform.position.y - 108;
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
            MainGame.Instance.m_Diary.Objective.sprite = gameObject.GetComponent<Image>().sprite;
            gameObject.GetComponent<Image>().sprite = null;
            foreach (bool item in gameObject.GetComponent<Picture>().HasObjectivesIn)
            {
                if (item)
                {
                    MainGame.Instance.m_Diary.TypeOfPhoto++;
                }
                MainGame.Instance.m_Diary.ValidatingButton.gameObject.SetActive(true);
            }

            for (int i = 0; i < MainGame.Instance.m_DiaryMissions.Count; i++)
            {
                foreach (var image in MainGame.Instance.m_DiaryMissions[i].PicturesDev)
                {
                    if (image.sprite == null)
                    {
                        image.sprite = MainGame.Instance.m_Diary.Objective.sprite;
                        break;
                    }
                }
            }

            foreach (var image in MainGame.Instance.m_Diary.Pages[MainGame.Instance.m_Diary.PageOn].GetComponent<DiaryMission>().PicturesDev)
            {
                if (image.sprite != null)
                    image.gameObject.SetActive(true);
            }

            gameObject.SetActive(false);
        }

        transform.position = originalPosition;
    }
}
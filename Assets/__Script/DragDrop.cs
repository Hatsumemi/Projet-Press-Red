using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using static UnityEditor.Progress;

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
            transform.position = MainGame.Instance.m_Diary.Objective.transform.position;
            MainGame.Instance.m_Diary.ValidatingButton.gameObject.SetActive(true);
        }

        else
            transform.position = originalPosition;
    }
}
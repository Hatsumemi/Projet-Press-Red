using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Picture : MonoBehaviour
{
    Outline _outline;
    Collider2D _collider;

    void Start()
    {
        _outline = GetComponent<Outline>();
        _collider = GetComponent<Collider2D>();
        _outline.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

        if (Input.GetMouseButtonDown(1))
        {
            if (hit.collider == _collider)
            {
                OnClick();
            }
        }
    }

    void OnClick()
    {
        if (_outline.enabled==false)
        {
            _outline.enabled = true;
            MainGame.Instance.m_PhotoDevelopment.Selected++;
        }
        else if (_outline.enabled == true)
        {
            _outline.enabled = false;
            MainGame.Instance.m_PhotoDevelopment.Selected--;
        }
    }
}

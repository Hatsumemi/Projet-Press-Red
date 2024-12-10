using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Photography : MonoBehaviour
{

    public bool IsActive = false;
    public bool Triggered = false;
    public Image Flash;
    [SerializeField] private int _photoToTake;
    private int _photoCount;


    void Start()
    {
        Flash.color = new Color(Flash.color.r, Flash.color.g, Flash.color.b, 0f);
    }

    void Update()
    {
        if (IsActive && Input.GetMouseButtonDown(0))
        {
            if (Triggered)
            {
                if (_photoCount < _photoToTake)
                    StartCoroutine(WaitToPhotograph());
                else
                    Debug.Log("You can't take anymore photos.");
            }
        }
    }

    IEnumerator WaitToPhotograph()
    {
        Flash.DOFade(1, 0.2f);
        yield return new WaitForSeconds(0.2f);
        _photoCount++;
        Flash.DOFade(0, 0.2f);
    }

}

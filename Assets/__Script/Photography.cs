using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Rendering;
using System.IO;

public class Photography : MonoBehaviour
{

    [Header("Values")]
    public Camera Camera;
    [HideInInspector] public bool IsActive = false;
    [HideInInspector] public bool Triggered = false;
    private int _photoCount;
    private bool _takePhoto = false;


    [Header("Photographies")]
    [SerializeField] private List<Image> _photos;
    public Image Flash;
    private Texture2D _photoTakenTexture;

    void Start()
    {
        Flash.color = new Color(Flash.color.r, Flash.color.g, Flash.color.b, 0f);
        _photoTakenTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
    }

    void Update()
    {
        if (IsActive && Input.GetMouseButtonDown(0))
        {
            if (Triggered && _takePhoto == false)
            {
                if (_photoCount < _photos.Count)
                    StartCoroutine(Photo());
                else
                    Debug.Log("You can't take anymore photos.");
            }
            else
                Debug.Log("There is nothing important to take here.");
        }
    }

    IEnumerator WaitToPhotograph()
    {
        Flash.DOFade(1, 0.2f);
        yield return new WaitForSeconds(0.2f);
        _photoCount++;
        _takePhoto = false;
        Flash.DOFade(0, 0.2f);
    }

    IEnumerator Photo()
    {
        yield return new WaitForEndOfFrame();

        Rect regionToCapture = new Rect(0, 0, Screen.width, Screen.height);

        _photoTakenTexture.ReadPixels(regionToCapture, 0, 0, false);
        _photoTakenTexture.Apply();
        TakingPhotopragy();
    }

    private void TakingPhotopragy()
    {
        Debug.Log("Screenshot taken");
        _takePhoto = true;

        Sprite PhotoSprite = Sprite.Create(_photoTakenTexture, new Rect(0.0f, 0.0f,
                                           _photoTakenTexture.width, _photoTakenTexture.height),
                                           new Vector2(0.5f, 0.5f), 100.0f);
        foreach (Image image in _photos)
        {
            if (image.sprite == null)
            {
                image.sprite = PhotoSprite;
                break;
            }
        }

        StartCoroutine(WaitToPhotograph());
    }

}
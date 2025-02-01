using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Photography : MonoBehaviour
{

    [Header("Values")]
    public Camera Camera;
    public float NormalFov = 60;
    [HideInInspector] public bool IsActive = false;
    private int _photoCount;
    private bool _takePhoto = false;


    [Header("Photographies")]
    public float MinFoV;
    public float MaxFoV;
    public float Sensitivity;
    public List<GameObject> Objetives;
    public Image Flash;
    [SerializeField] private List<Image> _photos;
    [SerializeField] private List<bool> _objectivesAreOn = new List<bool> { false, false, false };
    public GameObject TriggerDeath;
    public GameObject Trigger;
    private Texture2D _photoTakenTexture;

    void Start()
    {
        Flash.color = new Color(Flash.color.r, Flash.color.g, Flash.color.b, 0f);
        _photoTakenTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        TriggerDeath.SetActive(false);
        Trigger.SetActive(false);
    }

    void Update()
    {
        if (IsActive)
        {
            var fov = Camera.fieldOfView;
            fov += Input.GetAxis("Mouse ScrollWheel") * Sensitivity;
            fov = Mathf.Clamp(fov, MinFoV, MaxFoV);
            Camera.fieldOfView = fov;

            if (Input.GetMouseButtonDown(0))
            {
                if (_takePhoto == false)
                {
                    if (_photoCount < _photos.Count)
                        StartCoroutine(Photo());
                    else
                        Debug.Log("You can't take anymore photos.");
                }
            }
        }

        if (IsActive == false)
            Camera.fieldOfView = NormalFov;
    }

    IEnumerator WaitToPhotograph()
    {
        Flash.DOFade(1, 0.2f);
        yield return new WaitForSeconds(0.2f);
        _photoCount++;
        _takePhoto = false;
        Flash.DOFade(0, 0.2f);
        for (int i = 0; i < _objectivesAreOn.Count; i++)
        {
            if (_objectivesAreOn[i] == true)
            {
                _objectivesAreOn[i] = false;
            }
        }
        yield return new WaitForSeconds(5);
        TriggerDeath.SetActive(false);
        Trigger.SetActive(false);
    }

    IEnumerator Photo()
    {
        yield return new WaitForEndOfFrame();
        foreach (var item in Objetives)
        {
            if (item.GetComponent<Renderer>().isVisible)
            {
                for (int i = 0; i < _objectivesAreOn.Count; i++)
                {
                    if (_objectivesAreOn[i] == false)
                    {
                        _objectivesAreOn[i] = true;
                        break;
                    }
                }
            }
        }

        Rect regionToCapture = new Rect(0, 0, Screen.width, Screen.height);

        _photoTakenTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

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
        PhotoSprite.name = "photo" + _photoCount;
        foreach (Image image in _photos)
        {
            if (image.sprite == null)
            {
                int i = 0;
                image.sprite = PhotoSprite;
                foreach (bool item in _objectivesAreOn)
                {
                    if (item == true)
                        image.GetComponent<Picture>().HasObjectivesIn[i] = true;
                    i++;
                }
                break;
            }
        }
        TriggerDeath.SetActive(true);
        Trigger.SetActive(true);
        StartCoroutine(WaitToPhotograph());
    }

}
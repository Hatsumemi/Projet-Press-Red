using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class Photography : MonoBehaviour
{
    [Header("Values")] public Camera Camera;
    public float NormalFov = 60;
    [HideInInspector] public bool IsActive = false;
    private int _photoCount;
    private bool _takePhoto = false;


    [Header("Photographies")] public float MinFoV;
    public float MaxFoV;
    public float Sensitivity;
    public List<GameObject> Objetives;
    public Image Flash;
    public GameObject FlashLight;
    [SerializeField] private List<Image> _photos;
    [SerializeField] private List<bool> _objectivesAreOn = new List<bool> { false, false, false };
    public GameObject TriggerDeath;
    public GameObject Trigger;
    private Texture2D _photoTakenTexture;
    private bool _flashActivated = false;

    void Start()
    {
        Flash.color = new Color(Flash.color.r, Flash.color.g, Flash.color.b, 0f);
        FlashLight.SetActive(false);
        _photoTakenTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        TriggerDeath.SetActive(false);
        Trigger.SetActive(false);
    }

    void Update()
    {
        if (IsActive)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                var focal = gameObject.GetComponent<DepthOfField>().focalLength;
                //focal += Input.GetAxis("Mouse ScrollWheel") * Sensitivity;
                
            }
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

            if (Input.GetKeyDown(KeyCode.T))
                _flashActivated = !_flashActivated;
        }

        if (IsActive == false)
            Camera.fieldOfView = NormalFov;
    }

    IEnumerator WaitToPhotograph()
    {
        if (_flashActivated)
        {
            yield return new WaitForSeconds(0.2f);
            Flash.DOFade(1, 0.2f);
            yield return new WaitForSeconds(0.2f);
            Flash.DOFade(0, 0.2f);
            yield return new WaitForSeconds(0.5f);
            FlashLight.SetActive(false);
        }
        _photoCount++;
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
        _takePhoto = false;
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
        if (_flashActivated)
            FlashLight.SetActive(true);
        Rect regionToCapture = new Rect(0, 0, 500, 500);

        _photoTakenTexture = new Texture2D(500, 500, TextureFormat.RGB24, false);

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
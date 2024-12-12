using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Rendering;
using System.IO;

public class Photography : MonoBehaviour
{

    public bool IsActive = false;
    public bool Triggered = false;
    public Camera Camera;
    public Image Flash;
    [SerializeField] private int _photoToTake;
    private int _photoCount;
    private bool _takePhoto = false;


    void Start()
    {
        Flash.color = new Color(Flash.color.r, Flash.color.g, Flash.color.b, 0f);
    }

    void Update()
    {
        if (IsActive && Input.GetMouseButtonDown(0))
        {
            if (Triggered && _takePhoto == false)
            {
                if (_photoCount < _photoToTake)
                    TakingPhotopragy();
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

    private void TakingPhotopragy()
    {
        Debug.Log("Screenshot taken");
        _takePhoto = true;
        int width = Screen.width;
        int height = Screen.height;
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);

        // Read the screen contents into the texture
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        tex.Apply();

        // Encode the texture in JPG format
        byte[] bytes = ImageConversion.EncodeToJPG(tex);
        Object.Destroy(tex);

        // Write the returned byte array to a file in the project folder
        File.WriteAllBytes(Application.dataPath + "/../SavedScreen.jpg", bytes);
        StartCoroutine(WaitToPhotograph());
    }

}
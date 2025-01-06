using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diary : MonoBehaviour
{
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            gameObject.SetActive(false);
        }
    }
}

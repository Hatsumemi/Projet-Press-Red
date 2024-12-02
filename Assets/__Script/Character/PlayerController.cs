using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.SceneView;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void OnDrawGizmos()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvenementArea : MonoBehaviour
{
    [SerializeField] private float _timer;
    [SerializeField] private GameObject[] _areaElements;
    private bool _canStartTimer;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_canStartTimer)
        {
            _timer -= Time.deltaTime;

            if (_timer < 0 )
            {
                for (int i = 0; i < _areaElements.Length; i++)
                {
                    Destroy(_areaElements[i]);
                }
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponentInParent<PlayerController>() != null)
        {
            _canStartTimer = true;
        }
    }
}

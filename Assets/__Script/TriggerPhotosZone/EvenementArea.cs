using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvenementArea : MonoBehaviour
{
    public IntentionList AreaType;
    private IntentionList.IntentionType _originalEntityType;

    [SerializeField] private GameObject _player;

    [SerializeField] private float _timer;
    [SerializeField] private GameObject[] _areaElements;
    private bool _canStartTimer;

    [SerializeField] private GameObject _objective;
    [SerializeField] private DistanceIntention[] _distanceIntentions;

    [HideInInspector] public bool IsInEmotionalZone = false;

    [SerializeField] private IA_Pathing[] _iaPathing;

    [SerializeField] private GameObject _evidence;
    [SerializeField] private Transform _eviddencePos;
    private bool _canSpawnEvidence = false;

    // Start is called before the first frame update
    void Start()
    {
        _originalEntityType = AreaType.Type;
    }

    // Update is called once per frame
    void Update()
    {
        if (_canStartTimer)
        {
            _timer -= Time.deltaTime;

            if (_timer < 0 )
            {
                for (int y = 0; y < _iaPathing.Length; y++)
                {
                    _iaPathing[y].CanExit = true;
                }
                for (int i = 0; i < _areaElements.Length; i++)
                {
                    //Destroy(_areaElements[i]);
                    Destroy(gameObject);
                }
            }

            if (IsInEmotionalZone == false)
            {
                float _distancePlayerObjective = Vector3.Distance(_objective.transform.position, _player.transform.position);

                float _previousDistanceSelect = 1000;

                for (int i = 0; i < _distanceIntentions.Length; i++)
                {
                    if (_distancePlayerObjective <= _distanceIntentions[i]._distance && _distanceIntentions[i]._distance < _previousDistanceSelect)
                    {
                        AreaType.Type = _distanceIntentions[i].distanceIntentionType;

                        _previousDistanceSelect = _distanceIntentions[i]._distance;
                    }
                }
            }
        }

        if (_timer < 20 && _canSpawnEvidence == false)
        {
            _canSpawnEvidence = true;
            GameObject _evidenceInst = Instantiate(_evidence, _eviddencePos.transform.position, Quaternion.identity);
            Destroy(_evidenceInst, 10);
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponentInParent<PlayerController>() != null)
        {
            _canStartTimer = true;
            for (int i = 0; i < _iaPathing.Length; i++)
            {
                _iaPathing[i].CanGoObjective = true;
            }
        }
    }
}



[Serializable]
public class DistanceIntention
{
    public float _distance;
    public IntentionList.IntentionType distanceIntentionType;
}
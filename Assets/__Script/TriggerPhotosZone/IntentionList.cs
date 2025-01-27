using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class IntentionList : MonoBehaviour
{

    public EntityType Type;
    public enum EntityType
    {
        None,
        Sensational,
        Emotional,
        Informative,
        Shocking,
        Inspiring,
        Demonstrative,
        Symbolic,
        Controversy,
        Proximity
    }
}
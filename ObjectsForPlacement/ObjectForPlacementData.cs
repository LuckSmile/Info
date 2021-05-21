using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectForPlacementData : ScriptableObject
{
    public Sprite BodySprite => bodySprite;
    [SerializeField] private Sprite bodySprite = null;
    /// <summary>
    /// Удача (от 1 до 100)
    /// </summary>
    public uint Chance => chance;
    [SerializeField] [Range(1, 100)] private uint chance = 1;
}

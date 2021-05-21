using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "CurrencyData", menuName ="Data/Currency")]
public class CurrencyData : ScriptableObject
{
    public Sprite Icon => icon;
    [SerializeField] private Sprite icon = null;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class CurrencySubtype
{
    public CurrencyData Selected => selected;
    [SerializeField] private CurrencyData selected = null;
    public CurrencyData Subtype => subtype;
    [SerializeField] private CurrencyData subtype = null;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "FigurineData", menuName = "Data/Figurine/Prefab")]
public class FigurineData : ObjectForPlacementData
{
    public int Significance => significance;
    [SerializeField] private int significance = 0;
    public Currency[] AdditionalCurrencies => additionalCurrencies;
    [SerializeField] private Currency[] additionalCurrencies;
    public Currency[] Currencies => currencies;
    [SerializeField] private Currency[] currencies = null;
    public FigurineData Child => child;
    [SerializeField] private FigurineData child = null;
    public FigurineInteractions[] Interactions => interactions;
    [SerializeField] private FigurineInteractions[] interactions = null;
}

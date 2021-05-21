using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Player : MonoBehaviour
{
    public IReadOnlyDictionary<CurrencyData, Currency> Currencies => currencies;
    private Dictionary<CurrencyData, Currency> currencies = null;
    [SerializeField] private Currency[] dataCurrencies = null;
    private void Awake()
    {
        currencies = new Dictionary<CurrencyData, Currency>();
        dataCurrencies.ToList().ForEach((x) => currencies.Add(x.Data, x));
    }
}

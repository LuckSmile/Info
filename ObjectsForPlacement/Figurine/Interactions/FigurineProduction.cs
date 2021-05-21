using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
[CreateAssetMenu(fileName = "FigurineProduction", menuName = "Data/Figurine/Interactions/Production")]
public class FigurineProduction : FigurineInteractions
{
    [SerializeField] private CurrencySubtype[] currencies = null;
    public override bool CheckingUse(Cell selected, GameManager gameManager)
    {
        return true;
    }
    public override bool Use(Cell selected, GameManager gameManager)
    {
        Figurine figurine = selected.Selected;
        FigurineData data = figurine.Data;

        data.AdditionalCurrencies.ToList().ForEach(
            x => 
            {
                for(int index = 0; index < currencies.Length; index++)
                {
                    CurrencySubtype currency = currencies[index];
                    if(currency.Subtype == x.Data)
                    {
                        GameManager.Player.Currencies[currency.Selected].Amount += x.Amount;
                        break;
                    }
                }
            });

        return true;
    }
}

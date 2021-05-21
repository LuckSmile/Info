using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[CreateAssetMenu(fileName = "SubjectCollection", menuName = "Data/Subject/Interactions/Collection")]
public class SubjectCollection : SubjectInteractions
{
    [SerializeField] private CurrencySubtype[] currencies = null;
    public override bool CheckingUse((Cell cell, Subject subject) selected, GameManager gameManager) => selected.cell.Selected != null;
    public override void Use((Cell cell, Subject subject) selected, GameManager gameManager)
    {        
        if(CheckingUse(selected, gameManager))
        {
            Figurine figurine = selected.cell.Selected;
            FigurineData data = figurine.Data;

            data.Currencies.ToList().ForEach(
                x =>
                {
                    if(currencies.Select(y => y.Selected).Contains(x.Data) == false)
                    {
                        GameManager.Player.Currencies[x.Data].Amount += x.Amount;
                    }
                });

            data.AdditionalCurrencies.ToList().ForEach(
                x =>
                {
                    for (int index = 0; index < currencies.Length; index++)
                    {
                        CurrencySubtype currency = currencies[index];
                        if (currency.Subtype == x.Data)
                        {
                            GameManager.Player.Currencies[currency.Selected].Amount += x.Amount;
                            break;
                        }
                    }
                });
            Destroy(figurine.gameObject);
            selected.cell.Selected = null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class Figurine : ObjectForPlacement<FigurineData>
{
    public bool Interaction
    {
        get
        {
            return InteractionsColmlited.Count != Data.Interactions.Length;
        }
    }
    public List<FigurineInteractions> InteractionsColmlited { get; private set; } = new List<FigurineInteractions>();
    public void Destroy()
    {
        //GetComponentInParent<Cell>().Selected = null;
        FigurineData data = this.Data;
        if(data.Currencies != null && data.Currencies.Length > 0)
            data.Currencies.ToList().ForEach(x => GameManager.Player.Currencies[x.Data].Amount += x.Amount);
        Destroy(this.gameObject);
    }
}

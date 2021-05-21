using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
[CreateAssetMenu(fileName = "FigurineGive", menuName = "Data/Figurine/Interactions/Give")]
public class FigurineGive : FigurineInteractions
{
    [SerializeField] private List<ObjectForPlacementData> objectForPlacementData = null;
    public override bool CheckingUse(Cell selected, GameManager gameManager)
    {
        return true;
    }

    public override bool Use(Cell selected, GameManager gameManager)
    {
        objectForPlacementData.ForEach(x => gameManager.SpawnObjectForPlacement.AddQueue(x));
        selected.Selected.Destroy();
        return true;
    }
}

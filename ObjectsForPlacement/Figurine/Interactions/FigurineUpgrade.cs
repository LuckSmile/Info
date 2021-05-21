using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
[CreateAssetMenu(fileName = "FigurineUpgrade", menuName = "Data/Figurine/Interactions/Upgrade")]
public class FigurineUpgrade : FigurineInteractions
{
    [SerializeField] private Figurine prefab = null;
    public override bool CheckingUse(Cell selected, GameManager gameManager)
    {
        return true;
    }
    public override bool Use(Cell selected, GameManager gameManager)
    {
        selected.Selected.Destroy();
        UpgradeFigurine(selected, gameManager, prefab);
        return true;
    }
}

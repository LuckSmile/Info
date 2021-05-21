using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
[CreateAssetMenu(fileName = "FigurineUnification", menuName = "Data/Figurine/Interactions/Unification")]
public class FigurineUnification : FigurineInteractions
{
    [SerializeField] private int amount = 0;
    [SerializeField] private Figurine prefab = null;
    private readonly List<Cell> unification = new List<Cell>();
    public override bool CheckingUse(Cell selected, GameManager gameManager)
    {
        return SearchNeighborsCell(selected, gameManager.Cells).Count >= amount;
    }
    public override bool Use(Cell selected, GameManager gameManager)
    {
        List<Cell> neighborsCell = new List<Cell>();
        neighborsCell.AddRange(SearchNeighborsCell(selected, gameManager.Cells));
        if(neighborsCell.Count >= amount && unification.Contains(selected) == false)
        {
            unification.AddRange(neighborsCell);

            selected.Selected.Destroy();
            unification.Remove(selected);

            UpgradeFigurine(selected, gameManager, prefab);
        }
        
        if(unification.Contains(selected))
        {
            selected.Selected.Destroy();
            unification.Remove(selected);
        }
        
        return true;
    }
}

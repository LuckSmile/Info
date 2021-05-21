using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
[CreateAssetMenu(fileName = "FigurineImpossibleToMove", menuName = "Data/Figurine/Interactions/ImpossibleToMove")]

public class FigurineImpossibleToMove : FigurineInteractions
{
    [SerializeField] private Figurine prefab = null;
    [Space]
    [SerializeField] private int amount;
    private readonly List<Cell> unification = new List<Cell>();
    public override bool CheckingUse(Cell selected, GameManager gameManager)
    {
        bool impossibleToMove = true;
        IReadOnlyList<Cell> cells = SearchNeighborsCell(selected, gameManager.Cells);
        for (int index = 0; index < cells.Count; index++)
        {
            Cell cell = cells[index];
            if (SearchEmptyCell(cell, gameManager.Cells).Count > 0)
            {
                impossibleToMove = false;
                break;
            }
        }
        return impossibleToMove;
    }
    public override bool Use(Cell selected, GameManager gameManager)
    {
        bool impossibleToMove = true;
        IReadOnlyList<Cell> cells = SearchNeighborsCell(selected, gameManager.Cells);
        for(int index = 0; index < cells.Count; index++)
        {
            Cell cell = cells[index];
            if(SearchEmptyCell(cell, gameManager.Cells).Count > 0)
            {
                impossibleToMove = false;
                break;
            }
        }
        if (impossibleToMove == true)
        {
            if (cells.Count >= amount && unification.Contains(selected) == false)
            {
                unification.AddRange(cells);

                selected.Selected.Destroy();
                unification.Remove(selected);

                UpgradeFigurine(selected, gameManager, prefab);
            }
            else if(cells.Count < amount)
            {
                selected.Selected.Destroy();
                UpgradeFigurine(selected, gameManager, prefab);
            }

            if(unification.Contains(selected))
            {
                selected.Selected.Destroy();
                unification.Remove(selected);
            }
        }
        return true;
    }
}

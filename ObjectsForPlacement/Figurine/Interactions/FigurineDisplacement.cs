using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
[CreateAssetMenu(fileName = "FigurineDisplacement", menuName = "Data/Figurine/Interactions/Displacement")]
public class FigurineDisplacement : FigurineInteractions
{
    private IEnumerator AnimDisplacement(Cell oldLocation, Cell newLocation)
    {
        Figurine figurine = oldLocation.Selected;
        this.SetFigurine(oldLocation: oldLocation, newLocation: newLocation, false);

        while(Vector2.Distance(Vector2.zero, figurine.transform.localPosition) > 0f)
        {
            figurine.transform.localPosition = Vector2.Lerp(Vector2.zero, figurine.transform.localPosition, 0.5f);
            yield return new WaitForFixedUpdate();
        }
        figurine.transform.localPosition = Vector2.zero;
    }
    public override bool CheckingUse(Cell selected, GameManager gameManager)
    {
        List<Cell> neighborsCell = new List<Cell>();
        neighborsCell.AddRange(SearchNeighborsCell(selected, gameManager.Cells));
        //Debug.Log(selected.Index);

        if (neighborsCell.Count > 1)
        {
            Dictionary<Cell, int> emptyCountCells = new Dictionary<Cell, int>();
            neighborsCell.ForEach(x =>
            {
                if (x.Selected.Interaction == true)
                {
                    int count = SearchEmptyCell(x, gameManager.Cells).Count;
                    if (count > 0)
                    {
                        emptyCountCells.Add(x, count);
                    }
                }
            });
            //foreach (KeyValuePair<Cell, int> keyValuePair in emptyCountCells)
            //{
            //    Debug.LogError("A:" + keyValuePair.Key.Index + " " + keyValuePair.Value);
            //}
            emptyCountCells.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            //foreach (KeyValuePair<Cell, int> keyValuePair in emptyCountCells)
            //{
            //    Debug.LogError("B:" + keyValuePair.Key.Index + " " + keyValuePair.Value);
            //}
            if (emptyCountCells.Count > 0)
            {
                Cell minEmptyCountCell = emptyCountCells.Keys.ToArray()[0];
                if (emptyCountCells.ContainsKey(selected) && emptyCountCells[minEmptyCountCell] == emptyCountCells[selected])
                {
                    return true;
                }
                return false;
            }
            return true;
        }
        return true;
    }
    public override bool Use(Cell selected, GameManager gameManager)
    {
        List<Cell> neighborsCell = new List<Cell>();
        neighborsCell.AddRange(SearchNeighborsCell(selected, gameManager.Cells));
        //Debug.Log(selected.Index);

        if (neighborsCell.Count > 1)
        {
            Dictionary<Cell, int> emptyCountCells = new Dictionary<Cell, int>();
            neighborsCell.ForEach(x =>
            {
                if (x.Selected.Interaction == true)
                {
                    int count = SearchEmptyCell(x, gameManager.Cells).Count;
                    if (count > 0)
                    {
                        emptyCountCells.Add(x, count);
                    }
                }
            });
            //foreach (KeyValuePair<Cell, int> keyValuePair in emptyCountCells)
            //{
            //    Debug.LogError("A:" + keyValuePair.Key.Index + " " + keyValuePair.Value);
            //}
            emptyCountCells.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            //foreach (KeyValuePair<Cell, int> keyValuePair in emptyCountCells)
            //{
            //    Debug.LogError("B:" + keyValuePair.Key.Index + " " + keyValuePair.Value);
            //}
            if (emptyCountCells.Count > 0)
            {
                Cell minEmptyCountCell = emptyCountCells.Keys.ToArray()[0];
                if (emptyCountCells.ContainsKey(selected) && emptyCountCells[minEmptyCountCell] == emptyCountCells[selected])
                {
                    this.Displacement(selected, gameManager.Cells);
                    return true;
                }
                return false;
            }
            return true;
        }
        this.Displacement(selected, gameManager.Cells);
        return true;
    }
    private void Displacement(Cell selected, IReadOnlyDictionary<Vector2, Cell> cells)
    {
        Cell[] emptyCells = SearchEmptyCell(selected, cells).ToArray();
        if (emptyCells.Length > 0)
        {
            //Debug.LogWarning("MOVE:" + selected.Index);
            Cell newLocation = emptyCells[Random.Range(0, emptyCells.Length)];
            Figurine figurine = selected.Selected;
            figurine.StartCoroutine(AnimDisplacement(oldLocation: selected, newLocation: cells[newLocation.Index]));
        }
    }
}

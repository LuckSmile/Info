using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public abstract class FigurineInteractions : ScriptableObject
{
    public bool ActivateOnDrop => activateOnDrop;
    [SerializeField] private bool activateOnDrop = false;
    public bool Use(Vector2 selectedIndex, GameManager gameManager) => Use(gameManager.Cells[selectedIndex], gameManager);
    public abstract bool Use(Cell selected, GameManager gameManager);
    public abstract bool CheckingUse(Cell selected, GameManager gameManager);
    /// <summary>
    /// Поиск пустых клеток
    /// </summary>
    /// <param name="selected"></param>
    /// <param name="cells"></param>
    /// <returns></returns>
    protected IReadOnlyList<Cell> SearchEmptyCell(Cell selected, IReadOnlyDictionary<Vector2, Cell> cells)
    {
        List<Cell> answer = new List<Cell>();
        int[] direction = new int[] { -1, 0, 1 };
        for (int y = 0; y < direction.Length; y++)
        {
            for (int x = 0; x < direction.Length; x++)
            {
                if (direction[x] != 0 && direction[y] != 0)
                    continue;
                else if (direction[x] == 0 && direction[y] == 0)
                    continue;

                Vector2 indexDirty = selected.Index + new Vector2(direction[x], direction[y]);
                if (cells.ContainsKey(indexDirty) == true)
                {
                    Cell cellDirty = cells[indexDirty];
                    if(cellDirty.Selected == null)
                    {
                        answer.Add(cellDirty);
                    }
                }
            }
        }
        return answer;
    }
    /// <summary>
    /// Поиск всех соседей
    /// </summary>
    /// <param name="selected"></param>
    /// <param name="cells"></param>
    /// <returns></returns>
    protected IReadOnlyList<Cell> SearchNeighborsCell(Cell selected, IReadOnlyDictionary<Vector2, Cell> cells)
    {
        List<Cell> blackList = new List<Cell>();
        List<Cell> search = new List<Cell>
        {
            selected
        };
        while (search.Count > 0)
        {
            blackList.AddRange(search);

            List<Cell> oldSearch = new List<Cell>();
            oldSearch.AddRange(search);
            search.Clear();

            oldSearch.ForEach(x => search.AddRange(SearchSimilarCell(x, cells)));
            search = search.Where(x => blackList.Contains(x) == false).ToList();
        }
        return blackList.Distinct().ToList();
    }
    /// <summary>
    /// Поиск соседей рядом
    /// </summary>
    /// <param name="selected"></param>
    /// <param name="cells"></param>
    /// <returns></returns>
    protected IReadOnlyList<Cell> SearchSimilarCell(Cell selected, IReadOnlyDictionary<Vector2, Cell> cells)
    {
        List<Cell> answer = new List<Cell>();
        int[] direction = new int[] { -1, 0, 1 };
        for (int y = 0; y < direction.Length; y++)
        {
            for (int x = 0; x < direction.Length; x++)
            {
                if (direction[x] != 0 && direction[y] != 0)
                    continue;
                else if (direction[x] == 0 && direction[y] == 0)
                    continue;

                Vector2 indexDirty = selected.Index + new Vector2(direction[x], direction[y]);
                if (cells.ContainsKey(indexDirty) == true)
                {
                    Cell cellDirty = cells[indexDirty];
                    if (cellDirty.Selected != null && cellDirty.Selected.Data == selected.Selected.Data)
                    {
                        answer.Add(cellDirty);
                    }
                }
            }
        }
        return answer;
    }
    /// <summary>
    /// Поиск всех фигурок рядом
    /// </summary>
    /// <param name="selected"></param>
    /// <param name="cells"></param>
    /// <returns></returns>
    protected IReadOnlyList<Cell> SearchFigurines(Cell selected, IReadOnlyDictionary<Vector2, Cell> cells)
    {
        List<Cell> answer = new List<Cell>();
        int[] direction = new int[] { -1, 0, 1 };
        for (int y = 0; y < direction.Length; y++)
        {
            for (int x = 0; x < direction.Length; x++)
            {
                if (direction[x] != 0 && direction[y] != 0)
                    continue;
                else if (direction[x] == 0 && direction[y] == 0)
                    continue;

                Vector2 indexDirty = selected.Index + new Vector2(direction[x], direction[y]);
                if (cells.ContainsKey(indexDirty) == true)
                {
                    Cell cellDirty = cells[indexDirty];
                    if (cellDirty.Selected != null)
                    {
                        answer.Add(cellDirty);
                    }
                }
            }
        }
        return answer;
    }
    protected void SetFigurine(Cell oldLocation, Cell newLocation, bool setPosition = true)
    {
        Figurine figurine = oldLocation.Selected;
        oldLocation.Selected = null;
        newLocation.Selected = figurine;
        
        if(setPosition)
        {
            figurine.transform.localPosition = Vector2.zero;
        }
    }
    /// <summary>
    /// Обновить ячейку (Обновляеться на дочерний объект child)
    /// </summary>
    /// <param name="selected">Ячейка где находиться фигурка</param>
    /// <param name="gameManager"></param>
    /// <param name="prefab"></param>
    protected void UpgradeFigurine(Cell selected, GameManager gameManager, Figurine prefab)
    {
        if (selected.Selected.Data.Child != null)
        {
            Cell cell = selected;
            Figurine figurine = Figurine.Create(prefab, cell.GetComponent<RectTransform>(), selected.Selected.Data.Child) as Figurine;
            cell.Selected = figurine;
            figurine.GetComponent<ControlDrag>().Interaction = false;
            for (int index = 0; index < figurine.Data.Interactions.Length; index++)
            {
                FigurineInteractions interaction = figurine.Data.Interactions[index];
                if (interaction.ActivateOnDrop == true)
                {
                    interaction.Use(selected, gameManager);
                }
            }
        }
    }
}

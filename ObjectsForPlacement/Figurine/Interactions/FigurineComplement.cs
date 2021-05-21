using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
[CreateAssetMenu(fileName = "FigurineComplement", menuName = "Data/Figurine/Interactions/Complement")]
public class FigurineComplement : FigurineInteractions
{
    [SerializeField] private Figurine prefab = null;
    [SerializeField] private FigurineInteractions[] conditionForExecutions = null;
    [SerializeField] private FigurineInteractions notCompleted = null;
    public override bool CheckingUse(Cell selected, GameManager gameManager)
    {
        Cell cell = selected;
        Figurine oldFigurine = cell.Selected;
        IReadOnlyList<Cell> cells = SearchFigurines(selected, gameManager.Cells);
        if (cells.Count > 0)
        {
            cells = cells.Where(x => (x.Selected.Data != selected.Selected)).ToList();
            if (cells.Count > 0)
            {
                FigurineData[] datas = cells.OrderBy(x => -x.Selected.Data.Significance).Select(x => x.Selected.Data).ToArray();
                for (int indexData = 0; indexData < datas.Length; indexData++)
                {
                    FigurineData data = datas[indexData];
                    for (int indexInteraction = 0; indexInteraction < conditionForExecutions.Length; indexInteraction++)
                    {
                        FigurineInteractions conditionForExecution = conditionForExecutions[indexInteraction];
                        if (data.Interactions.Contains(conditionForExecution) == true)
                        {
                            Figurine figurine = Figurine.Create(prefab, cell.GetComponent<RectTransform>(), data) as Figurine;
                            cell.Selected = figurine;
                            if (conditionForExecution.CheckingUse(selected, gameManager) == true)
                            {
                                figurine.GetComponent<ControlDrag>().Interaction = false;
                                for (int index = 0; index < data.Interactions.Length; index++)
                                {
                                    FigurineInteractions interaction = data.Interactions[index];
                                    if (interaction.ActivateOnDrop == true)
                                    {
                                        interaction.Use(selected, gameManager);
                                    }
                                }
                                Destroy(figurine.gameObject);
                                cell.Selected = oldFigurine;
                                return true;
                            }
                            else
                            {
                                Destroy(figurine.gameObject);
                                cell.Selected = oldFigurine;
                            }
                        }
                    }
                }
            }
        }
        return false;
    }
    public override bool Use(Cell selected, GameManager gameManager)
    {   
        Cell cell = selected;
        Figurine oldFigurine = cell.Selected;
        IReadOnlyList<Cell> cells = SearchFigurines(selected, gameManager.Cells);
        if(cells.Count > 0)
        {
            cells = cells.Where(x => (x.Selected.Data != selected.Selected)).ToList();
            if(cells.Count > 0)
            {
                FigurineData[] datas = cells.OrderBy(x => -x.Selected.Data.Significance).Select(x => x.Selected.Data).ToArray();
                for (int indexData = 0; indexData < datas.Length; indexData++)
                {
                    FigurineData data = datas[indexData];
                    for (int indexInteraction = 0; indexInteraction < conditionForExecutions.Length; indexInteraction++)
                    {
                        FigurineInteractions conditionForExecution = conditionForExecutions[indexInteraction];
                        if(data.Interactions.Contains(conditionForExecution) == true)
                        {
                            Figurine figurine = Figurine.Create(prefab, cell.GetComponent<RectTransform>(), data) as Figurine;
                            cell.Selected = figurine;
                            if (conditionForExecution.CheckingUse(selected, gameManager) == true)
                            {
                                figurine.GetComponent<ControlDrag>().Interaction = false;
                                for (int index = 0; index < data.Interactions.Length; index++)
                                {
                                    FigurineInteractions interaction = data.Interactions[index];
                                    if (interaction.ActivateOnDrop == true)
                                    {
                                        interaction.Use(selected, gameManager);
                                    }
                                }
                                oldFigurine.Destroy();
                                return true;
                            }
                            else
                            {
                                Destroy(figurine.gameObject);
                                cell.Selected = oldFigurine;
                            }
                        }
                    }
                }
            }
        }
        notCompleted.Use(selected, gameManager);
        return true;
    }
}

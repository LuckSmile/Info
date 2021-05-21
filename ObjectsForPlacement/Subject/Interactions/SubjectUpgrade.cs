using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
[CreateAssetMenu(fileName = "SubjectUpgrade", menuName = "Data/Subject/Interactions/Upgrade")]
public class SubjectUpgrade : SubjectInteractions
{
    [SerializeField] private FigurineData[] figurineDatas = null;
    [SerializeField] private Figurine prefab = null;
    public override bool CheckingUse((Cell cell, Subject subject) selected, GameManager gameManager)
    {
        Debug.Log(selected.cell.Selected != null && figurineDatas.Contains(selected.cell.Selected.Data) == true);
        return selected.cell.Selected != null && figurineDatas.Contains(selected.cell.Selected.Data) == true;
    }
    public override void Use((Cell cell, Subject subject) selected, GameManager gameManager)
    {
        if (CheckingUse(selected, gameManager))
        {
            Figurine oldFigurine = selected.cell.Selected;
            FigurineData data = oldFigurine.Data;
            if (data.Child != null)
            {
                Cell cell = selected.cell;
                Figurine figurine = Figurine.Create(prefab, cell.GetComponent<RectTransform>(), data.Child) as Figurine;
                cell.Selected = figurine;
                figurine.GetComponent<ControlDrag>().Interaction = false;
            }

            Destroy(oldFigurine.gameObject);
        }
    }
}

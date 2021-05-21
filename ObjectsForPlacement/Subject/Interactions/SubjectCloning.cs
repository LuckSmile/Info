using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SubjectCloning", menuName = "Data/Subject/Interactions/Cloning")]
public class SubjectCloning : SubjectInteractions
{
    [SerializeField] private bool createNewFigurine = false;
    public override bool CheckingUse((Cell cell, Subject subject) selected, GameManager gameManager) => selected.cell.Selected != null;
    public override void Use((Cell cell, Subject subject) selected, GameManager gameManager)
    {
        if(CheckingUse(selected, gameManager))
        {
            Figurine figurine = selected.cell.Selected;
            FigurineData data = figurine.Data;
            gameManager.SpawnObjectForPlacement.AddQueue(data);
            if(createNewFigurine == false)
            {
                Destroy(figurine.gameObject);
            }
        }
    }
}

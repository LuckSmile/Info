using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
[CreateAssetMenu(fileName = "FigurineTeleportation", menuName = "Data/Figurine/Interactions/Teleportation")]
public class FigurineTeleportation : FigurineInteractions
{
    public override bool CheckingUse(Cell selected, GameManager gameManager)
    {
        IReadOnlyList<Cell> availableSeats = gameManager.Cells.Values.ToArray().Where(x => x.Selected == null).ToList();
        return availableSeats.Count > 0;
    }
    public override bool Use(Cell selected, GameManager gameManager)
    {
        IReadOnlyList<Cell> availableSeats = gameManager.Cells.Values.ToArray().Where(x => x.Selected == null).ToList();
        if(availableSeats.Count > 0)
        {
            Vector2 newLocation = availableSeats[Random.Range(0, availableSeats.Count)].Index;
            this.SetFigurine(selected, gameManager.Cells[newLocation]);
        }
        return true;
    }
}

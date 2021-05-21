using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Accessories", menuName = "Data/Accessories")]
public class Accessories : ObjectForPlacementData
{
    public FigurineData Figurine => figurine;
    [SerializeField] private FigurineData figurine = null;
    public SubjectData Subject => subject;
    [SerializeField] private SubjectData subject = null; 
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SubjectData", menuName = "Data/Subject/Prefab")]
public class SubjectData : ObjectForPlacementData
{
    public TypesCompleted TypeCompleted => typeCompleted;
    [SerializeField] private TypesCompleted typeCompleted = TypesCompleted.All;

    public SubjectInteractions[] Interactions => interactions;
    [SerializeField] private SubjectInteractions[] interactions = null;
    public enum TypesCompleted
    {
        OneCompleted,
        NecessarilyCompleteds,
        All
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SubjectInteractions : ScriptableObject
{
    public abstract void Use((Cell cell, Subject subject) selected, GameManager gameManager);
    public abstract bool CheckingUse((Cell cell, Subject subject) selected, GameManager gameManager);
}

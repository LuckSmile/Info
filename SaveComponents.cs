using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SaveComponents<T> : MonoBehaviour
{
    public abstract void OnSave(T component);
}
public abstract class SaveComponentsCell : SaveComponents<Cell> { }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UICounter : MonoBehaviour
{
    [SerializeField] private Text textAmount = null;
    public uint Amount 
    { 
        get => amount; 
        set
        {
            amount = value;
            textAmount.text = "" + amount;
        }
    }
    private uint amount = 0;
}

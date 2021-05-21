using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public abstract class ObjectForPlacement<TData> : MonoBehaviour where TData : ObjectForPlacementData
{
    [SerializeField] protected Image body = null;
    public TData Data { get; private set; }
    public static ObjectForPlacement<TData> Create(ObjectForPlacement<TData> prefab, RectTransform parent, TData data)
    {
        ObjectForPlacement<TData> answer = Instantiate(prefab, parent);
        answer.Data = data;
        answer.body.sprite = answer.Data.BodySprite;
        return answer;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Image))]
[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(CanvasGroup))]
public class ControlDrag : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{

    [SerializeField] protected bool interaction = true;
    [SerializeField] protected Canvas canvas = null;
    protected RectTransform rectTransform = null;
    protected CanvasGroup canvasGroup = null;
    protected Image image = null;
    public bool Interaction
    {
        get => interaction;
        set
        {
            if (value == false)
            {
                canvasGroup.alpha = 1f;
                canvasGroup.blocksRaycasts = true;
                rectTransform.localPosition = new Vector3(0f, 0f, 0f);
            }
            image.raycastTarget = value;
            interaction = value;
        }
    }
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        image = GetComponent<Image>();
    }
    public virtual void OnDrop(PointerEventData eventData) { }
    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        if(interaction == true)
        {
            canvasGroup.alpha = .6f;
            canvasGroup.blocksRaycasts = false;
        }
    }
    public virtual void OnDrag(PointerEventData eventData)
    {
        if(interaction == true)
        {
            rectTransform.position = eventData.position + new Vector2(0, rectTransform.rect.height);
        }
    }
    public virtual void OnEndDrag(PointerEventData eventData)
    {
        if (interaction == true)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
            rectTransform.localPosition = new Vector3(0f, 0f, 0f);
        }
    }
}

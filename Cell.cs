using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Cell : MonoBehaviour, IDropHandler
{
    public Figurine Selected
    {
        get => selected;
        set
        {
            selected = value;
            if(selected != null)
            {
                selected.transform.SetParent(this.transform);
            }
        }
    }
    [SerializeField] private Figurine selected = null;
    public Vector2 Index => index;
    [SerializeField] private Vector2 index = Vector2.zero;
    [SerializeField] private bool offset = false;
    public event System.Action OnDropFigureActivete;
    public event System.Action<Cell, Subject> OnDropSubjectActivete;
    private Image image = null;
    public static Cell Create(Cell prefab, RectTransform parent, Vector2 index)
    {
        Cell answer = Instantiate(prefab, parent);
        answer.image = answer.GetComponent<Image>();
        answer.index = index;
        return answer;
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            if (eventData.pointerDrag.TryGetComponent<ControlDrag>(out ControlDrag controlDrag))
            {
                if (controlDrag.Interaction == true)
                {
                    if(this.transform.childCount == 0 || (this.GetComponentInChildren<Subject>() == null && controlDrag.GetComponent<Subject>() != null))
                    {
                        if (offset == false)
                        {
                            controlDrag.Interaction = false;
                            if (Selected == null)
                            {
                                if (controlDrag.TryGetComponent<Figurine>(out Figurine figurine))
                                {
                                    Selected = figurine;
                                }
                            }
                            if (this.GetComponentInChildren<Subject>() == null)
                            {
                                if (controlDrag.TryGetComponent<Subject>(out Subject subject))
                                {
                                    OnDropSubjectActivete?.Invoke(this, subject);
                                }
                            }
                            else
                            {
                                return;
                            }
                        }
                        controlDrag.transform.SetParent(transform);
                        controlDrag.transform.localPosition = Vector3.zero;
                        if (Selected != null)
                        {
                            OnDropFigureActivete?.Invoke();
                        }
                        //if (this.GetComponentInChildren<Subject>() == null)
                        //{
                        //    if (controlDrag.TryGetComponent<Subject>(out Subject subject))
                        //    {
                        //        OnDropSubjectActivete?.Invoke(this, subject);
                        //    }
                        //}
                    }
                }
            }
        }
    }
}

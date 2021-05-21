using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Placeholder : MonoBehaviour
{
    [SerializeField] private Canvas canvas = null;
    [SerializeField] private GridLayoutGroup content = null;
    [SerializeField] private Cell prefab = null;
    [Space]
    [SerializeField] private Vector2Int constraint = Vector2Int.zero;
    [SerializeField] private List<SaveComponentsCell> saveComponents = null;
    private void Awake()
    {
        float sizeX = (canvas.pixelRect.size.x / constraint.x) / canvas.scaleFactor;
        float sizeY = (canvas.pixelRect.size.y / constraint.y) / canvas.scaleFactor;
        float size = sizeX > sizeY ? sizeY : sizeX;
        size = (float)System.Math.Truncate(size);

        content.cellSize = new Vector2(size - 4f, size);
        content.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        content.constraintCount = constraint.x;

        RectTransform thisRectTransform = this.GetComponent<RectTransform>();
        thisRectTransform.sizeDelta = new Vector2(canvas.pixelRect.size.x / canvas.scaleFactor, size * constraint.y + 5f);
    }
    private void Start()
    {
        RectTransform parent = content.GetComponent<RectTransform>();
        for (int y = 0; y < constraint.y; y++)
        {
            for (int x = 0; x < constraint.x; x++)
            {
                Cell cell = Cell.Create(prefab, parent, new Vector2(x, y));
                saveComponents.ForEach(component => component.OnSave(cell));
            }
        }
    }
}

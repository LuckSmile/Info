using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class SpawnObjectForPlacement : MonoBehaviour
{
    [SerializeField] private Figurine prefabFigurine = null;
    [SerializeField] private Subject prefabSubject = null;
    [Space]
    [SerializeField] private RectTransform content = null;
    [Space]
    [SerializeField] private ObjectForPlacementData[] objectsForPlacementData = null;
    [Space]
    [SerializeField] private Player player = null;
    [SerializeField] private CurrencyData step = null;
    [SerializeField] private GameManager gameManager = null;
    private List<ObjectForPlacementData> objectsRepeat = null;

    public event System.Action<Figurine> CreateFigurine;
    public event System.Action<Subject> CreateSubject;
    private void Awake()
    {
        objectsRepeat = new List<ObjectForPlacementData>();
    }
    private void Update()
    {
        if (objectsRepeat.Count > 0 && content.childCount == 0)
        {
            Create(objectsRepeat[0]);
            objectsRepeat.RemoveAt(0);
        }
        if (content.childCount == 0 && player.Currencies[step].Amount > 0)
        {
            while(true)
            {
                for(int index = 0; index < objectsForPlacementData.Length; index++)
                {
                    int indexRandomNumber = Random.Range(0, objectsForPlacementData.Length);
                    ObjectForPlacementData temp = objectsForPlacementData[index];
                    objectsForPlacementData[index] = objectsForPlacementData[indexRandomNumber];
                    objectsForPlacementData[indexRandomNumber] = temp;
                }

                ObjectForPlacementData objectForPlacementData = objectsForPlacementData[Random.Range(0, objectsForPlacementData.Length)];
                int randomNumber = Random.Range(0, 100);
                if (randomNumber <= objectForPlacementData.Chance)
                {
                    if (objectForPlacementData is SubjectData && gameManager.Cells.Where(x => x.Value.Selected != null).Count() > 0)
                    {
                        Create(objectForPlacementData);
                        player.Currencies[step].Amount--;
                        break;
                    }
                    else if(objectForPlacementData is FigurineData)
                    {
                        Create(objectForPlacementData);
                        player.Currencies[step].Amount--;
                        break;
                    }
                    else if (objectForPlacementData is  Accessories)
                    {
                        Accessories accessories = objectForPlacementData as Accessories;
                        if(gameManager.Cells.Where(x => x.Value.Selected != null).Select(x => x.Value.Selected.Data).Contains(accessories.Figurine))
                        {
                            Create(accessories.Subject);
                            player.Currencies[step].Amount--;
                            break;
                        }
                        else
                        {
                            Create(accessories.Figurine);
                            player.Currencies[step].Amount--;
                            break;
                        }
                    }
                }
            }
        }
    }
    private void Create(ObjectForPlacementData data)
    {
        if (data is FigurineData)
        {
            Figurine figurine = Figurine.Create(prefabFigurine, content, data as FigurineData) as Figurine;
            CreateFigurine?.Invoke(figurine);
        }
        else if (data is SubjectData)
        {
            Subject subject = Subject.Create(prefabSubject, content, data as SubjectData) as Subject;
            CreateSubject?.Invoke(subject);
        }
    }
    /// <summary>
    /// Добавить в очередь
    /// </summary>
    /// <param name="objectForPlacement"></param>
    public void AddQueue(ObjectForPlacementData objectForPlacement)
    {
        if (content.childCount > 0)
        {
            ClearContent<FigurineData>();
            ClearContent<SubjectData>();
        }
        Create(objectForPlacement);
    }
    private void ClearContent<TData>() where TData : ObjectForPlacementData
    {
        ObjectForPlacement<TData> obj = content.GetComponentInChildren<ObjectForPlacement<TData>>();
        if(obj != null)
        {
            ObjectForPlacementData dataRepeat = obj.Data;
            this.objectsRepeat.Add(dataRepeat);
            Destroy(obj.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public RuntimeSet_GameObject AllEntities;
    public SelectionGrid selectionGrid;
    // Start is called before the first frame update
    public TextMeshPro HelpText;
    private GameObject selectedEntity;
    void Start()
    {
        Invoke("StartTurn", 0.01f);    
    }
    private void InitUnitSelect()
    {
        SetCanSelectEntities(true);
        selectedEntity = null;
        HelpText.text = "Select a unit";
    }
    private void InitSelectMove()
    {
        HelpText.text = "Choose position to move to";
        selectionGrid.EnableGridForMove(new Vector2Int((int)selectedEntity.transform.position.x, (int)selectedEntity.transform.position.y), 5, GetEntityPositions());
    }
    private void InitSelectTarget()
    {
        HelpText.text = "Select attack target";
    }
    public static Vector2 TileToWorldPos(Vector2Int tilePos)
    {
        return new Vector2(tilePos.x + 0.5f, tilePos.y + 0.5f);
    }
    public static Vector2Int WorldToTilePos(Vector2 worldPos)
    {
        return new Vector2Int((int)(worldPos.x + 0.0f), (int)(worldPos.y + 0.0f));
    }
    private void StartTurn()
    {
        foreach (GameObject entity in PlayerOwnedEntities())
        {
            entity.GetComponent<Clickable>().OnClicked.AddListener(OnEntityClicked);
        }
        selectionGrid.gridSelectedEvent.AddListener(OnGridClicked);
        InitUnitSelect();
    }
    private List<GameObject> PlayerOwnedEntities()
    {
        List<GameObject> list = new List<GameObject>();
        foreach (GameObject entity in AllEntities.Items)
        {
            if (entity.GetComponent<EntityBehavior>().PlayerOwned)
            {
                list.Add(entity);
            }
        }
        return list;

    }
    private void OnGridClicked(Vector2Int pos)
    {
        UnselectAll();
        HelpText.text = "";
        //selectedEntity.transform.position = new Vector3(pos.x + 0.5f, pos.y + 0.5f, -0.1f);
        selectedEntity.GetComponent<EntityBehavior>().MoveToPos(pos, selectionGrid.passableMap(GetEntityPositions()));
        selectedEntity.GetComponent<EntityBehavior>().FinishAnimation.AddListener(OnEntityFinishMoving);
        //selectedEntity = null;
        selectionGrid.EnableGrid(false);
        SetCanSelectEntities(false);
    }
    private void OnEntityFinishMoving()
    {
        selectedEntity.GetComponent<EntityBehavior>().FinishAnimation.RemoveAllListeners();
        InitUnitSelect();
    }

    private List<Vector2Int> GetEntityPositions()
    {
        List<Vector2Int> list = new List<Vector2Int>();
        foreach (GameObject entity in AllEntities.Items)
        {
            list.Add(new Vector2Int((int)entity.transform.position.x, (int)entity.transform.position.y));
        }
        return list;
    }
    private void OnEntityClicked(GameObject eventSource)
    {
        selectedEntity = eventSource;

        UnselectAll();
        eventSource.GetComponent<Selectable>().SetIsSelected(true);
        InitSelectMove();
    }
    private void SetCanSelectEntities(bool val)
    {
        foreach (GameObject entity in PlayerOwnedEntities())
        {
            entity.GetComponent<Clickable>().enabled = val;
        }
    }
    private void UnselectAll()
    {
        foreach (GameObject entity in AllEntities.Items)
        {
            entity.GetComponent<Selectable>().SetIsSelected(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class GridSelectedEvent : UnityEvent<Vector2Int> { };

public class SelectionGrid : MonoBehaviour
{
    public GridSelectedEvent gridSelectedEvent = new GridSelectedEvent();
    public Tilemap tileMap;

    public void EnableGrid(bool enable)
    {
        foreach(GameObject ob in GetAllTileObjects())
        {
            if(ob != null)
            {
                ob.SetActive(enable);
            }
        }
    }
    public void EnableGridForMove(Vector2Int start, int maxMove, List<Vector2Int> invalidPositions, List<Vector2Int> restrictToPos = null)
    {
        enablePositions(PassableMapFromPoint(start, invalidPositions, restrictToPos, maxMove));
    }

    private void enablePositions(bool[,] spaces)
    {
        BoundsInt bounds = tileMap.cellBounds;
        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                GameObject ob = tileMap.GetInstantiatedObject(new Vector3Int(x, y, 0));
                ob?.SetActive(spaces[x, y]);
            }
        }
    }

    private bool[,] PassableMapFromPoint(Vector2Int start, List<Vector2Int> invalidPositions, List<Vector2Int> restrictToPos, int maxMove)
    {
        bool[,] map = passableMap(invalidPositions);
        bool[,] finalMap = passableMap(invalidPositions);
        //restrict length
        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                if (map[x, y])
                {
                    PathSearch p = new PathSearch(map, start, new Vector2Int(x, y));
                    List<Vector2Int> path = p.GetPath();
                    int length = path.Count;
                    if(length == 0 || length > maxMove)
                    {
                        finalMap[x, y] = false;
                    }
                }
            }
        }

        //restrictToPos
        if(restrictToPos != null)
        {
            //restrict length
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    bool found = false;
                    foreach(Vector2Int pos in restrictToPos)
                    {
                        if(pos.x == x && pos.y == y)
                        {
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        finalMap[x, y] = false;
                    }
                }
            }
        }
        return finalMap;
    }

    public bool[,] passableMap(List<Vector2Int> invalidPositions)
    {
        //will break if tilemap has tiles at < 0 x or y
        BoundsInt bounds = tileMap.cellBounds;
        bool[,] map = new bool[bounds.xMax, bounds.yMax];
        for (int x = 0; x < bounds.xMax; x++)
        {
            for (int y = 0; y < bounds.yMax; y++)
            {
                SpecialTile ob = tileMap.GetTile(new Vector3Int(x, y, 0)) as SpecialTile;
                if(ob != null && ob.passable)
                {
                    if(!invalidPositions.Contains(new Vector2Int(x, y)))
                    {
                        map[x, y] = true;
                    }
                }
            }
        }
        return map;
    }

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Setup",0.1f);
    }

    public void Setup()
    {
        EnableGrid(false);
        TileBase[] tileList = tileMap.GetTilesBlock(tileMap.cellBounds);
        Debug.Log(tileList.Length);

        BoundsInt bounds = tileMap.cellBounds;
        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                GameObject ob = tileMap.GetInstantiatedObject(new Vector3Int(x,y,0));
                if(ob == null)
                {
                    continue;
                }
                OnClickEvent clickEvent = ob.GetComponent<Clickable>().OnClicked;
                clickEvent.AddListener(TileClicked);
            }
        }
    }
    private List<GameObject> GetAllTileObjects()
    {
        List<GameObject> list = new List<GameObject>();
        BoundsInt bounds = tileMap.cellBounds;
        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                GameObject ob = tileMap.GetInstantiatedObject(new Vector3Int(x, y, 0));
                list.Add(ob);
            }
        }
        return list;
    }
    public void TileClicked(GameObject src)
    {
        BoundsInt bounds = tileMap.cellBounds;
        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                GameObject gameObject = tileMap.GetInstantiatedObject(new Vector3Int(x, y, 0));

                if (gameObject == src)
                {
                    Debug.Log("clicked " + x + " " + y);
                    gridSelectedEvent.Invoke(new Vector2Int(x, y));
                    return;
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

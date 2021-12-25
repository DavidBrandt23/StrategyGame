using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EntityBehavior : MonoBehaviour
{
    public UnityEvent FinishAnimation = new UnityEvent();
    public bool PlayerOwned;
    public void MoveToPos(Vector2Int pos, bool[,] map)
    {
        StartCoroutine(MoveToTargetPos(pos, map));
    }

    IEnumerator MoveToTargetPos(Vector2Int pos, bool[,] map)
    {
        Vector3 curPos = transform.position;
        Vector2Int start = GameLogic.WorldToTilePos(new Vector2(curPos.x, curPos.y));

        Debug.Log("starting from: " + start.x +"," + start.y);

        PathSearch p = new PathSearch(map, start, pos);
        List<Vector2Int> path = p.GetPath();

        float speed = 0.02f;
        while (path.Count > 0)
        {
            Vector2Int nextPos = path[0];
            Vector2 targetWorldPos = GameLogic.TileToWorldPos(nextPos);
            while (transform.position.x < targetWorldPos.x)
            {
                curPos = transform.position;
                float newX = transform.position.x + speed;
                transform.position = new Vector3(newX, curPos.y, curPos.z);
                if (newX < targetWorldPos.x)
                {
                    newX = targetWorldPos.x;
                }
                yield return null;
            }
            while (transform.position.x > targetWorldPos.x)
            {
                curPos = transform.position;
                float newX = transform.position.x - speed;
                transform.position = new Vector3(newX, curPos.y, curPos.z);
                if (newX > targetWorldPos.x)
                {
                    newX = targetWorldPos.x;
                }
                yield return null;
            }
            while (transform.position.y < targetWorldPos.y)
            {
                curPos = transform.position;
                float newY = transform.position.y + speed;
                if (newY > targetWorldPos.y)
                {
                    newY = targetWorldPos.y;
                }
                transform.position = new Vector3(curPos.x, newY, curPos.z);
                yield return null;
            }
            while (transform.position.y > targetWorldPos.y)
            {
                curPos = transform.position;
                float newY = transform.position.y - speed;
                if (newY < targetWorldPos.y)
                {
                    newY = targetWorldPos.y;
                }
                transform.position = new Vector3(curPos.x, newY, curPos.z);
                yield return null;
            }
            path.Remove(nextPos);
        }
        FinishAnimation.Invoke();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

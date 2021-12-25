using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemberOfSetsBehavior : MonoBehaviour
{
    public List<RuntimeSet_GameObject> Sets;

    // Start is called before the first frame update
    void OnEnable()
    {
        foreach(RuntimeSet_GameObject set in Sets)
        {
            set.Add(gameObject);
        }
    }

    private void OnDisable()
    {
        foreach (RuntimeSet_GameObject set in Sets)
        {
            set.Remove(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

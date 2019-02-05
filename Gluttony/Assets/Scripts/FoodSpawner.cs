using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject foodPrefab;
    private List<Transform> spawns = new List<Transform>();
    private int index;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            spawns.Add(transform.GetChild(i));
        }
        index =0;
        SpawnNewFood();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnNewFood()
    {

        index %= spawns.Count;
        GameObject go = spawns[index].gameObject;
        Debug.Log("index: " + index);
        go.SetActive(true);
        index++;
    }
}

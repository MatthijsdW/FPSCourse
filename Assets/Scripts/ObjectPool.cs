using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject objPrefab;
    public int createOnStart;

    private List<GameObject> pooledObjs = new List<GameObject>();

    private void Start()
    {
        for (int x = 0; x < createOnStart; x++)
        {
            CreateNewObject();
        }
    }

    private GameObject CreateNewObject()
    {
        GameObject obj = Instantiate(objPrefab);
        obj.SetActive(false);
        pooledObjs.Add(obj);

        return obj;
    }

    public GameObject GetObject()
    {
        GameObject obj = pooledObjs.Find(x => !x.activeInHierarchy);
        if (obj == null)
            obj = CreateNewObject();
        obj.SetActive(true);

        return obj;
    }

    private void OnDestroy()
    {
        foreach (GameObject obj in pooledObjs)
        {
            Destroy(obj);
        }
    }
}

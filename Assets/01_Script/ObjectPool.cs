using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    public List<GameObject> pooledObject = new List<GameObject>();
    public int amountToPool = 20;

    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject coinParent;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(coinPrefab);
            obj.transform.SetParent(coinParent.transform,false);
            obj.SetActive(false);
            pooledObject.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObject.Count; i++)
        {
            if (!pooledObject[i].activeInHierarchy)
            {
                return pooledObject[i];
            }
        }

        return null;
    }
}

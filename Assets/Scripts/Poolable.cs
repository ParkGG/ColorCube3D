using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]   
public class Poolable {

    public int poolCount = 0;              //생성할 오브젝트 수
    public int ID = -1;                    //각 Pool들간의 고유 ID  (PoolManager LIst에서 Push,Pop 할 때 구분하기 위한 용도)
    public GameObject poolPref;     //Pool 프리팹

    List<GameObject> poolList = new List<GameObject>();

   

    public void Initialize(Transform parents = null)
    {
        for (int count = 0; count < poolCount; ++count)
        {
            poolList.Add(CreateItem(parents));
        }
    }


    GameObject CreateItem(Transform parent = null)
    {
        GameObject item = Object.Instantiate(poolPref);
        item.transform.SetParent(parent);
        item.SetActive(false);

        return item;
    }


    public void PushToPool(GameObject item, Transform parent = null)
    {
        item.transform.SetParent(parent);
        item.SetActive(false);
        poolList.Add(item);

    }



    public GameObject PopFromPool(Transform parent = null)
    {
        if (poolList.Count == 0)
        {
            poolList.Add(CreateItem(parent));
        }

        GameObject item = poolList[0];
        poolList.RemoveAt(0);

        return item;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : SingleTon<PoolManager> {

    public List<Poolable> objectPool = new List<Poolable>();

    public Transform[] parents;     //Pool 생성 시 부모 객체

    
    private void Start()
    {
        Init();
    }

    public void Init()
    {
        for (int ix = 0; ix < objectPool.Count; ++ix)
        {
            objectPool[ix].Initialize(parents[ix]);
        }
    }


    public bool PushToPool(int ID, GameObject item, Transform parent = null)
    {
        Poolable pool = GetPoolItem(ID);

        if (pool == null)
        {
            return false;
        }

        pool.PushToPool(item, parent == null ? transform : parent);
        return true;
    }


    public GameObject PopFromPool(int ID, Transform parent = null)
    {
        Poolable pool = GetPoolItem(ID);

        if(pool == null)
        {
            return null;
        }

        return pool.PopFromPool(parent);
    }




    Poolable GetPoolItem(int ID)
    {
        for(int ix = 0; ix<objectPool.Count; ++ix)
        {
            if (objectPool[ix].ID == ID)
            {
                return objectPool[ix];
            }
        }

        return null;
    }
}

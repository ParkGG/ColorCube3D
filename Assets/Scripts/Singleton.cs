﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SingleTon<T> : MonoBehaviour where T : MonoBehaviour {

    private static T _instance;


	public static T Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType(typeof(T)) as T;

                if(_instance == null)
                {
                    Debug.LogError("There is no active" + typeof(T) + " in this scene");
                }

               
            }
           
            
            return _instance;
        }
    }

   
    private void OnDestroy()
    {
        _instance = null;
    }
}

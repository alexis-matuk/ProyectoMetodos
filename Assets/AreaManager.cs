﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class AreaManager : MonoBehaviour {   
    public int totalUsers;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
        
    public void addCleaningTime(float time)
    {
        Transform me = transform.Find("ItemsContainer").transform.Find("ItemsPanel").transform;
        for (int i = 0; i < me.childCount; i++)
        {
            me.GetChild(i).GetComponent<User>().addWaitingTime(time);
            me.GetChild(i).GetComponent<User>().addPartialWaitingTime(gameObject.name, time);
        }
    }

    public void addToList(GameObject user)
    {            
        user.transform.SetParent(transform.Find("ItemsContainer").transform.Find("ItemsPanel").transform, true);
        user.transform.localScale = new Vector2(1,1);
        float time = VariableManager.instance.getServiceRate(transform.tag);
        user.GetComponent<User>().addWaitingTime(time);
        user.GetComponent<User>().addPartialWaitingTime(gameObject.name, time);
        user.GetComponent<User>().startTimer();
        user.GetComponent<User>().setNextTarget("Caja");
    }       
}

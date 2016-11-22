using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ExitStore : MonoBehaviour {
    public void addToList(GameObject user)
    {        
        if (user.GetComponent<User>() != null)
        {
            user.GetComponent<User>().writeData();
        }
        Destroy(user);
        user.transform.SetParent(transform.Find("ItemsContainer").transform.Find("ItemsPanel").transform, true);
        user.transform.localScale = new Vector2(1,1);
    }   
}

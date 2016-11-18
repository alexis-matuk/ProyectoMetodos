using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class QueueManager : MonoBehaviour {    
    Queue<GameObject> queue = new Queue<GameObject>();
    GameObject userInFront;
    public int totalUsers;
    float totalWaitSeconds;
    // Use this for initialization
    void Start () {
        userInFront = null;
        totalWaitSeconds = 0;
        totalUsers = 0;
    }

    // Update is called once per frame
    void Update () {
        if (queue.Count > 0)
        {            
            if (userInFront == null)
            {                
                userInFront = queue.Peek();
                float time = VariableManager.instance.getServiceRate(transform.name);
                if(gameObject.name.Contains("Caja") || gameObject.name.Contains("Embolsadora"))
                {
                    User u = userInFront.GetComponent<User>();
                    u.addWaitingTime(time*u.numberOfItems);
                }
                else
                    userInFront.GetComponent<User>().addWaitingTime(time);
                userInFront.GetComponent<User>().startTimer();
            }
            else
            {
                if (userInFront.GetComponent<User>().waitingTime <= 0)
                {                                        
                    if (transform.name.Contains("Caja"))
                    {
                        userInFront.GetComponent<User>().setNextTarget("Embolsado");
                    }
                    else if (transform.name.Contains("Embolsado"))
                    {
                        userInFront.GetComponent<User>().setNextTarget("Salida");
//                        Destroy(userInFront);
                    }
                    else
                    {
                        userInFront.GetComponent<User>().makeDecision();
                    }
                    queue.Dequeue();
                    totalUsers--;
                    userInFront = null;
                }
            }
        }
        else
        {
            userInFront = null;
            if (transform.GetChild(0).transform.GetChild(0).childCount > 0)
            {                
                for (int i = 0; i < transform.GetChild(0).transform.GetChild(0).childCount; i++)
                {
                    if (transform.name.Contains("Caja"))
                    {                        
                        transform.GetChild(0).transform.GetChild(0).GetChild(i).GetComponent<User>().setNextTarget("Embolsado");
                        transform.GetChild(0).transform.GetChild(0).GetChild(i).GetComponent<User>().done = true;
                    }
                    else if (transform.name.Contains("Embolsado"))
                    {                        
                        transform.GetChild(0).transform.GetChild(0).GetChild(i).gameObject.GetComponent<User>().setNextTarget("Salida");
//                        Destroy(transform.GetChild(0).transform.GetChild(0).GetChild(i).gameObject);
                    }
                    else
                    {                        
                        transform.GetChild(0).transform.GetChild(0).GetChild(i).GetComponent<User>().makeDecision();
                    }
                }
            }                
        }           
    }

    public int getTotalUsers()
    {        
        return totalUsers;
    }

    public void addToList(GameObject user)
    {       
        queue.Enqueue(user);
        totalUsers++;
        user.transform.SetParent(transform.Find("ItemsContainer").transform.Find("ItemsPanel").transform, true);
        user.transform.localScale = new Vector2(1,1);
    }   
}

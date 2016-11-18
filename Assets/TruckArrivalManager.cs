using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TruckArrivalManager : MonoBehaviour {
    Queue<GameObject> trucks = new Queue<GameObject>();
    [SerializeField]
    GameObject downloadManager;
    GameObject currentTruck;
    public int totalTrucks;
    bool unloading;
	// Use this for initialization
	void Start () {        
        totalTrucks = 0;
        currentTruck = null;
        unloading = false;
	}
	
	// Update is called once per frame
	void Update () {        
        if (trucks.Count > 0 && !downloadManager.GetComponent<DescargaManager>().isUnloading)
        {    
            currentTruck = trucks.Peek();
            currentTruck.GetComponent<Truck>().setNextTarget("Descarga");
            unloading = true;
            downloadManager.GetComponent<DescargaManager>().isUnloading = true;
        }

        if (currentTruck != null && currentTruck.GetComponent<Truck>().waitingTime <= 0)
        {                
            trucks.Dequeue();
            currentTruck = null;
            totalTrucks--;
            unloading = false;
        }
	}

    public void addToList(GameObject truck)
    {               
        trucks.Enqueue(truck);
        totalTrucks++;
        truck.transform.SetParent(transform.Find("ItemsContainer").transform.Find("ItemsPanel").transform, true);
        truck.transform.localScale = new Vector2(1,1);
    } 
}

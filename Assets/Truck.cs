using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Truck : MonoBehaviour {
    public float waitingTime;
    string nextTargetString;
    public int id;
    GameObject nextTarget;
    VariableManager manager;
    ShowStats stats;
    bool moving;
    List<string> places = new List<string>();
	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Button>().onClick.AddListener( () => {selectButton();});
        nextTarget = null;
        moving = false;
        nextTargetString = "";
        manager = VariableManager.instance;
        id = manager.asignNewTruckId();
        transform.GetChild(0).GetComponent<Text>().text = id.ToString();
        setNextTarget("Llegada_Camiones");
	}

    public void addWaitingTime(float seconds)
    {        
        waitingTime += seconds;
    }
        
    public void startTimer()
    {
        if (!moving)
            StartCoroutine(timer());
    }

    IEnumerator timer()
    {
        moving = true;
        while(waitingTime > 0)//esperar "time" segundos y después activar la funcionalidad principal
        {
            waitingTime -= Time.deltaTime * VariableManager.instance.timeMultiplier;//aumentar Time.deltaTime a la variable temporal tTime
            yield return null;//yield necesario para la corrutina
        }
        if (waitingTime < 0)
            waitingTime = 0;
        moving = false;
        //Acaba
    }

    public void setNextTarget(string target)
    {        
        nextTargetString = target;
    }

	
	// Update is called once per frame
	void Update () {
        if (nextTargetString != "" && !moving)
        {                       
            nextTarget = GameObject.Find(nextTargetString);
            TransitionManager.instance.moveToArea(this.gameObject, nextTarget.name, VariableManager.instance.movementSpeed);
            nextTargetString = "";
            Debug.Log("moving truck");
        }
	}

    public void selectButton()
    {        
        VariableManager.instance.setLastButtonPressed(this.gameObject);
    }
}

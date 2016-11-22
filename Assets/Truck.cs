using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class Truck : MonoBehaviour {
    public float waitingTime;
    string nextTargetString;
    public int id;
    GameObject nextTarget;
    VariableManager manager;
    ShowStats stats;
    bool moving;
    float totalWaitingTime;
    List<string> places = new List<string>();
    Dictionary<string, float> waitingTimes = new Dictionary<string, float>();

    float arrivalTime;
	// Use this for initialization
	void Start () {

        waitingTimes.Add("Llegada_Camiones", 0);
        waitingTimes.Add("Descarga", 0);
        arrivalTime = 0;
        totalWaitingTime = 0;
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
        totalWaitingTime += seconds;
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
        }
	}

    public void selectButton()
    {        
        VariableManager.instance.setLastButtonPressed(this.gameObject);
    }

    public void addPartialWaitingTime(string name, float _time)
    {        
        waitingTimes[name] += _time;
    }

    public void setArrivalTime(float _time)
    {
        arrivalTime = _time;
    }

    public void setArrivalWaitingTime(float _leavingTime)
    {
        float waited = _leavingTime - arrivalTime;
        totalWaitingTime += waited;
        waitingTimes["Llegada_Camiones"] += waited;
    }

    public void writeData()
    {
        TimeSpan actualTime = TimeSpan.FromSeconds(VariableManager.instance.secondsElapsed);              
        string text = "\n";
        text += "[Truck]" + "\n";
        text += "[" + string.Format("{3:d}d:{0:D2}h:{1:D2}m:{2:D2}s", actualTime.Hours, actualTime.Minutes, actualTime.Seconds, actualTime.Days) + "]" + "\n";
        text += "Truck id: " + id.ToString() + "\n";
        TimeSpan tiempoLlegada = TimeSpan.FromSeconds(waitingTimes["Llegada_Camiones"]);
        TimeSpan tiempoDescarga = TimeSpan.FromSeconds(waitingTimes["Descarga"]);
        TimeSpan tiempoTotal = TimeSpan.FromSeconds(totalWaitingTime);
        text += "Tiempo en Llegada " + string.Format("{3:d}d:{0:D2}h:{1:D2}m:{2:D2}s", tiempoLlegada.Hours, tiempoLlegada.Minutes, tiempoLlegada.Seconds, tiempoLlegada.Days) + "\n";
        text += "Tiempo en Descarga " + string.Format("{3:d}d:{0:D2}h:{1:D2}m:{2:D2}s", tiempoDescarga.Hours, tiempoDescarga.Minutes, tiempoDescarga.Seconds, tiempoDescarga.Days) + "\n";
        text += "Tiempo total en colas: " + string.Format("{3:d}d:{0:D2}h:{1:D2}m:{2:D2}s", tiempoTotal.Hours, tiempoTotal.Minutes, tiempoTotal.Seconds, tiempoTotal.Days) + "\n";
        FileWriter.instance.writeLine(text);
    }
}

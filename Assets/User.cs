using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class User : MonoBehaviour {

	// Use this for initialization
    public bool FarmaciaBool, SalchichasBool, AtencionClienteBool, CarnesBool, CompraBool = false;
    public int id = 0;
    public float waitingTime;
    public int numberOfItems;
    public float totalWaitingTime;
    string nextTargetString;
    int numberOfTransitions;
    GameObject nextTarget;
    VariableManager manager;
    UnityRandom urand;
    ShowStats stats;
    public bool done;
    List<string> places = new List<string>();
    Dictionary<string, float> waitingTimes = new Dictionary<string, float>();

    void Awake()
    {
        gameObject.GetComponent<Button>().onClick.AddListener( () => {selectButton();});
    }         

    void Start () {  
        waitingTimes.Add("Farmacia", 0);
        waitingTimes.Add("Salchichas", 0);
        waitingTimes.Add("Atencion_Cliente", 0);
        waitingTimes.Add("Carnes", 0);
        waitingTimes.Add("Compras", 0);
        waitingTimes.Add("Caja", 0);
        waitingTimes.Add("Embolsadora", 0);              

        numberOfTransitions = 0;
        done = false;
        numberOfItems = 0;
        nextTargetString = "";    
        nextTarget = null;
        manager = VariableManager.instance;
        id = manager.asignNewId();
        urand = new UnityRandom();
        waitingTime = 0;
        float Farmacia = urand.Value();
        float Salchichas = urand.Value();
        float AtencionCliente = urand.Value();
        float Carnes = urand.Value();
        float Compra = urand.Value();
        if (Farmacia <= manager.ProbabilidadFarmacia)
        {
            FarmaciaBool = true;
            places.Add("Farmacia");
        }
        if (Salchichas <= manager.ProbabilidadSalchichoneria)
        {
            SalchichasBool = true;
            places.Add("Salchichas");
        }
        if (AtencionCliente <= manager.ProbabilidadServicioCliente)
        {
            AtencionClienteBool = true;
            places.Add("Atencion_Cliente");
        }
        if (Carnes <= manager.ProbabilidadCarnes)
        {
            CarnesBool = true;
            places.Add("Carnes");
        }
        if (Compra <= manager.ProbabilidadCompras)
        {
            CompraBool = true;
            numberOfItems = manager.getNormalDistribution("Compra");
        }
        transform.GetChild(0).GetComponent<Text>().text = id.ToString();
        makeDecision();
	}
        
    public void setNextTarget(string target)
    {        
        nextTargetString = target;
    }

    public void addWaitingTime(float seconds)
    {        
        waitingTime += seconds;
        totalWaitingTime += waitingTime;
    }

    bool hasNoQueues()
    {
        return !FarmaciaBool && !SalchichasBool && !AtencionClienteBool && !CarnesBool;
    }      

    public void makeDecision()
    {
        string lower = getLowerQueue();
        if (lower != "")
        {
            places.Remove(lower);
            TransitionManager.instance.moveToArea(this.gameObject, lower, VariableManager.instance.movementSpeed);
        }
        else
        {
            if (CompraBool)
                TransitionManager.instance.moveToArea(this.gameObject, "Compras", VariableManager.instance.movementSpeed);
            else
            {
                TransitionManager.instance.moveToArea(this.gameObject, "Salida", VariableManager.instance.movementSpeed);
//                Destroy(this.gameObject);
            }
        }
    }

    string getLowerQueue()
    {
        if (!hasNoQueues() && places.Count > 0)
        {
            int lower = GameObject.Find(places[0]).GetComponent<QueueManager>().totalUsers;
            int pos = 0;
            for (int i = 1; i < places.Count; i++)
            {
                int current = GameObject.Find(places[i]).GetComponent<QueueManager>().totalUsers;
                if (current < lower)
                {
                    lower = current;
                    pos = i;
                }
            }
            return places[pos];
        }
        else
            return "";
    }

    void Update()
    {        
        if (done)
        {
            if (nextTargetString != "")
            {          
                if (nextTargetString.Contains("Caja"))
                {
                    GameObject cajas = GameObject.Find("Cajas");
                    int lower = cajas.transform.GetChild(0).GetComponent<QueueManager>().getTotalUsers();
                    int place = 0;
                    for (int i = 1; i < cajas.transform.childCount; i++)
                    {
                        int current = cajas.transform.GetChild(i).GetComponent<QueueManager>().getTotalUsers();
                        if (current < lower)
                        {
                            lower = current;
                            place = i;
                        }
                    }
                    nextTarget = cajas.transform.GetChild(place).gameObject;
                }
                else if (nextTargetString.Contains("Embolsado"))
                {
                    GameObject embolsadoras = GameObject.Find("Embolsadoras");
                    int lower = embolsadoras.transform.GetChild(0).GetComponent<QueueManager>().getTotalUsers();
                    int place = 0;
                    for (int i = 1; i < embolsadoras.transform.childCount; i++)
                    {
                        int current = embolsadoras.transform.GetChild(i).GetComponent<QueueManager>().getTotalUsers();
                        if (current < lower)
                        {
                            lower = current;
                            place = i;
                        }
                    }
                    nextTarget = embolsadoras.transform.GetChild(place).gameObject;
                }
                else
                {
                    nextTarget = GameObject.Find(nextTargetString);
                }
                TransitionManager.instance.moveToArea(this.gameObject, nextTarget.name, VariableManager.instance.movementSpeed);
            }
            nextTargetString = "";
            done = false;
        }
    }


    public void startTimer()
    {
        if (!done)
            StartCoroutine(timer());
    }

    IEnumerator timer()
    {
        while(waitingTime > 0)//esperar "time" segundos y después activar la funcionalidad principal
        {
            waitingTime -= Time.deltaTime * VariableManager.instance.timeMultiplier;//aumentar Time.deltaTime a la variable temporal tTime
            yield return null;//yield necesario para la corrutina
        }
        if (waitingTime < 0)
            waitingTime = 0;
        done = true;
        //Acaba
    }

    public void selectButton()
    {        
        VariableManager.instance.setLastButtonPressed(this.gameObject);
    }

    public void addTransition()
    {
        numberOfTransitions++;
    }      

    public void addToTotalWaitingTime(float _time)
    {
        totalWaitingTime += _time;
    }

    public void addPartialWaitingTime(string name, float _time)
    {
        if (name.Contains("Caja"))
        {
            waitingTimes["Caja"] += _time;
        }
        else if (name.Contains("Embolsado"))
        {
            waitingTimes["Embolsadora"] += _time;
        }
        else
        {            
            waitingTimes[name] += _time;
        }
    }

    public void writeData()
    {
        TimeSpan actualTime = TimeSpan.FromSeconds(VariableManager.instance.secondsElapsed);

        string text = "\n";
        text += "[User]" + "\n";
        text += "[" + string.Format("{3:d}d:{0:D2}h:{1:D2}m:{2:D2}s", actualTime.Hours, actualTime.Minutes, actualTime.Seconds, actualTime.Days) + "]" + "\n";
        text += "User id: " + id.ToString() + "\n";
        if(FarmaciaBool)
            text += "Visita Farmacia"+ "\n";
        if(SalchichasBool)
            text += "Visita Salchichonería"+ "\n";
        if(AtencionClienteBool)
            text += "Visita Servicio al Cliente"+ "\n";
        if(CarnesBool)
            text += "Visita Carnes"+ "\n";
        if(CompraBool)
            text += "Visita Compras"+ "\n";

        TimeSpan tiempoFarmacia = TimeSpan.FromSeconds(waitingTimes["Farmacia"]);
        TimeSpan tiempoSalchichas = TimeSpan.FromSeconds(waitingTimes["Salchichas"]);
        TimeSpan tiempoServicioCliente = TimeSpan.FromSeconds(waitingTimes["Atencion_Cliente"]);      
        TimeSpan tiempoCarnes = TimeSpan.FromSeconds(waitingTimes["Carnes"]);
        TimeSpan tiempoCompras = TimeSpan.FromSeconds(waitingTimes["Compras"]);
        TimeSpan tiempoCajas = TimeSpan.FromSeconds(waitingTimes["Caja"]);
        TimeSpan tiempoEmbolsadora = TimeSpan.FromSeconds(waitingTimes["Embolsadora"]);
        TimeSpan tiempoTotal = TimeSpan.FromSeconds(totalWaitingTime);
            
        if(FarmaciaBool)
            text += "Tiempo en Farmacia " + string.Format("{3:d}d:{0:D2}h:{1:D2}m:{2:D2}s", tiempoFarmacia.Hours, tiempoFarmacia.Minutes, tiempoFarmacia.Seconds, tiempoFarmacia.Days) + "\n";
        if(SalchichasBool)
            text += "Tiempo en Salchichas " + string.Format("{3:d}d:{0:D2}h:{1:D2}m:{2:D2}s", tiempoSalchichas.Hours, tiempoSalchichas.Minutes, tiempoSalchichas.Seconds, tiempoSalchichas.Days) + "\n";
        if(AtencionClienteBool)
            text += "Tiempo en Servicio al Cliente " + string.Format("{3:d}d:{0:D2}h:{1:D2}m:{2:D2}s", tiempoServicioCliente.Hours, tiempoServicioCliente.Minutes, tiempoServicioCliente.Seconds, tiempoServicioCliente.Days) + "\n";
        if(CarnesBool)
            text += "Tiempo en Carnes " + string.Format("{3:d}d:{0:D2}h:{1:D2}m:{2:D2}s", tiempoCarnes.Hours, tiempoCarnes.Minutes, tiempoCarnes.Seconds, tiempoCarnes.Days) + "\n";
        if (CompraBool)
        {
            text += "Artículos comprados: " + numberOfItems.ToString() + "\n";
            text += "Tiempo en Compras " + string.Format("{3:d}d:{0:D2}h:{1:D2}m:{2:D2}s", tiempoCompras.Hours, tiempoCompras.Minutes, tiempoCompras.Seconds, tiempoCompras.Days) + "\n";
            text += "Tiempo en Cajas " + string.Format("{3:d}d:{0:D2}h:{1:D2}m:{2:D2}s", tiempoCajas.Hours, tiempoCajas.Minutes, tiempoCajas.Seconds, tiempoCajas.Days) + "\n";
            text += "Tiempo en Embolsadoras " + string.Format("{3:d}d:{0:D2}h:{1:D2}m:{2:D2}s", tiempoEmbolsadora.Hours, tiempoEmbolsadora.Minutes, tiempoEmbolsadora.Seconds, tiempoEmbolsadora.Days) + "\n";
        }
        text += "Tiempo total en colas: " + string.Format("{3:d}d:{0:D2}h:{1:D2}m:{2:D2}s", tiempoTotal.Hours, tiempoTotal.Minutes, tiempoTotal.Seconds, tiempoTotal.Days) + "\n";

        FileWriter.instance.writeLine(text);
    }
}

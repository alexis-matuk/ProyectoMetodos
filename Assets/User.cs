using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class User : MonoBehaviour {

	// Use this for initialization
    public bool FarmaciaBool, SalchichasBool, AtencionClienteBool, CarnesBool, CompraBool = false;
    public int id = 0;
    public float waitingTime;
    public int numberOfItems;
    string nextTargetString;
    GameObject nextTarget;
    VariableManager manager;
    UnityRandom urand;
    ShowStats stats;
    public bool done;
    List<string> places = new List<string>();
    void Awake()
    {
        gameObject.GetComponent<Button>().onClick.AddListener( () => {selectButton();});
    }         

	void Start () {  
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
}

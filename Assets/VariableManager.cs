using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VariableManager : MonoBehaviour {

    public static VariableManager instance = null;

    int currentId = 0;
    int currentTruckId = 0;
    [HideInInspector]
    public GameObject lastButtonPressed;

    private float _ProbabilidadFarmacia = 0.5f;
    private float _ProbabilidadCarnes = 0.6f;
    private float _ProbabilidadServicioCliente = 0.25f;
    private float _ProbabilidadSalchichoneria = 0.4f;
    private float _ProbabilidadCompras = 0.8f;
    private float _arrivalRate = 10f; //5 personas por hora
    private float _truckArrivalRate = 1f; //camiones por hora
    private float _timeMultiplier = 60f; // 1 segundo = 1 minuto
    private float _ProbabilidadLimpieza = .2f; //2 por hora 

    //Service rates 
    private float _rateDescarga = 6f;
    private float _rateFarmacia = 7f;
    private float _rateSalchichas = 6f;
    private float _rateCarnes = 7f;
    private float _rateAtencionClientes = 6f;
    private float _rateEmbolsadora = 7f;
    private float _rateCaja = 6f;
    private float _rateCompras = 7f;

    private float _movementSpeed = 1f;
    private float _secondsElapsed = 0f;

    private float _totalHoursToRun = 1f;


    public float ProbabilidadFarmacia { get { return _ProbabilidadFarmacia; } set { _ProbabilidadFarmacia = value; } }
    public float ProbabilidadCarnes { get { return _ProbabilidadCarnes; } set { _ProbabilidadCarnes = value; } }
    public float ProbabilidadServicioCliente { get { return _ProbabilidadServicioCliente; } set { _ProbabilidadServicioCliente = value; } }
    public float ProbabilidadSalchichoneria { get { return _ProbabilidadSalchichoneria; } set { _ProbabilidadSalchichoneria = value; } }
    public float ProbabilidadCompras { get { return _ProbabilidadCompras; } set { _ProbabilidadCompras = value; } }
    public float arrivalRate { get { return _arrivalRate; } set { _arrivalRate = value; } }
    public float timeMultiplier { get { return _timeMultiplier; } set { _timeMultiplier = value; }}
    public float rateDescarga { get { return _rateDescarga; } set { _rateDescarga = value; } }
    public float rateFarmacia { get { return _rateFarmacia; } set { _rateFarmacia = value; }}
    public float rateSalchichas { get { return _rateSalchichas; } set { _rateSalchichas = value; }}
    public float rateCarnes { get { return _rateCarnes; } set { _rateCarnes = value; }}
    public float rateAtencionClientes { get { return _rateAtencionClientes; } set { _rateAtencionClientes = value; } }
    public float rateEmbolsadora { get { return _rateEmbolsadora; } set { _rateEmbolsadora = value; }}
    public float rateCaja { get { return _rateCaja; } set { _rateCaja = value; }}
    public float rateCompras { get { return _rateCompras; } set { _rateCompras = value; } }
    public float ProbabilidadLimpieza { get { return _ProbabilidadLimpieza; } set { _ProbabilidadLimpieza = value; } }
    public float movementSpeed { get { return _movementSpeed; } set { _movementSpeed = value; } }
    public float truckArrivalRate { get { return _truckArrivalRate; } set { _truckArrivalRate = value; } }
    public float secondsElapsed { get { return _secondsElapsed; } set { _secondsElapsed = value; } }
    public float totalHoursToRun { get { return _totalHoursToRun; } set { _totalHoursToRun = value; } }
   

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public int asignNewId()
    {
        currentId++;
        return currentId;
    }

    public int asignNewTruckId()
    {
        currentTruckId++;
        return currentTruckId;
    }

    void Update()
    {
        if (lastButtonPressed != null)
        {
            ShowStats.instance.setStats(lastButtonPressed);
        }
        else
        {
            ShowStats.instance.setStats(null);
        }
    }        

    public void setLastButtonPressed(GameObject _button)
    {
        if (lastButtonPressed != null)
        {                        
            if (_button != lastButtonPressed)
            {
                _button.GetComponent<Button>().image.color = Color.green;
                lastButtonPressed.GetComponent<Button>().image.color = Color.white;
                lastButtonPressed = _button;
            }
            else
            {                
                _button.GetComponent<Button>().image.color = Color.white;                
                lastButtonPressed = null;
            }
        }
        else
        {
            _button.GetComponent<Button>().image.color = Color.green;
            lastButtonPressed = _button;
        }
    }

    public int getNormalDistribution(string Area)
    {
        UnityRandom urand = new UnityRandom();
        switch (Area)
        {
            case "Compra":
            {
                    return (int) urand.Range(1, 100, UnityRandom.Normalization.STDNORMAL, 1.0f);
                break;
            }
        }
        return 0;
    }

    public float getServiceRate(string Area)
    {
        UnityRandom urand = new UnityRandom();
        switch (Area)
        {
            case "Descarga":
                {
                    float val = urand.Exponential(VariableManager.instance.rateDescarga);
                    if (val >= 0 && val < 0.3f)
                        return 6 * 60f;
                    else if (val >= 0.3f && val < 0.5f)
                        return 5 * 60f;
                    else if (val >= 0.5f && val < 0.7f)
                        return 4 * 60f;
                    else if (val >= 0.7f && val <= 1f)
                        return 3 * 60f;
                    break;
                }
            case "Farmacia":
                {
                    float val = urand.Exponential(VariableManager.instance.rateFarmacia);
                    if (val >= 0 && val < 0.3f)
                        return 6 * 60f;
                    else if (val >= 0.3f && val < 0.5f)
                        return 5 * 60f;
                    else if (val >= 0.5f && val < 0.7f)
                        return 4 * 60f;
                    else if (val >= 0.7f && val <= 1f)
                        return 3 * 60f;
                    break;
                }
            case "Salchichas":
                {
                    float val =  urand.Exponential(VariableManager.instance.rateSalchichas);
                    if (val >= 0 && val < 0.3f)
                        return 6 * 60f;
                    else if (val >= 0.3f && val < 0.5f)
                        return 5 * 60f;
                    else if (val >= 0.5f && val < 0.7f)
                        return 4 * 60f;
                    else if (val >= 0.7f && val <= 1f)
                        return 3 * 60f;
                    break;
                }
            case "Carnes":
                {
                    float val =  urand.Exponential(VariableManager.instance.rateCarnes);
                    if (val >= 0 && val < 0.3f)
                        return 6 * 60f;
                    else if (val >= 0.3f && val < 0.5f)
                        return 5 * 60f;
                    else if (val >= 0.5f && val < 0.7f)
                        return 4 * 60f;
                    else if (val >= 0.7f && val <= 1f)
                        return 3 * 60f;
                    break;
                }
            case "Atencion_Cliente":
                {
                    float val =  urand.Exponential(VariableManager.instance.rateAtencionClientes);
                    if (val >= 0 && val < 0.3f)
                        return 6 * 60f;
                    else if (val >= 0.3f && val < 0.5f)
                        return 5 * 60f;
                    else if (val >= 0.5f && val < 0.7f)
                        return 4 * 60f;
                    else if (val >= 0.7f && val <= 1f)
                        return 3 * 60f;
                    break;
                }
            case "Embolsadora_1":
                {
                    float val = urand.Exponential(VariableManager.instance.rateEmbolsadora);
                    return cashierRate(val);
                    break;            
                }
            case "Embolsadora_2":
                {
                    float val = urand.Exponential(VariableManager.instance.rateEmbolsadora);
                    return cashierRate(val);
                    break;
                }
            case "Embolsadora_3":
                {
                    float val = urand.Exponential(VariableManager.instance.rateEmbolsadora);
                    return cashierRate(val);
                    break;
                }
            case "Embolsadora_4":
                {
                    float val = urand.Exponential(VariableManager.instance.rateEmbolsadora);
                    return cashierRate(val);
                    break;
                }
            case "Embolsadora_5":
                {
                    float val = urand.Exponential(VariableManager.instance.rateEmbolsadora);
                    return cashierRate(val);
                    break;
                }
            case "Embolsadora_6":
                {
                    float val = urand.Exponential(VariableManager.instance.rateEmbolsadora);
                    return cashierRate(val);
                    break;
                }
            case "Embolsadora_7":
                {
                    float val = urand.Exponential(VariableManager.instance.rateEmbolsadora);
                    return cashierRate(val);
                    break;
                }
            case "Caja_1":
                {
                    float val = urand.Exponential(VariableManager.instance.rateCaja);
                    return cashierRate(val);
                    break;
                }
            case "Caja_2":
                {
                    float val = urand.Exponential(VariableManager.instance.rateCaja);
                    return cashierRate(val);
                    break;
                }
            case "Caja_3":
                {
                    float val = urand.Exponential(VariableManager.instance.rateCaja);
                    return cashierRate(val);
                    break;
                }
            case "Caja_4":
                {
                    float val = urand.Exponential(VariableManager.instance.rateCaja);
                    return cashierRate(val);
                    break;
                }
            case "Caja_5":
                {
                    float val = urand.Exponential(VariableManager.instance.rateCaja);
                    return cashierRate(val);
                    break;
                }
            case "Caja_6":
                {
                    float val = urand  .Exponential(VariableManager.instance.rateCaja);  
                    return cashierRate(val);
                    break;
                }
            case "Caja_7":
                {
                    float val = urand.Exponential(VariableManager.instance.rateCaja);
                    return cashierRate(val);
                    break;
                }
            case "Compras":
                {
                    float val = urand.Exponential(VariableManager.instance.rateCompras);

                    if (val >= 0 && val < 0.3f)
                        return 6 * 60f;
                    else if (val >= 0.3f && val < 0.5f)
                        return 5 * 60f;
                    else if (val >= 0.5f && val < 0.7f)
                        return 4 * 60f;
                    else if (val >= 0.7f && val <= 1f)
                        return 3 * 60f;

                    break;
                }
        }
        return 0;
    }

    public float getArrivalRate(float val)
    {
        UnityRandom urand = new UnityRandom();
        return urand.Possion(val);
    }

    float cashierRate(float val)
    {
        if (val >= 0 && val < 0.3f)
            return 0.2f * 60f;
        else if (val >= 0.3f && val < 0.5f)
            return 0.4f * 60f;
        else if (val >= 0.5f && val < 0.7f)
            return 0.7f * 60f;
        else if (val >= 0.7f && val <= 1f)
            return 1f * 60f;
        return 0;
    }
}

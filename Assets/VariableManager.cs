using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class VariableManager : MonoBehaviour {

    public static VariableManager instance = null;

    [HideInInspector]
    public GameObject lastButtonPressed;

    int currentId = 0;
    int currentTruckId = 0;

    private float _ProbabilidadFarmacia = 0.5f;
    private float _ProbabilidadCarnes = 0.6f;
    private float _ProbabilidadServicioCliente = 0.25f;
    private float _ProbabilidadSalchichoneria = 0.4f;
    private float _ProbabilidadCompras = 0.8f;
    private float _ProbabilidadAccidente = 0.25f;
    private float _arrivalRate = 40f; //5 personas por hora
    private float _truckArrivalRate = 1f; //camiones por hora
    private float _timeMultiplier = 60f; // 1 segundo = 1 minuto
    private int _numberOfItems = 100;
    private float _buyRateMultiplier = 1f;
    private float _cashierRateMultiplier = 1f;
    private float _packingRateMultiplier = 1f;
    private float _departmentMultiplier = 1f;
    private float _limpiezaMultiplier = 1f;
    private float _descargaMultiplier = 1f;

    //Service rates 
    private float _rateDescarga = 6f;
    private float _rateFarmacia = 7f;
    private float _rateSalchichas = 6f;
    private float _rateCarnes = 7f;
    private float _rateAtencionClientes = 6f;
    private float _rateEmbolsadora = 7f;
    private float _rateCaja = 6f;
    private float _rateCompras = 7f;
    private float _rateLimpieza = 5f;


    private float _movementSpeed = 0.5f;
    private float _secondsElapsed = 0f;

    private float _totalHoursToRun = 0.5f;
    private int _openCashiers = 5;

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
    public float movementSpeed { get { return _movementSpeed; } set { _movementSpeed = value; } }
    public float truckArrivalRate { get { return _truckArrivalRate; } set { _truckArrivalRate = value; } }
    public float secondsElapsed { get { return _secondsElapsed; } set { _secondsElapsed = value; } }
    public float totalHoursToRun { get { return _totalHoursToRun; } set { _totalHoursToRun = value; } }
    public float ProbabilidadAccidente { get { return _ProbabilidadAccidente; } set { _ProbabilidadAccidente = value; } }
    public float rateLimpieza { get { return _rateLimpieza; } set { _rateLimpieza = value; } }
    public int numberOfItems { get { return _numberOfItems; } set { _numberOfItems = value; } }
    public float buyRateMultiplier { get { return _buyRateMultiplier; } set { _buyRateMultiplier = value; } }
    public float cashierRateMultiplier { get { return _cashierRateMultiplier; } set { _cashierRateMultiplier = value; } }
    public float packingRateMultiplier { get { return _packingRateMultiplier; } set { _packingRateMultiplier = value; } }
    public float departmentMultiplier { get { return _departmentMultiplier; } set { _departmentMultiplier = value; } }
    public float limpiezaMultiplier { get { return _limpiezaMultiplier; } set { _limpiezaMultiplier = value; } }
    public float descargaMultiplier { get { return _descargaMultiplier; } set { _descargaMultiplier = value; } }
    public int openCashiers { get { return _openCashiers; } set { _openCashiers = value; } }
   
    void OnLevelWasLoaded()
    {
        currentId = 0;
        currentTruckId = 0;
        _secondsElapsed = 0f;
    }

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
        if (ShowStats.instance != null && SceneManager.GetActiveScene().name == "Grid")
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
                return (int) urand.Range(1, _numberOfItems, UnityRandom.Normalization.STDNORMAL, 1.0f);
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
            case "Limpieza":
                {
                    float val = urand.Exponential(VariableManager.instance.rateLimpieza);

                    if (val >= 0 && val < 0.3f)
                        return 10 * 60f * limpiezaMultiplier;
                    else if (val >= 0.3f && val < 0.5f)
                        return 11 * 60f * limpiezaMultiplier;
                    else if (val >= 0.5f && val < 0.7f)
                        return 13 * 60f * limpiezaMultiplier;
                    else if (val >= 0.7f && val <= 1f)
                        return 15 * 60f * limpiezaMultiplier;
                    break;
                }
            case "Descarga":
                {
                    float val = urand.Exponential(VariableManager.instance.rateDescarga);
                    if (val >= 0 && val < 0.3f)
                        return 6 * 60f * descargaMultiplier;
                    else if (val >= 0.3f && val < 0.5f)
                        return 5 * 60f * descargaMultiplier;
                    else if (val >= 0.5f && val < 0.7f)
                        return 4 * 60f * descargaMultiplier;
                    else if (val >= 0.7f && val <= 1f)
                        return 3 * 60f * descargaMultiplier;
                    break;
                }
            case "Farmacia":
                {
                    float val = urand.Exponential(VariableManager.instance.rateFarmacia);
                    if (val >= 0 && val < 0.3f)
                        return 6 * 60f * departmentMultiplier;
                    else if (val >= 0.3f && val < 0.5f)
                        return 5 * 60f * departmentMultiplier;
                    else if (val >= 0.5f && val < 0.7f)
                        return 4 * 60f * departmentMultiplier;
                    else if (val >= 0.7f && val <= 1f)
                        return 3 * 60f * departmentMultiplier;
                    break;
                }
            case "Salchichas":
                {
                    float val =  urand.Exponential(VariableManager.instance.rateSalchichas);
                    if (val >= 0 && val < 0.3f)
                        return 6 * 60f * departmentMultiplier;
                    else if (val >= 0.3f && val < 0.5f)
                        return 5 * 60f * departmentMultiplier;
                    else if (val >= 0.5f && val < 0.7f)
                        return 4 * 60f * departmentMultiplier;
                    else if (val >= 0.7f && val <= 1f)
                        return 3 * 60f * departmentMultiplier;
                    break;
                }
            case "Carnes":
                {
                    float val =  urand.Exponential(VariableManager.instance.rateCarnes);
                    if (val >= 0 && val < 0.3f)
                        return 6 * 60f * departmentMultiplier;
                    else if (val >= 0.3f && val < 0.5f)
                        return 5 * 60f * departmentMultiplier;
                    else if (val >= 0.5f && val < 0.7f)
                        return 4 * 60f * departmentMultiplier;
                    else if (val >= 0.7f && val <= 1f)
                        return 3 * 60f * departmentMultiplier;
                    break;
                }
            case "Atencion_Cliente":
                {
                    float val =  urand.Exponential(VariableManager.instance.rateAtencionClientes);
                    if (val >= 0 && val < 0.3f)
                        return 6 * 60f * departmentMultiplier;
                    else if (val >= 0.3f && val < 0.5f)
                        return 5 * 60f * departmentMultiplier;
                    else if (val >= 0.5f && val < 0.7f)
                        return 4 * 60f * departmentMultiplier;
                    else if (val >= 0.7f && val <= 1f)
                        return 3 * 60f * departmentMultiplier;
                    break;
                }
            case "Embolsadora_1":
                {
                    float val = urand.Exponential(VariableManager.instance.rateEmbolsadora);
                    return packingRate(val);
                    break;            
                }
            case "Embolsadora_2":
                {
                    float val = urand.Exponential(VariableManager.instance.rateEmbolsadora);
                    return packingRate(val);
                    break;
                }
            case "Embolsadora_3":
                {
                    float val = urand.Exponential(VariableManager.instance.rateEmbolsadora);
                    return packingRate(val);
                    break;
                }
            case "Embolsadora_4":
                {
                    float val = urand.Exponential(VariableManager.instance.rateEmbolsadora);
                    return packingRate(val);
                    break;
                }
            case "Embolsadora_5":
                {
                    float val = urand.Exponential(VariableManager.instance.rateEmbolsadora);
                    return packingRate(val);
                    break;
                }
            case "Embolsadora_6":
                {
                    float val = urand.Exponential(VariableManager.instance.rateEmbolsadora);
                    return packingRate(val);
                    break;
                }
            case "Embolsadora_7":
                {
                    float val = urand.Exponential(VariableManager.instance.rateEmbolsadora);
                    return packingRate(val);
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
                        return 6 * 60f * buyRateMultiplier;
                    else if (val >= 0.3f && val < 0.5f)
                        return 5 * 60f * buyRateMultiplier;
                    else if (val >= 0.5f && val < 0.7f)
                        return 4 * 60f * buyRateMultiplier;
                    else if (val >= 0.7f && val <= 1f)
                        return 3 * 60f * buyRateMultiplier;

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
            return 0.2f * 60f * cashierRateMultiplier;
        else if (val >= 0.3f && val < 0.5f)
            return 0.4f * 60f * cashierRateMultiplier;
        else if (val >= 0.5f && val < 0.7f)
            return 0.7f * 60f * cashierRateMultiplier;
        else if (val >= 0.7f && val <= 1f)
            return 1f * 60f * cashierRateMultiplier;
        return 0;
    }

    float packingRate(float val)
    {
        if (val >= 0 && val < 0.3f)
            return 0.05f * 60f * packingRateMultiplier;
        else if (val >= 0.3f && val < 0.5f)
            return 0.1f * 60f * packingRateMultiplier;
        else if (val >= 0.5f && val < 0.7f)
            return 0.2f * 60f * packingRateMultiplier;
        else if (val >= 0.7f && val <= 1f)
            return 0.4f * 60f * packingRateMultiplier;
        return 0;
    }
}

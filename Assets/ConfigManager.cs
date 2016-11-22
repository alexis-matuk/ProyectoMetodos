using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ConfigManager : MonoBehaviour {
    [SerializeField]
    Text flujoClientes;
    [SerializeField]
    Text numeroItems;
    [SerializeField]
    Text buyRate;
    [SerializeField]
    Slider openCashiers;
    [SerializeField]
    Text serviceRateCashiers;
    [SerializeField]
    Text packingRate;
    [SerializeField]
    Text departmentServiceRate;
    [SerializeField]
    Text farmacia;
    [SerializeField]
    Text relativeTime;
    [SerializeField]
    Text animationSpeed;
    [SerializeField]
    Text salchichas;
    [SerializeField]
    Text carnes;
    [SerializeField]
    Text servicioCliente;
    [SerializeField]
    Text limpieza;
    [SerializeField]
    Text limpiezaMultiplier;
    [SerializeField]
    Text llegadaCamiones;
    [SerializeField]
    Text descargaMultiplier;
    [SerializeField]
    Text compras;
    [SerializeField]
    Text horas;
	// Use thifs for initialization
	void Start () {
        flujoClientes.text = VariableManager.instance.arrivalRate.ToString();
        numeroItems.text = VariableManager.instance.numberOfItems.ToString();
        buyRate.text = VariableManager.instance.buyRateMultiplier.ToString();
        openCashiers.value = VariableManager.instance.openCashiers;
        serviceRateCashiers.text= VariableManager.instance.cashierRateMultiplier.ToString();
        packingRate.text= VariableManager.instance.packingRateMultiplier.ToString();
        departmentServiceRate.text = VariableManager.instance.departmentMultiplier.ToString();
        farmacia.text = VariableManager.instance.ProbabilidadFarmacia.ToString();
        relativeTime.text = VariableManager.instance.timeMultiplier.ToString();
        animationSpeed.text = VariableManager.instance.movementSpeed.ToString();
        salchichas.text = VariableManager.instance.ProbabilidadSalchichoneria.ToString();
        carnes.text = VariableManager.instance.ProbabilidadCarnes.ToString();
        servicioCliente.text = VariableManager.instance.ProbabilidadServicioCliente.ToString();
        limpieza.text = VariableManager.instance.ProbabilidadAccidente.ToString();
        limpiezaMultiplier.text = VariableManager.instance.limpiezaMultiplier.ToString();
        llegadaCamiones.text = VariableManager.instance.truckArrivalRate.ToString();
        descargaMultiplier.text = VariableManager.instance.descargaMultiplier.ToString();
        compras.text = VariableManager.instance.ProbabilidadCompras.ToString();
        horas.text = VariableManager.instance.totalHoursToRun.ToString();
	}

    public void saveSettings()
    {
        VariableManager.instance.arrivalRate = GetFloat(flujoClientes.transform.parent.GetChild(2).GetComponent<Text>().text, VariableManager.instance.arrivalRate);
        VariableManager.instance.numberOfItems = GetInt(numeroItems.transform.parent.GetChild(2).GetComponent<Text>().text, VariableManager.instance.numberOfItems);
        VariableManager.instance.buyRateMultiplier = GetFloat(buyRate.transform.parent.GetChild(2).GetComponent<Text>().text, VariableManager.instance.buyRateMultiplier);
        VariableManager.instance.cashierRateMultiplier = GetFloat(serviceRateCashiers.transform.parent.GetChild(2).GetComponent<Text>().text, VariableManager.instance.cashierRateMultiplier);
        VariableManager.instance.packingRateMultiplier = GetFloat(packingRate.transform.parent.GetChild(2).GetComponent<Text>().text, VariableManager.instance.packingRateMultiplier);
        VariableManager.instance.departmentMultiplier = GetFloat(departmentServiceRate.transform.parent.GetChild(2).GetComponent<Text>().text, VariableManager.instance.departmentMultiplier);
        VariableManager.instance.ProbabilidadFarmacia = GetFloat(farmacia.transform.parent.GetChild(2).GetComponent<Text>().text, VariableManager.instance.ProbabilidadFarmacia);
        VariableManager.instance.timeMultiplier = GetFloat(relativeTime.transform.parent.GetChild(2).GetComponent<Text>().text, VariableManager.instance.timeMultiplier);
        VariableManager.instance.movementSpeed = GetFloat(animationSpeed.transform.parent.GetChild(2).GetComponent<Text>().text, VariableManager.instance.movementSpeed);
        VariableManager.instance.ProbabilidadSalchichoneria = GetFloat(salchichas.transform.parent.GetChild(2).GetComponent<Text>().text, VariableManager.instance.ProbabilidadSalchichoneria);
        VariableManager.instance.ProbabilidadCarnes = GetFloat(carnes.transform.parent.GetChild(2).GetComponent<Text>().text, VariableManager.instance.ProbabilidadCarnes);
        VariableManager.instance.ProbabilidadServicioCliente = GetFloat(servicioCliente.transform.parent.GetChild(2).GetComponent<Text>().text, VariableManager.instance.ProbabilidadServicioCliente);
        VariableManager.instance.ProbabilidadAccidente = GetFloat(limpieza.transform.parent.GetChild(2).GetComponent<Text>().text, VariableManager.instance.ProbabilidadAccidente);
        VariableManager.instance.limpiezaMultiplier = GetFloat(limpiezaMultiplier.transform.parent.GetChild(2).GetComponent<Text>().text, VariableManager.instance.limpiezaMultiplier);
        VariableManager.instance.truckArrivalRate = GetFloat(llegadaCamiones.transform.parent.GetChild(2).GetComponent<Text>().text, VariableManager.instance.truckArrivalRate);
        VariableManager.instance.descargaMultiplier = GetFloat(descargaMultiplier.transform.parent.GetChild(2).GetComponent<Text>().text, VariableManager.instance.descargaMultiplier);
        VariableManager.instance.ProbabilidadCompras = GetFloat(compras.transform.parent.GetChild(2).GetComponent<Text>().text, VariableManager.instance.ProbabilidadCompras);
        VariableManager.instance.totalHoursToRun = GetFloat(horas.transform.parent.GetChild(2).GetComponent<Text>().text, VariableManager.instance.totalHoursToRun);
        VariableManager.instance.openCashiers = (int) openCashiers.value;
    }

    private float GetFloat(string stringValue, float def)
    {
        float result = 0f;
        if (float.TryParse(stringValue, out result))
            return result;
        else
            return def;
    }

    private int GetInt(string stringValue, int def)
    {
        int result = 0;
        if (int.TryParse(stringValue, out result))
            return result;
        else
            return def;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}

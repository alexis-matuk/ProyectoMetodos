using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TransitionManager : MonoBehaviour {  
    bool reached;

    public static TransitionManager instance = null;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);        
    }

    void OnLevelWasLoaded()
    {
        StopCoroutine("move");
    }
        

    public void moveToArea(GameObject button, string Area, float time)
    {        
        button.transform.SetParent(GameObject.Find("Canvas").transform, true);
        GameObject areaTag = GameObject.Find(Area);
        GameObject area = areaTag.transform.Find("Anchor").gameObject;
        reached = false;
        if (!reached)
        {
            StartCoroutine(move(time, button, area, Area, areaTag));
        }
    }

    public IEnumerator move(float time, GameObject button, GameObject area, string Area, GameObject areaTag)
    {
        if (button != null && area != null && areaTag != null)
        {
            Vector3 currentPos = button.transform.position;

            for (var t = 0f; t < 1; t += Time.deltaTime / (float)time)
            {            
                button.transform.position = Vector3.Lerp(currentPos, area.transform.position, t);
                yield return null;
            }
            reached = true;

            if (button.GetComponent<User>())
            {
                button.GetComponent<User>().addTransition();
            }

            if (Area == "Compras")
                GameObject.Find(Area).GetComponent<AreaManager>().addToList(button);    
            if (areaTag.gameObject.tag == "Queue")
                GameObject.Find(Area).GetComponent<QueueManager>().addToList(button);
            if (areaTag.gameObject.tag == "Salida")
                GameObject.Find(Area).GetComponent<ExitStore>().addToList(button);
            if (areaTag.gameObject.tag == "Llegada_Camiones")
                GameObject.Find(Area).GetComponent<TruckArrivalManager>().addToList(button);
            if (areaTag.gameObject.tag == "Descarga")
                GameObject.Find(Area).GetComponent<DescargaManager>().UnloadTruck(button);
        }
    }

}

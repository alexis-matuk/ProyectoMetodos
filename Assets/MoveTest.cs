using UnityEngine;
using System.Collections;

public class MoveTest : MonoBehaviour {

	// Use this for initialization
    TransitionManager manager;
	void Start () {
        manager = TransitionManager.instance;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {            
            if(gameObject.name=="Button")   
                manager.moveToArea(this.gameObject, "Compras", 1f);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {            
            if(gameObject.name=="Button2")
                manager.moveToArea(this.gameObject, "Compras", 1f);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {            
            if(gameObject.name=="Button3")
                manager.moveToArea(this.gameObject, "Compras", 1f);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {            
            if(gameObject.name=="Button4")
                manager.moveToArea(this.gameObject, "Compras", 1f);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {            
            UnityRandom urand = new UnityRandom();
            Debug.Log(urand.Range(0, 100, UnityRandom.Normalization.STDNORMAL, 1.0f));
        }
	}
}

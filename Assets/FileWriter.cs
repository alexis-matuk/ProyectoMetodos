using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Text;
public class FileWriter : MonoBehaviour {
    public static FileWriter instance;
    string currentText;
	// Use this for initialization
	void Start () {
        currentText = "";
	}

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);       
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void saveFile()
    {
        Directory.CreateDirectory("./Logs");
        string time_now = DateTime.Now.ToString();
        time_now = time_now.Replace("/", "-");
        time_now = time_now.Replace(" ", "_");
        time_now = time_now.Replace(":", "_");
        File.WriteAllText("./Logs/" + time_now + ".txt", currentText);
    }

    public void writeLine(string _text)
    {
        currentText += _text;
    }
        
}

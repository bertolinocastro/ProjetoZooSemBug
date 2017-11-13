using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScript : MonoBehaviour {

    public Text textUi;
    public string txt;
    // Use this for initialization
    void Start () {
        //textUi.enabled = false;
	}
    void Update()
    {
        this.GetComponent<Text>().text = txt;
    }
}

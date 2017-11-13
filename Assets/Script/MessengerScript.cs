using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessengerScript : MonoBehaviour {

	public string messengerTxt = "";
	private Rect messengerRect;
	public GUIStyle messengerStyle;
	private int modo = -1;

	// Use this for initialization
	void Start () {
		//inicializaMessenger ();
		//posicionaMessenger ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnGUI(){
		//if(messengerRect)
		GUI.Label (messengerRect, messengerTxt, messengerStyle);
	}

	void inicializaMessenger(){
		messengerStyle = GUIStyle.none;
		messengerStyle.wordWrap = true;
		messengerStyle.alignment = TextAnchor.UpperCenter;
		messengerStyle.fontSize = (int) (Screen.height*0.08f);
	}

	public void InsereRect(Rect rectangle){
		inicializaMessenger ();
		messengerRect = rectangle;
	}

	public void InsereRectLinhas(float a, float b, float larg, float linhas){
		inicializaMessenger ();
		messengerRect = new Rect(a, b - linhas *messengerStyle.lineHeight, larg, linhas*messengerStyle.lineHeight);
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gizmosScript : MonoBehaviour {

	public Color cor;
	private GameObject presoNaTela;
	public Camera cam;

	// Use this for initialization
	void Awake () {
		cam = Camera.main;
		/*
		cor = Color.black;
		GameObject go = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		go.transform.parent = this.transform;
		go.transform.localPosition = Vector3.zero;
		go.transform.localScale = new Vector3 (0.3f,0.3f,0.3f);*/

		presoNaTela = new GameObject ();

		//presoNaTela = GameObject.CreatePrimitive (PrimitiveType.Plane);
		presoNaTela.transform.Rotate (new Vector3(-90.0f,0,0));
		presoNaTela.transform.localScale = new Vector3 (0.01f,1,0.01f);
		presoNaTela.transform.position = new Vector3 (0,0,-1);
		presoNaTela.transform.parent = this.transform;

	}
	
	// Update is called once per frame
	void Update () {
		float myX = this.transform.position.x;
		float myY = this.transform.position.y;
		float myZ = this.transform.position.z;
		Vector3 vet = cam.WorldToScreenPoint (new Vector3 (myX, myY, myZ));
		vet.z = 10;
		vet = cam.ScreenToWorldPoint (vet);
		presoNaTela.transform.position = vet;
	}



}

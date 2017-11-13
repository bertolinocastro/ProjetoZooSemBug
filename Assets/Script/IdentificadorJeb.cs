using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdentificadorJeb : MonoBehaviour {

	private MessengerScript messenger;

	private Camera cam;
	private GameObject imTarget;
	private Vector3 seD, idD, seE, idE;

	private Vector3 cantoSE, cantoID; 

	private float dimensaoMax;
	private float dimensaoMin;

	private float tempoParaDelay = 0.0f;

	private float precisao = 0.05f;

	private bool checaDobrado = false;
	private bool checaEsticado = false;
	private bool checaDobraEst = false;

	private string t, t1, t2, t3;

	private Vector2 a1, a2, a3, a4;

	// Use this for initialization
	void Start () {
		cantoSE = imTarget.transform.GetChild (0).GetChild(0).position;
		cantoID = imTarget.transform.GetChild (1).GetChild(0).position;

		messenger = gameObject.AddComponent<MessengerScript> ();
		messenger.InsereRect (new Rect(0.0f, Screen.height*0.70f, Screen.width, Screen.height*0.3f));
		DEFINIRPRECISAOCOMSCREEN ();
	}
	
	// Update is called once per frame
	void Update () {
		cantoSE = imTarget.transform.GetChild (0).GetChild(0).position;
		cantoID = imTarget.transform.GetChild (1).GetChild(0).position;
		//DEBUGARTAMANHO ();
	}

	public void InsereImTarget(GameObject im){
		imTarget = im;
	}

	public void InsereCamera(Camera c){
		cam = c;
	}

	public void SetaCantos(Vector3 a, Vector3 b, Vector3 c, Vector3 d){
		seD = setaNoScreenSpace(a);
		idD = setaNoScreenSpace(b);
		seE = setaNoScreenSpace(c);
		idE = setaNoScreenSpace(d);

		SetaDimensaoPJeb (
			Vector3.Distance(seD,idD),
			Vector3.Distance(seE,idE)
		);
	}

	private Vector3 setaNoScreenSpace(Vector3 i){
		float myX = i.x;
		float myY = i.y;
		float myZ = i.z;
		Vector3 vet = cam.WorldToScreenPoint (new Vector3 (myX, myY, myZ));
		vet.z = 10;
		vet = cam.ScreenToWorldPoint (vet);
		return vet;
	}

	public void SetaDimensaoPJeb(float dimMin,float dimMax){
		dimensaoMax = dimMax;
		dimensaoMin = dimMin;
	}

	public bool DobrouBraco(){
		if (checaDobrado)
			return true;

		float dist = Vector3.Distance (cantoSE,cantoID);

		//bool retorno = dist >= dimensaoMin*(1.0f - precisao) && dist < dimensaoMin*(1.0f + precisao);
		bool retorno = estaEntre (dist, dimensaoMin, precisao);// dist >= (dimensaoMin - precisao) && dist < (dimensaoMin + precisao);
		if (retorno) {
			tempoParaDelay = Time.time;
			checaDobrado = true;
		}
	
		return retorno;
	}

	public bool EsticouBraco(){
		if (checaEsticado)
			return true;

		if (tempoParaDelay + 1.5f > Time.time || !checaDobrado)
			return false;

		float dist = Vector3.Distance (cantoSE,cantoID);

		//bool retorno = dist >= dimensaoMax*(1.0f - precisao) && dist < dimensaoMax*(1.0f + precisao);
		bool retorno = estaEntre (dist, dimensaoMax, precisao);//dist >= dimensaoMax - precisao && dist < dimensaoMax + precisao;
		if (retorno) {
			tempoParaDelay = Time.time;
			checaEsticado = true;
		}
		 
		return  retorno;
	}

	public bool DobrouNovamenteBraco(){
		if (checaDobraEst)
			return true;

		if (tempoParaDelay + 1.5f > Time.time || !checaDobrado || !checaEsticado)
			return false;
		
		float dist = Vector3.Distance (cantoSE,cantoID);

		//bool retorno = dist >= dimensaoMin*(1.0f - precisao) && dist < dimensaoMin*(1.0f + precisao);
		bool retorno = estaEntre (dist, dimensaoMin, precisao); //dist >= dimensaoMin - precisao && dist < dimensaoMin + precisao;

		if (retorno) {
			checaDobrado = false;
			checaEsticado = false;
		}

		return retorno;
	}

	private bool estaEntre(float value, float dim, float accuracy){
		return value > dim*(1.0f-accuracy) && value < dim*(1.0f+accuracy);
	}


	private float calculaDistancia(Vector3 a, Vector3 b){
		Vector3 im0 = cam.WorldToScreenPoint (a);
		Vector3 im1 = cam.WorldToScreenPoint (b);
		im0.z = 0; im1.z = 0;
		return (im1 - im0).magnitude;
	}

	public float PorcentagemEsticado(){
		float distIM = Vector3.Distance (cantoSE,cantoID);
		return distIM / dimensaoMax;
	}

	void DEBUGARTAMANHO(){
		float dist = Vector3.Distance (cantoSE,cantoID);

		/*Vector3 i1 = cam.WorldToScreenPoint (a1);
		Vector3 i2 = cam.WorldToScreenPoint (a2);
		float dist1 = Vector3.Distance (i1, i2);*/

		//this.precisao = 15.0f;

		messenger.messengerTxt = "<color=magenta>" +
			dist + " " +
			//dist1 + " " +
			precisao + " \n" +
			(dimensaoMin*(1.0f - precisao)) + " " +
			(dimensaoMin*(1.0f + precisao)) + " \n" +
			(dimensaoMax*(1.0f - precisao)) + " " +
			(dimensaoMax*(1.0f + precisao)) +
			"</color>";
		
		/*
		messenger.messengerTxt = "<color=magenta>" +
			imTarget.transform.GetChild (0).transform.position+ " " +
			imTarget.transform.GetChild (1).transform.position + "\n" +
			a1 + " " +
			a2 + "\n" +
			a3 + " " +
			a4 +
			"</color>";*/
	}

	void DEFINIRPRECISAOCOMSCREEN (){
		/*a1 = cam.ScreenToWorldPoint(new Vector3(0, 0, 1));
		a2 = cam.ScreenToWorldPoint (new Vector3(1, 1, 1));*/
		a1 = new Vector2 (0, 0);
		a2 = new Vector2 (Screen.width, Screen.height);

		print (a1);
		print (a2);
	}

	public void INSERExyzCANTOS(Vector3 seD, Vector3 idD, Vector3 seE, Vector3 idE){
	/*	a1 = seD;
		a2 = idD;
		a3 = seE;
		a4 = idE;*/
	}

}

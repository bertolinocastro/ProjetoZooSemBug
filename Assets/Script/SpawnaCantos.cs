using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnaCantos : MonoBehaviour {

	public GameObject CantoSE;
	public GameObject CantoSD;
	public GameObject CantoIE;
	public GameObject CantoID;

	public Camera cam;

	private Vector3 SupEsq;
	private Vector3 InfDir;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void setaSEeID(Vector3 se, Vector3 id){
		transform.GetChild (0).transform.position = se;
		transform.GetChild (1).transform.position = id;
	}


	public void setaCantosPosicoes(){
		SupEsq = transform.GetChild (0).transform.position;
		InfDir = transform.GetChild (1).transform.position;

		Vector3 screenSE = cam.WorldToScreenPoint (SupEsq);
		Vector3 screenID = cam.WorldToScreenPoint (InfDir);

		// OBS: Isso só funciona se a câmera não estiver rotacionada!
		float width = Mathf.Abs (screenSE.x - screenID.x);
		float height = Mathf.Abs (screenSE.y - screenID.y);
		float zMedio = (screenSE.z + screenID.z) / 2;
		// FIM OBS: Isso só funciona se a câmera não estiver rotacionada!

		setaSEeID (
			cam.ScreenToWorldPoint(new Vector3(Screen.width/2-width/2,Screen.height/2+height/2, zMedio)),
			cam.ScreenToWorldPoint(new Vector3(Screen.width/2+width/2,Screen.height/2-height/2, zMedio))
		);

		SupEsq = transform.GetChild (0).transform.position;
		InfDir = transform.GetChild (1).transform.position;

		CantoSE.transform.position = new Vector3 ( SupEsq.x, SupEsq.y, zMedio );
		CantoSD.transform.position = new Vector3 ( InfDir.x, SupEsq.y, zMedio );
		CantoID.transform.position = new Vector3 ( InfDir.x, InfDir.y, zMedio );
		CantoIE.transform.position = new Vector3 ( SupEsq.x, InfDir.y, zMedio );



	}

	void centralizaPosicoes(){
	}

	public void zeraCantosPosicoes(){
		CantoSE.transform.position = Vector3.zero;
		CantoSD.transform.position = Vector3.zero;
		CantoID.transform.position = Vector3.zero;
		CantoIE.transform.position = Vector3.zero;
	}
}

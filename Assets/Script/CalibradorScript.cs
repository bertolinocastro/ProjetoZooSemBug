using UnityEngine;
using System.Collections;

public class CalibradorScript : MonoBehaviour
{
	public Camera cam;
	public ReadTarget imDetector;

	public GameObject ImageTarget;
	public GameObject CantosDobrado;
	public GameObject CantosEsticado;

	private bool definido1;
	private bool definido2;

	private bool calibrarBtn;
	private bool reiniciarBtn;

	private float temp;
	private float recemCalibrado = 0.0f;

	private MessengerScript messenger;

	private bool ePraSalvar;

	// Textos de output
	const string corTagIni = "<color=white>";
	const string corTagFim = "</color>";
	const string texto0 = "Aponte o dispositivo para o marcador!";
	const string texto1 = "Dobre os braços e toque em calibrar.";
	const string texto2 = "Estique os braços e toque em calibrar.";
	const string texto3 = "Calibrado!";
	const string texto4 = "Aponte para o marcador no. ";
	const string texto5 = "Descalibrado!";
	const string texto6 = "Marcador próximo da calibragem DOBRADA!";
	const string texto7 = "Marcador próximo da calibragem ESTICADA!";
	// Fim Textos

	// Use this for initialization
	void Start ()
	{
		definido1 = false;
		definido2 = false;

		calibrarBtn = false;
		reiniciarBtn = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (reiniciarBtn)
			resetarCalibracao ();

		if (imDetector.isFound) {

			if (!(definido1 && definido2))
				calibraMargens ();
			else {
				if (Time.time < recemCalibrado + 2.0f)
					messenger.messengerTxt = corTagIni+texto3+corTagFim;
				else {
					if (verificaPosicao (CantosDobrado.transform, 20.0f))
						messenger.messengerTxt = corTagIni+texto6+corTagFim;
					else if (verificaPosicao (CantosEsticado.transform, 20.0f))
						messenger.messengerTxt = corTagIni+texto7+corTagFim;
					else
						messenger.messengerTxt = "";
				}
			}
		} else {
			if (!(definido1 && definido2))
				messenger.messengerTxt = corTagIni+texto0+corTagFim;
		}
	}

	public bool EstaCalibrado(){
		return definido1 && definido2;
	}

	void OnGUI(){
		calibrarBtn = GUI.RepeatButton (new Rect (0, 0, 100.0f, 100.0f), "<b>Calibrar</b>");
		reiniciarBtn = GUI.RepeatButton (new Rect (Screen.width - 100.0f, 0, 100.0f, 100.0f), "<b>Reiniciar</b>");
	}

	public void InsereCantos(GameObject CD, GameObject CE, GameObject IM){
		CantosDobrado = CD;
		CantosEsticado = CE;
		ImageTarget = IM;
	}

	public void InsereMessenger(MessengerScript msn){
		messenger = msn;
	}
	public void InsereIMDetector(ReadTarget im){
		imDetector = im;
	}

	public void InsereCam(Camera cm){
		cam = cm;
	}

	void calibraMargens(){
		if (!definido1) {
			messenger.messengerTxt = corTagIni+texto1+corTagFim;

			if (calibrarBtn) {
				CantosDobrado.transform.GetChild (0).transform.position = ImageTarget.transform.GetChild (0).transform.position;
				CantosDobrado.transform.GetChild (1).transform.position = ImageTarget.transform.GetChild (1).transform.position;
				temp = Time.time;

				CantosDobrado.GetComponent<SpawnaCantos> ().setaCantosPosicoes ();

				definido1 = true;
			}

		}

		if (definido1 && !definido2) {
			//print (texto2);
			messenger.messengerTxt = corTagIni+texto2+corTagFim;

			if (calibrarBtn && Time.time > temp + 2.0f) {
				CantosEsticado.GetComponent<SpawnaCantos> ().setaSEeID (
					ImageTarget.transform.GetChild (0).transform.position,
					ImageTarget.transform.GetChild (1).transform.position);
				CantosEsticado.GetComponent<SpawnaCantos> ().setaCantosPosicoes ();

				recemCalibrado = Time.time;
				definido2 = true;
			}
		}


	}

	private bool verificaPosicao(Transform paiT, float distancia){
		Vector3 child0 = cam.WorldToScreenPoint(paiT.GetChild(0).transform.position);
		Vector3 child1 = cam.WorldToScreenPoint(paiT.GetChild(1).transform.position);
		Vector3 im0 = cam.WorldToScreenPoint (ImageTarget.transform.GetChild (0).transform.position);
		Vector3 im1 = cam.WorldToScreenPoint (ImageTarget.transform.GetChild (1).transform.position);

		if (Vector3.Distance (child0, im0) < distancia &&
			Vector3.Distance (child1, im1) < distancia) {
			return true;
		}
		return false;
	}

	void resetarCalibracao(){
		definido1 = definido2 = false;
		CantosDobrado.GetComponent<SpawnaCantos> ().zeraCantosPosicoes ();
		CantosEsticado.GetComponent<SpawnaCantos> ().zeraCantosPosicoes ();
	}
}


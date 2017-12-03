﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GMTutorialScript : MonoBehaviour {

	public RectTransform tutorial;

	private string[] msgInstrutor;
	private List<Vector3> posesInstrutor;
	public TextAsset textoInstrutorAsset;
	public GameObject modalInstrutor;
	public RectTransform imagemInstrutorRect;
	public Image imagemInstrutor;
	public Text textInstrutor;
	private int idTextInstrutor = 0;

	private List<Sprite> instrutorSprites;

	public GameObject skipa;

	public GameObject modalNick;
	public Text nick;
	private SalvaDadosEntreScenes salvador;

	public List<Sprite> imagens;
	public int idImagens = 0;
	private int qtdImagensPAnimacao = 0;
	private float delayPAnimacao = 0.5f;
	private int idAnim = 0;
	private List<int> iniImagensCena;

	public List<string> imagensName;

	public Image background;

	public Text textoFalas;
	public TextAsset textoFalasAsset;

	private const int textLength = 68;
	private const float delayEntreLetras = 0.03f;
	private bool passavel = true;
	private bool pular = false;

	private string[] msg;
	private string[] msgAct;

	private int cenaAct = 0;
	private int subCenaAct = 0;

	private bool reescrevi = false;
	private bool atingiuInput = false;

	private float start;

	// Use this for initialization
	void Start () {
		salvador = gameObject.AddComponent<SalvaDadosEntreScenes> ();
		filenamesInit ();
		backgroudInit ();
		cenasGrupoInit ();
		ativaTodosOsTutoriais ();
		
		instrutorInit ();

		start = Time.time;
	}

	private void instrutorInit(){
		instrutorSprites = new List<Sprite> (Resources.LoadAll<Sprite> ("pointerImages/"));
		imagemInstrutor.sprite = instrutorSprites [0];
		StartCoroutine("animaInstrutor");

		msgInstrutor = textoInstrutorAsset.text.Split(new string[] {"<fala>"}, System.StringSplitOptions.None);
		textInstrutor.text = msgInstrutor [idTextInstrutor];


		Vector3[] u = {
			new Vector3(250.0f, 0.0f, 0.0f),
			new Vector3(-320.0f, -55.0f, 0.0f),
			new Vector3(-300.0f, 125.0f, 0.0f),
		};

		posesInstrutor = new List<Vector3> (u);

		imagemInstrutorRect.localPosition = posesInstrutor [idTextInstrutor++];

	}

	private IEnumerator animaInstrutor(){
		int i = 0;
		imagemInstrutor.gameObject.SetActive (true);
		for (;;) {
			i = i != 0 ? 0 : 1;
			imagemInstrutor.sprite = instrutorSprites [i];
			yield return new WaitForSeconds (delayPAnimacao);
		}
	}

	public void toqueNoInstrutor(){
		if (idTextInstrutor < msgInstrutor.Length) {
			imagemInstrutorRect.localPosition = posesInstrutor [idTextInstrutor];
			textInstrutor.text = msgInstrutor [idTextInstrutor++];
		} else {
			StopCoroutine ("animaInstrutor");
			imagemInstrutorRect.transform.SetParent(tutorial);
			imagemInstrutorRect.transform.SetSiblingIndex (1);
			imagemInstrutorRect.localPosition = posesInstrutor [0];
			imagemInstrutorRect.gameObject.SetActive (false);
			Destroy (modalInstrutor.gameObject);
			mensagensInit ();
		}
	}

	private void cenasGrupoInit (){
		iniImagensCena = new List<int> ();

		char cenaGrupo = imagensName [0] [0];
		iniImagensCena.Add (0);
		for (int i = 1; i < imagensName.Count; ++i) {
			if (cenaGrupo != imagensName [i] [0]) {
				iniImagensCena.Add (i);
				cenaGrupo = imagensName [i] [0];
			}
		}
	}

	private void ativaTodosOsTutoriais(){
		salvador.setaMenuPrincipalTutorial (true);
	}

	private void backgroudInit(){
		// Carregamento das imagens do tutorial a serem usadas como background do Canvas
		imagens = new List<Sprite> (Resources.LoadAll<Sprite> ("imagensTutorial/"));
		proximaCena ();
	}

	private void filenamesInit(){
		int i = 0;
		imagensName = new List<string> ();
		foreach(Object fi in Resources.LoadAll<Object> ("imagensTutorial/")){
			imagensName.Add (fi.name);
		}
		imagensName = imagensName.Distinct ().ToList ();
	}

	private void mensagensInit(){
		msg = textoFalasAsset.text.Split(new string[] {"<cena>"}, System.StringSplitOptions.None);
		msgAct = msg [cenaAct++].Split(new string[] {"<fala>"}, System.StringSplitOptions.None);
		textoFalas.text = msgAct [subCenaAct++];
	}

	// Update is called once per frame
	void Update () {
	}

	public void voltaTexto(){
		
		print ("Entrei nesse if daqui. cenaAct:"+cenaAct+";subcenaAct:"+subCenaAct+
			"idAnim:"+idAnim
		);
		if(atingiuInput){
			return;
		}
		if (!passavel) {
			return;
		}

		/*if (!reescrevi && msgAct [subCenaAct - 1].Length >= textLength) {
			subCenaAct--;
			passaTexto ();
			reescrevi = true;
			return;
		}*/

		if (subCenaAct > 1) {
			subCenaAct -= 2;
			passaTexto ();
			//reescrevi = false;
		} else {
			if (cenaAct > 1) {
				cenaAct -= 2;
				msgAct = msg [cenaAct].Split (new string[] { "<fala>" }, System.StringSplitOptions.None);
				subCenaAct = 0;

				passaTexto ();
				anteriorCena ();
				cenaAct++;
				//reescrevi = false;
			}
		}

	}

	public void passaTexto(){
		print ("Chamara o Passa Texto!");
		if (subCenaAct < msgAct.Length) {
			if (passavel) {
				//pular = false;
				checaInput ();
				checaUsuario ();
				StartCoroutine ("printaLetras");
				//pular = false;
			} else {
				pular = true;
			}
		} else {
			if (cenaAct < msg.Length) {
				subCenaAct = 0;
				msgAct = msg [cenaAct++].Split (new string[] {"<fala>"}, System.StringSplitOptions.None);
				checaInput ();
				checaUsuario ();
				//textoFalas.text = msgAct [subCenaAct++];
				if (passavel) {
					StartCoroutine ("printaLetras");
				}
				if (idImagens < imagens.Count) {
					proximaCena ();
				} else {
					print ("Faltam imagens para as cenas!");
				}
			} else {
				finalizaTutorial ();
				print ("fim do tutorial");
			}
		}
	}

	private IEnumerator printaLetras() {
		passavel = false;
		pular = false;
		//textoFalas.text = "";

		StopCoroutine("animaInstrutor");
		imagemInstrutor.gameObject.SetActive (false);

		string frase = msgAct [subCenaAct];

		int end = 0, i = 0;
		do{
			textoFalas.text = "";
			if (textLength < frase.Length-end) {
				end = frase.LastIndexOf (' ', end+textLength-3);
			}else{
				end = frase.Length;
			}
			for (; i < end; ++i) {
				if(frase[i] == '\n'){
					continue;
				}
				textoFalas.text += frase [i];
				if(!pular)
					yield return new WaitForSeconds (delayEntreLetras);
			}
			pular = false;
			if (end < frase.Length) {
				textoFalas.text += "...";
				StartCoroutine("animaInstrutor");
				while(!pular)
					yield return new WaitForSeconds (delayEntreLetras);
				StopCoroutine("animaInstrutor");
				imagemInstrutor.gameObject.SetActive (false);
				pular = false;
			}
		}while(i < frase.Length);

		StartCoroutine("animaInstrutor");

		subCenaAct++;
		pular = false;
		passavel = true;
		yield break;
	}

	public void skipaTutorial() {
		int ccena, image, novaCat; char categoria; bool achou = false;
		for(ccena = cenaAct; ccena < msg.Length && !(achou=msg[ccena].Contains("<input>")); ++ccena);
		if (!achou)
			return;

		StopCoroutine ("printaLetras");
		passavel = true;
		pular = false;

		cenaAct = ccena;
		idImagens = iniImagensCena[cenaAct];
		subCenaAct = 99;
		desativaSkip ();
		passaTexto ();
		print ("Skipei o Tutorial");
	}

	private void finalizaTutorial(){
		// Carregar scene de início de usabilidade
		SceneManager.LoadSceneAsync("usabilidade");
	}

	private void anteriorCena(){
		idImagens = iniImagensCena [cenaAct];
		proximaCena ();
	}

	private void proximaCena() {
		StopCoroutine ("animaImagem");
		idAnim = idImagens;

		char cenaGrupo = imagensName [idImagens] [0];
        if(cenaGrupo == 'j'){
            delayPAnimacao = 2.0f;
        }else{
            delayPAnimacao = 0.5f;
        }
		qtdImagensPAnimacao = 1;
		for (int i = idAnim + 1; i < imagensName.Count && cenaGrupo == imagensName [i] [0]; ++i) {
			++qtdImagensPAnimacao;
		}
		
		StartCoroutine ("animaImagem");
		
		idImagens+=qtdImagensPAnimacao;
	}

	private IEnumerator animaImagem(){
		for (;;) {
			for (int i = 0; i < qtdImagensPAnimacao; ++i) {
				background.sprite = imagens [idAnim+i];
				yield return new WaitForSeconds (delayPAnimacao);
			}
		}
		yield break;
	}

	public void salvaNickName(){
		if (string.IsNullOrEmpty (nick.text))
			return;
		salvador.salvaNick (nick.text);
		modalNick.SetActive (false);
		passaTexto ();
	}

	private void checaInput(){
		if (msgAct [subCenaAct].Contains ("<input>")) {
			msgAct [subCenaAct] = msgAct [subCenaAct].Replace("<input>", "");
			modalNick.SetActive (true);
			desativaSkip ();
			atingiuInput = true;
		}
	}

	private void checaUsuario(){
		print ("Entrei no checausuario");	
		if (msgAct [subCenaAct].Contains ("<usuario>")) {
			msgAct [subCenaAct] = msgAct [subCenaAct].Replace ("<usuario>",
				salvador.leNick()
			);
		}
	}

	private void desativaSkip(){
		skipa.SetActive(false);
	}
}

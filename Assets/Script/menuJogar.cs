using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menuJogar : MonoBehaviour {

	public RectTransform imagemInstrutorRect;
	public Image imagemInstrutor;
	private List<Sprite> instrutorSprites;
	private float delayPAnimacao = 0.5f;

	private SalvaDadosEntreScenes salvador;
	public Button botaoVoltar;
	public Button botaoJogar;
	public GameObject canvasBtn;

	public GameObject placa;
	public GameObject panda;

	public Text textCanvas;
	public TextAsset falasTutorial;
	private string[] msg;
	private int idFala = 0;

	private bool passavel = true;
	private bool pular = false;

	private int textLength = 54;

	private float delayEntreLetras = 0.03f;

	// Use this for initialization
	void Start () {
		salvador = gameObject.AddComponent<SalvaDadosEntreScenes> ();
		checaTutorial ();
		inicializaDedao ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void inicializaDedao(){
		instrutorSprites = new List<Sprite> (Resources.LoadAll<Sprite> ("pointerImages/"));
		imagemInstrutor.sprite = instrutorSprites [0];
		imagemInstrutorRect.localPosition = new Vector3(-38.0f, 9.0f, 0.0f);
	}

	private IEnumerator animaDedao(){
		int i = 0;
		imagemInstrutorRect.gameObject.SetActive (true);
		for (;;) {
			i = i != 0 ? 0 : 1;
			imagemInstrutor.sprite = instrutorSprites [i];
			yield return new WaitForSeconds (delayPAnimacao);
		}
	}

	private void checaPiscar(){
		if (msg [idFala].Contains ("<piscar>")) {
			msg [idFala] = msg [idFala].Replace ("<piscar>","");
			StartCoroutine ("piscaBotao");
		}
	}

	private void desativaTodosBotoes(){
		botaoVoltar.interactable = false;
		botaoJogar.interactable = false;
	}

	private void checaTutorial(){
		if (salvador.leMenuPrincipalTutorial ()) {
			msg = falasTutorial.text.Split (new string[] { "<fala>" }, System.StringSplitOptions.None);
			desativaTodosBotoes ();
			placa.SetActive (true);
			panda.SetActive (true);
			passaTexto ();
		} else {
			Destroy (canvasBtn.GetComponent<Button>());
		}
	}

	private IEnumerator piscaBotao(){
		bool state = false;
		while (true){
			botaoJogar.interactable = state;
			state = !state;
			yield return new WaitForSeconds(0.5f);
		}
		yield break;
	}

	public void passaTexto(){
		if (idFala < msg.Length) {
			if (passavel) {
				checaPiscar ();
				StartCoroutine ("printaLetras");
				if(idFala >= msg.Length - 1){
					imagemInstrutor.color = Color.black;
					imagemInstrutorRect.localPosition = new Vector3(-252.0f, -59.0f, 0.0f);
				}
			} else {
				pular = true;
			}
		}
	}

	private IEnumerator printaLetras() {
		passavel = false;
		pular = false;
		//textoFalas.text = "";

		StopCoroutine("animaDedao");
		imagemInstrutor.gameObject.SetActive (false);

		string frase = msg [idFala];

		int end = 0, i = 0;
		do{
			textCanvas.text = "";
			if (textLength < frase.Length-end) {
				end = frase.LastIndexOf (' ', end+textLength-3);
			}else{
				end = frase.Length;
			}
			for (; i < end; ++i) {
				if(frase[i] == '\n'){
					continue;
				}
				textCanvas.text += frase [i];
				if(!pular)
					yield return new WaitForSeconds (delayEntreLetras);
			}
			pular = false;
			if (end < frase.Length) {
				textCanvas.text += "...";
				StartCoroutine("animaDedao");
				while(!pular)
					yield return new WaitForSeconds (delayEntreLetras);
				StopCoroutine("animaDedao");
				imagemInstrutor.gameObject.SetActive (false);
				pular = false;
			}
		}while(i < frase.Length);

		StartCoroutine("animaDedao");

		idFala++;
		pular = false;
		passavel = true;
		yield break;
	}

	public void inGame()
	{
		SceneManager.LoadScene("ingame");
	}

	public void Voltar()
	{
		SceneManager.LoadScene("menuInicial");
	}
}

﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MascoteGuiaScript : MonoBehaviour {

	public SalvarDadosScript enviadorDeDados;
	public string url;

	private GameObject mascoteGuiador;
	private List<string> nomesMarcadores;
	private MessengerScript messenger;
	private Text balaoMensagem;
	private Text balaoFimPontuacao;
	private Text balaoFimElogio;

	private GameObject[] estrelasGold;
	private GameObject[] estrelasGray;

	// Mensagens padrão do Mascote
	//const string texto0 = "<color=black>Aponte o dispositivo para o marcador</color> ";
	const string texto0 = "<color=black>Aponte o dispositivo para o (a)</color> ";
	const string texto1 = "<color=magenta>Dobre</color> <color=black>seus braços!</color>";
	const string texto2 = "<color=lime>Estique</color> <color=black>seus braços!</color>";
	const string texto3 = "<color=black>Fase <color=lime>concluída</color>!\nClique em sair.</color>";
	const string texto4a = "<color=black>Você irá para o passo</color> ";
	const string texto4b = " <color=black>de</color> ";
	const string texto7 = "Parabéns!!! Você concluiu a fase!!!";
	const string texto8a = "Tempo total de execução da fase ";
	const string texto8b = "\nAproveitamento de ";
	const string texto9 = "Infelizmente você ultrapassou o tempo limite :-(\nTente novamente!";
	// Fim mensagens padrão do Mascote

	// Use this for initialization
	void Start () {
		messenger = gameObject.AddComponent<MessengerScript> ();
		messenger.InsereRect (new Rect(0, Screen.height/2.0f, Screen.width, Screen.height/2.0f));
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void InsereMascote(GameObject mascoteGuiador){
		this.mascoteGuiador = mascoteGuiador;
	}

	public void InsereBalaoTexto(Text balao){
		balaoMensagem = balao;
	}
	public void InsereBalaoFim(Text pontuacao, Text elogio){
		balaoFimPontuacao = pontuacao;
		balaoFimElogio = elogio;
	}
	public void InsereEstrelas(GameObject[] gold, GameObject[] gray){
		estrelasGold = gold;
		estrelasGray = gray;
	}

	public void InsereNomesMarcadores(List<string> lista){
		nomesMarcadores = lista;
	}

	public void Ativador(bool status){
		gameObject.SetActive (status);
	}

	public void ApontarMarcador(int index){
		if(index < nomesMarcadores.Count)
			balaoMensagem.text = texto0 + "<color=red>"+nomesMarcadores[index]+"</color>";
	}

	public void DobrarBracos(){
		balaoMensagem.text = texto1;
	}

	public void EsticarBracos(){
		balaoMensagem.text = texto2;
	}

	public void FinalizaPassos(){
		balaoMensagem.text = texto7;
	}

	public void InsereEnviador(SalvarDadosScript s){
		enviadorDeDados = s;
	}

	public void FinalizarFase(float tempoInicio, float tempoMax){

		float tempoTotal = Mathf.Abs(Time.time - tempoInicio);
		TimeSpan time = TimeSpan.FromSeconds (tempoTotal);
		string answer = string.Format("{0:D2}h:{1:D2}m:{2:D2}s", 
			time.Hours, 
			time.Minutes, 
			time.Seconds);

		float porcentagem = ((tempoMax - tempoTotal) / tempoMax * 100);
		if (porcentagem < 33.3f) {
			balaoFimElogio.text = texto9;
			balaoFimPontuacao.text = answer + texto8b + "33%!";
		} else {
			balaoFimElogio.text = texto7;
			balaoFimPontuacao.text = answer + texto8b + porcentagem.ToString ("0.00") + "%!";
		}
		int div = (int) Mathf.Ceil (porcentagem / (100.0f / 3.0f));

		int i = 0;
		for (i = 0; i < 3; i++) {
			if (i <= div) {
				estrelasGold [i].SetActive (true);
				estrelasGray [i].SetActive (false);
			} else {
				estrelasGold [i].SetActive (false);
				estrelasGray [i].SetActive (true);
			}
		}

		SalvaStatus(porcentagem, tempoTotal);

		//balaoMensagem.text = texto3;
	}

	public void SalvaStatus(float pontuacao, float tempoTot){

		//enviadorDeDados.url = urlN + "/Class/Action/UsuarioAC.php?req=1";
		enviadorDeDados.url = url + "/Class/Action/UsuarioAC.php?req=1";

		enviadorDeDados.url = "http://cb2f5694.ngrok.io/MyProject/VilaAnimal/Class/Action/UsuarioAC.php?req=1";
		print ("Enviando dados ao server: " + enviadorDeDados.url);

		if (string.IsNullOrEmpty (url)) {
			print ("Não existe servidor para envio dos dados!");
			return;
		}

		DateTime agora = DateTime.Now;
		string data = agora.ToString ("dd/MM/yy");
		print ("Data de captação dos dados "+data);

		DadosSalvar dados = new DadosSalvar();
		//System.Random random = new System.Random();
		dados.nickname = PlayerPrefs.GetString("nickName");
		dados.pontuacao.Add(pontuacao);
		dados.tempo.Add(tempoTot);
		dados.data.Add(data);

		enviadorDeDados.GerarJson(dados);

	}

	public void AvisaEstagio(int act, int max){
		balaoMensagem.text = texto4a + "<color=magenta>" + act + "</color>" + texto4b + "<color=magenta>" + max + "</color>";
	}
}

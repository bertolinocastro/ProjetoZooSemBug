﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class configuracao : MonoBehaviour {
	private SalvaDadosEntreScenes salvador;
	public Text serv;
	public GameObject alterarnickGO;

	public Text nicknovo;

	// Use this for initialization
	void Start () {
		salvador = gameObject.AddComponent<SalvaDadosEntreScenes> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AtivarTutorial(){
		salvador.setaMenuPrincipalTutorial (true);
		salvador.tutorialJaVisto (false);
		SceneManager.LoadSceneAsync ("start");
	}

	public void Voltar()
	{
		SceneManager.LoadSceneAsync("menuInicial");
	}

	public void salvaServidor(){
		string result = serv.text;

		if (string.IsNullOrEmpty (result))
			return;
		
		if ( result[result.Length - 1] == '/')
			result = result.Remove (serv.text.Length - 1);
		salvador.setaServidor (result);
		print ("Servidor alterado com sucesso!\n" + result);
	}

	public void clicaAlterarNome(){
		alterarnickGO.SetActive (true);	
	}

	public void terminaAlterarNome(){
		salvador.nickName (nicknovo.text);
		print ("Novo nick salvo!\n" + nicknovo.text);

	}
	public void fechaAlterarNome(){
		alterarnickGO.SetActive (false);
	}

	public void apagarTudo(){
		salvador.limpaTudo ();
		SceneManager.LoadScene ("start");
	}
}

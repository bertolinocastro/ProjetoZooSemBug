using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ranking : MonoBehaviour {
    public GameObject painelNet;
    public GameObject urlD;
	// Use this for initialization
	void Start () {
        painelNet.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Voltar()
	{
		SceneManager.LoadSceneAsync("menuInicial");
	}

    public void Net()
    {
        painelNet.SetActive(true);
    }
    public void MandarUrl()
    {
		InputField input = urlD.GetComponent<InputField> ();
		if(!string.IsNullOrEmpty(input.text))
        {
			string urlN = input.text;

            GetComponent<SalvarDadosScript>().url = urlN + "/Class/Action/UsuarioAC.php?req=1";

            DadosSalvar dados = new DadosSalvar();
            System.Random random = new System.Random();
            dados.nickname = PlayerPrefs.GetString("nickName");
            dados.pontuacao.Add(random.Next(1, 10));
            dados.pontuacao.Add(random.Next(1, 10));
            dados.tempo.Add(random.Next(1, 60));
            dados.tempo.Add(random.Next(1, 60));
            dados.data.Add("10/12/2017");
            dados.data.Add("11/12/2017");
            GetComponent<SalvarDadosScript>().GerarJson(dados);
            painelNet.SetActive(false);
        }
        
    }
}

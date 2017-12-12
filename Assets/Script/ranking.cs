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
        if(urlD.GetComponent<InputField>().text != "" || urlD.GetComponent<InputField>().text != null)
        {
            //string urlN = urlD.GetComponent<InputField>().text;

            //GetComponent<SalvarDadosScript>().url = urlN + "/Class/Action/UsuarioAC.php?req=1";
           
            //GetComponent<SalvarDadosScript>().GerarJson(dados);
            //painelNet.SetActive(false);
        }
        
    }
}

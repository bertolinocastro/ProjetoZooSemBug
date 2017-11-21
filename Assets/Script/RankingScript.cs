using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class RankingScript : MonoBehaviour {

    public GameObject gameManager;
    private List<DadosRelatorio> pontFake = new List<DadosRelatorio>();
    public GameObject objeto;
    public Text nick;
    public Text pont;

    // Use this for initialization
    void Start () {
        //gameManager.GetComponent<SalvarDadosScript>();
        printaTela();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void gerarGhost()
    {
        float auxPont = 0,auxP = 0, auxN = 0;
        string[] nomes = { "Maicon","Sterphane","João","Gabriel","Eulina" ,
            "Joana","Angelica","Lucas","Marcos","Matheus","Stuart","José",
            "Thaila","Caique","Itamar","Nayara","Maiane","Sara","Suzie","Omar"};

        Debug.Log("Entoru");
        gameManager.GetComponent<SalvarDadosScript>().CarregaDados();
        
        DadosSalvar dadosG = gameManager.GetComponent<SalvarDadosScript>().getDadosCarregados();
        int count = dadosG.pontuacao.Count;
        System.Random random = new System.Random();

        //Pega o ponto do paciente e adiciona a uma variavel
        for (int i = 0; i < count; i++)
        {
            auxPont += (float)dadosG.pontuacao[i];
        }

        DadosRelatorio dr = new DadosRelatorio();
        dr.setNickName(dadosG.nickname);
        dr.setPontuacao(auxPont);
        dr.setTempo(60);
        pontFake.Add(dr);
        
        for (int y = 0; y < 3; y++)
        {
            int pontGerado = random.Next(1, 10);
            int tempoGerado = random.Next(10, 60);
            int nomesG = random.Next(0, 19);
            //Positivo
            auxP = auxPont + pontGerado;
            DadosRelatorio drP = new DadosRelatorio();
            drP.setNickName(nomes[nomesG]);
            drP.setPontuacao(auxP);
            drP.setTempo(tempoGerado);
            pontFake.Add(drP);
            //Negativo

            if (auxPont > 0 && auxPont > pontGerado)
            {
                nomesG = random.Next(0, 19);
                DadosRelatorio drN = new DadosRelatorio();
                auxN = auxPont - pontGerado;
                drN.setNickName(nomes[nomesG]);
                drN.setPontuacao(auxN);
                drN.setTempo(tempoGerado % 2 == 0 ? tempoGerado * 1.2f : tempoGerado * 1.3f);
                pontFake.Add(drN);
            }
            auxPont = auxP;
        }
        

    }

    public void OrdenaLista()
    {
        gerarGhost();
        if(pontFake.Count > 1)
        {
            pontFake.Sort(delegate (DadosRelatorio x, DadosRelatorio y)
            {
                return y.getPontuacao().CompareTo(x.getPontuacao());
            });
        }        
    }

    public void printaTela()
    {
        OrdenaLista();
        int cont = pontFake.Count;

        for (int i = 0; i < cont; i++)
        {
            Debug.Log("Nick "+ pontFake[i].getNickName()+" Pontuação "+ pontFake[i].getPontuacao());
            nick.text += pontFake[i].getNickName()+"\n";
            pont.text += pontFake[i].getPontuacao() + "\n";
        }
        
    }

}

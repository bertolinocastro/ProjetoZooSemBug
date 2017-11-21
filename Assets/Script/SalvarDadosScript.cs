using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[Serializable]
public class DadosSalvar
{

    public string nickname;
    public ArrayList pontuacao = new ArrayList();
    public ArrayList tempo = new ArrayList();
    public ArrayList data = new ArrayList();
}

public class SalvarDadosScript : MonoBehaviour {

    public static SalvarDadosScript salvar;

    private DadosSalvar dadosCarregados;
    


    public Text teste;
    
    private string filePath, temp;


	// Use this for initialization
	void Awake () {
		if(salvar == null)
        {
            salvar = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        CriarPath("info");
        temp = Application.persistentDataPath + Path.DirectorySeparatorChar + "temp.dat";
    }

    // Use this for initialization
    void Start()
    {
        //dadosCarregados = new List<DadosSalvar>();
        //CarregaDados();
    }

    public void CriarPath(string nome)
    {
        filePath = Application.persistentDataPath + Path.DirectorySeparatorChar + nome+".dat";
    }

	public void SalvarDados(DadosRelatorio dados)
    {
        if (File.Exists(filePath))
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream fileadd = new FileStream(filePath, FileMode.Open);
            FileStream arqTemp = new FileStream(temp, FileMode.Create);

            DadosSalvar dsAtual = (DadosSalvar)bf.Deserialize(fileadd);

            

            /*
            DadosSalvar dnovos = new DadosSalvar();
            
            dnovos.nickname = dados.getNickName();

            dnovos.data.Add(ds.data);
            dnovos.data.Add(dados.getData());

            dnovos.pontuacao.Add(ds.pontuacao);
            dnovos.pontuacao.Add(dados.getPontuacao());

            dnovos.tempo.Add(ds.tempo);
            dnovos.tempo.Add(dados.getTempo());
            */

            //DadosSalvar dsAtual = new DadosSalvar();
            
            dsAtual.nickname = dados.getNickName();
            dsAtual.data.Add(dados.getData());
            dsAtual.pontuacao.Add(dados.getPontuacao());
            dsAtual.tempo.Add(dados.getTempo());
            

            bf.Serialize(arqTemp, dsAtual);
            fileadd.Close();
            arqTemp.Close();
            if (File.Exists(temp))
            {
                Debug.Log("Temp existe vai deletar o arquvio e criar outro");
                File.Delete(filePath);
                File.Move(temp, filePath);
            }
            
            Debug.Log("Funcionou a Atualização");
            
        }
        else
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = new FileStream(filePath,FileMode.Create);
            DadosSalvar ds = new DadosSalvar();
            ds.nickname = dados.getNickName();
            ds.pontuacao.Add(dados.getPontuacao());
            ds.tempo.Add(dados.getTempo());
            ds.data.Add(dados.getData());


            bf.Serialize(file, ds);

            file.Close();
            Debug.Log("Funcionou");
        }
        
    }

    public void CarregaDados()
    {
        //teste.text = "";
        if (File.Exists(filePath))
        {
            dadosCarregados = new DadosSalvar();
            Debug.Log("Entrou no if");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(filePath, FileMode.Open);

            DadosSalvar ds =  (DadosSalvar) bf.Deserialize(file);
            file.Close();

            /*
            Debug.Log("Data "+ds.data.Count+"pontuação "+ ds.pontuacao[0]);
            
            for(int i = 0;i < ds.data.Count; i++)
            {
                teste.text += "Nome:" + ds.nickname + " Pontuação:" + ds.pontuacao[i] + " Tempo:" + ds.tempo[i]+"\n";
            }
            */
            
            dadosCarregados =ds ;
        }        
    }

    public void GerarDados()
    {
        System.Random random = new System.Random();
        DadosRelatorio ds = new DadosRelatorio();

        ds.setNickName("teste");
        int pont = random.Next(1, 999);
        int temp = random.Next(1, 60);
        ds.setPontuacao((pont));
        ds.setTempo(temp);
        ds.setData("12/11/2017");
        SalvarDados(ds);
        Debug.Log("Gerou");
    }
    
    public DadosSalvar getDadosCarregados()
    {
        return dadosCarregados;
    }
}

﻿using System.Collections;
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

    


    public Text teste;
    
    private string filePath;
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

        filePath = Application.persistentDataPath + Path.DirectorySeparatorChar + "info.dat";
        
	}
	
	public void SalvarDados(DadosRelatorio dados)
    {
        if (File.Exists(filePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fileadd = new FileStream(filePath, FileMode.Append);

            //DadosSalvar dsAtual = (DadosSalvar)bf.Deserialize(fileadd);

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
            DadosSalvar dsAtual = new DadosSalvar();
            
            dsAtual.nickname = dados.getNickName();
            dsAtual.data.Add(dados.getData());
            dsAtual.pontuacao.Add(dados.getPontuacao());
            dsAtual.tempo.Add(dados.getTempo());
            
            bf.Serialize(fileadd, dsAtual);
            
            fileadd.Close();
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
        if (File.Exists(filePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(filePath, FileMode.Open);

            DadosSalvar ds =  (DadosSalvar) bf.Deserialize(file);
            file.Close();
            Debug.Log(ds.data.Count);
            for(int i = 0;i < ds.data.Count; i++)
            {
                teste.text = "Nome:" + ds.nickname + "\nPontuação:" + ds.pontuacao[i] + "\nTempo:" + ds.tempo[i];
            }
            
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
    void Update()
    {
        CarregaDados();
    }
}

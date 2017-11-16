using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DadosRelatorio : MonoBehaviour {

    private string nickname;
    private float pontuacao;
    private float tempo;
    private string data;

    public void setNickName(string nn)
    {
        this.nickname = nn;
    }
    public string getNickName()
    {
        return this.nickname;
    }

    public void setPontuacao(float p)
    {
        this.pontuacao = p;
    }
    public float getPontuacao()
    {
        return this.pontuacao;
    }

    public void setTempo(float t)
    {
        this.tempo = t;
    }
    public float getTempo()
    {
        return this.tempo;
    }

    public void setData(string d)
    {
        this.data = d;
    }
    public string getData()
    {
        return this.data;
    }
}

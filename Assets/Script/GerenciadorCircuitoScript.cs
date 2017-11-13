using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GerenciadorCircuitoScript : MonoBehaviour
{

	private List<int> circuito;
	private int passoAtual;
	private int passoMaximo;

	// Use this for initialization
	void Start ()
	{
		passoAtual = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	public void InsereCircuito(List<int> lista){
		circuito = lista;
		print ("Debugando lista: ");
		foreach (int i in lista) {
			print (i);
		}
		passoMaximo = circuito.Count;
	}

	public bool TemProximo(){
		return (passoAtual < passoMaximo);
	}

	public int AvancarPasso(){
		passoAtual++;
		return TemProximo () ? circuito[passoAtual] : -1;
	}

	public int PassoAtual(){
		return passoAtual;
	}

	public int MarcadorAtual(){
		return circuito [passoAtual];
	}

	public int PassoMaximo(){
		return passoMaximo;
	}

	public void ReiniciarFase(){
		passoAtual = 0;
	}
}


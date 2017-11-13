using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListaImTargetsScript : MonoBehaviour {

	private int lastActive = 0;
	public List<Transform> lista;

	void Start(){
		
	}

	public void Inicializar(){
		//lista = Listar ();
		foreach(Transform go in lista){
			go.gameObject.SetActive (false);
		}
		ativaTarget (0); // Ativando o primeiro marcador do circuito
	}

	public List<Transform> Listar(){
		/*List<Transform> lista = new List<Transform>();

		int max = transform.childCount;
		for (int id = 0; id < max; ++id) {
			lista.Add (transform.GetChild(id).transform);
		}*/
		return lista;
	}

	public Transform Get(int index){
		if(index < lista.Count)
			return lista [index];
		return null;
	}

	public void AtivaTarget(int index){
		if (index == lastActive)
			return;

		if(index < lista.Count)
			ativaTarget (index);
	}

	private void ativaTarget(int index){
		lista[lastActive].gameObject.SetActive(false);
		lista[index].gameObject.SetActive(true);
		lastActive = index;
	}

	public ReadTarget LerReadTarget(int index){
		print ("Lendo Image Target no.: "+index);
		if (index < lista.Count)
			return Get (index).gameObject.GetComponent<ReadTarget> ();
		return null;
	}

	public bool ChecaSeExisteOCircuito(List<int> circuito){
		foreach(int i in circuito){
			if (i < 0 || i >= lista.Count)
				return false;
		}
		return true;
	}
}

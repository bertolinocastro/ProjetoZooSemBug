using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GMStartScript : MonoBehaviour {
	private SalvaDadosEntreScenes salvador;

	private float start;

	// Use this for initialization
	void Start () {
		salvador = gameObject.AddComponent<SalvaDadosEntreScenes> ();
		start = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time >= start + 1.0f) {
			if (salvador.tutorialJaVisto ())
				SceneManager.LoadSceneAsync ("menuInicial");
			else
				SceneManager.LoadSceneAsync ("tutorial");
		}
	}
}

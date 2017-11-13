using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class ReadTarget : MonoBehaviour, ITrackableEventHandler
{

    private TrackableBehaviour mTrackableBehaviour;

    //public GameObject contador;
    public GameObject target;
    //public GameObject tela;
    //public Text uiText;
    bool teste = false;

	public bool isFound;

	void Start()
    {
		isFound = false;
       //acont = gameObject.GetComponent<Contador>();

        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }

    public void OnTrackableStateChanged(
              TrackableBehaviour.Status previousStatus,
              TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
			newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            OnTrackingFound();
        }
        else
        {
            OnTrackingLost();
        }
    }
    private void OnTrackingFound()
    {
		isFound = true;

        //contador.GetComponent<MeshRenderer>().enabled = true;
        //uiText.GetComponent<TextScript>().textUi.enabled = true;
        //tela.GetComponent<SpriteRenderer>().enabled = true;
        


    }
    private void OnTrackingLost()
    {
		isFound = false;

        //tela.GetComponent<SpriteRenderer>().enabled = false;
        //contador.GetComponent<MeshRenderer>().enabled = false;
		//uiText.GetComponent<TextScript>().textUi.enabled = false;
        //contador.GetComponent<Contador>().cond = false;
        //contador.GetComponent<Contador>().cont = 3.0f;
    }

    void Update()
    {/*
        teste = target.GetComponent<ImageTargetScale>().dis;
        if (teste)
        {
            //contador.GetComponent<Contador>().cond = true;
            print("Variavel ta " + teste);
        }else
        {
            //contador.GetComponent<Contador>().cond = false;
        }*/
    }
}

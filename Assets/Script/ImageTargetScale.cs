using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageTargetScale : MonoBehaviour {
    public Transform topL;
    public Transform topR;
    public Transform botL;
    public Transform botR;

    //public Transform fake;

    public Transform borda;
    //public Transform target;

    //float x = Screen.width;
    //float y = Screen.height;

    public Transform BtopL;
    public Transform BtopR;
    public Transform BbotL;
    public Transform BbotR;

    public TextScript t;

    public bool dis;

	public float closer;

    // Use this for initialization
    void Start () {
       //print("Largura="+x+"Altura="+y);
        dis = false;
        //fake.transform.position = new Vector3(borda.position.x, target.position.y, target.position.z);
        //borda.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));

    }


    /*
    public bool distancia(Transform borda,Transform target)
    {
        float dist = Vector3.Distance(borda.position, target.position);
        print(dist);
        if(dist < 10.0f)
        {
            return  dis = true;
        }else
        {
            return dis = false;
        }
    }
    */
    // Update is called once per frame
    void Update()
    {
        
    }

}

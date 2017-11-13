using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KittenScript : MonoBehaviour {

	public string actAnim;

	// Use this for initialization
	void Start () {
		actAnim = "Idle";
	}
	
	// Update is called once per frame
	void Update () {
		if (this.GetComponent<Animation> ().IsPlaying (actAnim))
			return;
		
		if (! this.GetComponent<Animation> ().IsPlaying (actAnim) && !this.GetComponent<Animation> ().IsPlaying ("Idle")) {
			setaAnimation ("Idle");
		}
			
	}

	public void setaAnimation(string anim){
		actAnim = anim;
		this.GetComponent<Animation> ().Play (anim);
	}
}

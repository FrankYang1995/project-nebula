﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour {

	private GameObject player1;
	private GameObject player2;

	private GameObject relic;

	// Use this for initialization
	void Start () {
		player1 = GameObject.Find ("Player 1");
		player2 = GameObject.Find ("Player 2");
		relic = GameObject.Find("Relic");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void  OnTriggerExit2D(Collider2D other){
		if (other.name == "Player 1" && player1.GetComponent<PlayerController> ().GetIsHolding()) {
			relic.GetComponent<Grab> ().endTriggerColour ();
		} else if (other.name == "Player 2" && player2.GetComponent<PlayerController> ().GetIsHolding()){
			print ("hitbyp2");
			relic.GetComponent<Grab> ().endTriggerColour ();
		} 

	}
}

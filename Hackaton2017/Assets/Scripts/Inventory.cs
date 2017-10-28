using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
	private PickableItem item;

	// Use this for initialization
	void Start () {
		this.item = null;
	}
	
	// Update is called once per frame
	void Update () {
	}

	bool GrabItem(PickableItem item) {
		if (!this.item)
			return false;
		this.item = item;
		return true;
	}

	PickableItem Release() {
		var item = this.item;
		this.item = null;
		return item;
	}
}

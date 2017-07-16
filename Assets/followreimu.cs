using UnityEngine;
using System.Collections;

public class followreimu : MonoBehaviour {

	private GameObject reimu;

	private Vector3 pos;

	// Use this for initialization
	void Start () {
	
		reimu = GameObject.Find("Person");
	}
	
	// Update is called once per frame
	void LateUpdate () {
		pos = reimu.transform.position;
		pos.z = -10;
		transform.position = pos;
	}
}

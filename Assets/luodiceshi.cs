using UnityEngine;
using System.Collections;

public class luodiceshi : MonoBehaviour {

	private GameObject reimu;
	// Use this for initialization
	void Start () {
	
		reimu = transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D( Collider2D col ) 
	{
		Debug.Log(col.gameObject.name);
		print("luodi");
		if(reimu.GetComponent<control>().jump != 0)reimu.GetComponent<control>().jump = 0;
	}
}

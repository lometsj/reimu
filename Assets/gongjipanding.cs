using UnityEngine;
using System.Collections;

public class gongjipanding : MonoBehaviour {

	public GameObject reimu;

	// Use this for initialization
	void Start () {
	
		reimu = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {

	}
	void OnTriggerEnter2D( Collider2D col ) 
	{
		if(col.gameObject.tag == "enermy"  && reimu.GetComponent<PAinimation>().Attacking() == true)
		{
			Destroy(col.gameObject);
		}
	}
}

using UnityEngine;
using System.Collections;

public class beijipanding : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D( Collider2D col ) 
	{
		if(col.gameObject.tag == "zidan" )
		{
			//在这里操作扣血
			Destroy(col.gameObject);
		}
	}
}

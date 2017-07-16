using UnityEngine;
using System.Collections;

public class enermy2 : MonoBehaviour {

	public int state;

	public GameObject zidan;

	public float ttime;

	public float attackdis;

	private int STAND = 1;
	private int ATTACK = 2;
	private GameObject reimu;
	private Vector3 a;
	private Vector3 b;
	private Vector3 c;
	private float ltime;

	void getdierction()
	{
		a = reimu.transform.position;
		b = transform.position;
		c = a - b;
	}

	float distance()
	{
		float d = transform.position.x - reimu.transform.position.x;
		if (d <= 0)
			d = -d;
		
		return d;
	}

	// Use this for initialization
	void Start () {
		attackdis = 4.0f;
		state = STAND;
		reimu = GameObject.FindGameObjectWithTag ("Player");
		ttime = 2.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (distance () <= attackdis)
			state = ATTACK;
		if (distance () > attackdis)
			state = STAND;

		if(state == ATTACK)
		{
			getdierction();
			if (Time.time > ltime)
			{
				getdierction();
				ltime = Time.time + ttime;
			   	GameObject zzidan = GameObject.Instantiate(zidan,transform.position,transform.rotation) as GameObject;
				zzidan.GetComponent<Rigidbody2D>().AddForce(c*10);
			}
		}
	}
	
}

using UnityEngine;
using System.Collections;

public class enermy1 : MonoBehaviour {

	public float see;

	public int state;

	public float speed;

	public float ttime;

	public int direction;

	public float chasedis;

	public float attackdis;

	private int WALK = 1;
	private int CHASE = 2;
	private int ATTACK = 3;
	private int LEFT = -1;
	private int RIGHT = 1;
	private float ltime = 0.0f;
	private GameObject reimu;

	// Use this for initialization
	void Start () {
		state = WALK;
		direction = RIGHT;
		speed = 0.03f;
		ttime = 3.0f;
		chasedis = 2.0f;
		attackdis = 0.05f;
		reimu = GameObject.FindGameObjectWithTag ("Player");
	}

	float distance()
	{
		float a = transform.position.x - reimu.transform.position.x;
		if (a <= 0)
			a = -a;

		return a;
	}

	// Update is called once per frame
	void Update () {
		print ("?");
		see = distance ();
		if (state == WALK) {
			print("walk");
			if (direction == LEFT) {
				transform.Translate (0 - speed, 0f, 0f);
			} else if (direction == RIGHT) {
				transform.Translate (speed, 0f, 0f);
			}
			if (Time.time > ltime) {
				direction = - direction;
				ltime = Time.time + ttime;
			}
			if (distance() <= chasedis) {
				state = CHASE;
			}
		} else if (state == CHASE) {
			//speed *= 2;
			if(transform.position.x - reimu.transform.position.x >= 0)
			{
				transform.Translate(-speed,0f,0f);
			}
			if(transform.position.x - reimu.transform.position.x <= 0)
			{
				transform.Translate(speed,0f,0f);
			}
			if(distance() <= attackdis)
			{
				state = ATTACK;
			}

		}else if(state == ATTACK){
			if(distance() >= attackdis)
			{
				state = WALK;
			}
		}
	}
}

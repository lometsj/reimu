using UnityEngine;
using System.Collections;

public class Creature : MonoBehaviour {

	public int Health, Power;
	public int MaxHealth, MaxPower;
	public int NowCardID, LeftI, RightI = 2, NowPos = 0;
	public int[] CardHave;

	// Use this for initialization
	void Start () {
		Health = 100;
		MaxHealth = 100;
	}
	
	// Update is called once per frame
	void Update () {
		if (Health > MaxHealth)
			Health = MaxHealth;
		if (Input.GetKeyDown (KeyCode.Tab)) {
			if (NowPos + 1 > RightI){
				NowPos = LeftI;
				NowCardID = CardHave[NowPos];
			}else{
				NowPos += 1;
				NowCardID = CardHave[NowPos];
			}
		}
	}
}

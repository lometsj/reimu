using UnityEngine;
using System.Collections;

public class SystemHP : MonoBehaviour {

	public GameObject Reimu;
	public Texture2D HPBg, HPFor;
	public int NowHP, MaxHP;

	public Vector2 offset = new Vector2(13,15);

	// Use this for initialization
	void Start () {
		Reimu = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		NowHP = Reimu.GetComponent<Creature> ().Health;
		MaxHP = Reimu.GetComponent<Creature> ().MaxHealth;
	}

	void OnGUI(){
		//NowHP = GetComponent<Creature> ().Health;
		//MaxHP = GetComponent<Creature> ().MaxHealth;

		if (Event.current.type != EventType.Repaint) {
			return;
		}
		Rect HPBgRect = new Rect (0, 0, 200, 40);

		GUI.DrawTexture (HPBgRect, HPBg);
		float width = 200f * ((float)NowHP / (float)MaxHP);

		if (width < 1) return;

		Rect HPForRect = new Rect (offset.x, offset.y, width, 40);
		GUI.DrawTexture (HPForRect, HPFor);
	}
}

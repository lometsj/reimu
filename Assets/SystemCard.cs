using UnityEngine;
using System.Collections;

public class SystemCard : MonoBehaviour {

	public GameObject Reimu;
	
	public Texture2D[] CardSave;
	public int id, index;

	// Use this for initialization
	void Start () {
		Reimu = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		id = Reimu.GetComponent<Creature> ().NowCardID;
	}

	void OnGUI(){
		if (Event.current.type != EventType.Repaint) {
			return;
		}
		index = (int)((Time.timeSinceLevelLoad * 8) % CardSave.Length);
		Rect CardSaveRect = new Rect (400, 0, 64, 96);

		//GUI.DrawTexture (CardSaveRect, GetComponent<DataCard>().Cards[id]);
		GUI.DrawTexture (CardSaveRect, CardSave [index]);
	}
}

using UnityEngine;
using System.Collections;

public class PAinimation : MonoBehaviour {
	
	public int state, direction;
	private SpriteRenderer PersonRenderer;

	//Define some values
	public int framesPersecond, JumpFrames;
	public Sprite[] Stand, Jump, JumpToward, Dash, DashAirF, DashAirB, Walk;
	public Sprite[] Attack0, Attack1, Attack2, Attack3, Attack4, DashAttack;

	//Define state value
	private int STAND_R = 10, STAND_L = 11, WALK_R = 20, WALK_L = 21, JUMP_R = 30, JUMP_L = 31, JUMP_TOWARD_R = 32, JUMP_TOWARD_L = 33,
				DASH_R = 40, DASH_L = 41, DASH_AIR_R_FRONT = 42, DASH_AIR_L_FRONT = 43, DASH_STOP_R = 44, DASH_STOP_L = 45, DASH_AIR_R_BACK = 46, DASH_AIR_L_BACK = 47;
	private int ATTACK0_R = 50, ATTACK0_L = 51, ATTACK1_R = 52, ATTACK1_L = 53, ATTACK2_R = 54, ATTACK2_L = 55, ATTACK3_R = 56, ATTACK3_L = 57, ATTACK4_R = 58, ATTACK4_L = 59;
	private int RIGHT = 0, LEFT = 1;

	//Define control value
	private bool DashJump = false, isJump = false, isAttack0 = false, isAttack1 = false, isAttack2 = false, isAttack3 = false, isAttack4 = false, isDash = false, isStop = false,
				isAirJump = false;
	public float IndepenIndex = 0;
	public float LastAttack = 0, LastGo = -5, TempGo = -5; //time
	private int LastDirection = 0;
	private int LastState; // last unfree state
	private int DASH_BEGIN = 1, DASH_END = 4;

	// Use this for initialization
	void Start () {
		PersonRenderer = GetComponent<Renderer>() as SpriteRenderer;
		framesPersecond = 13;
		JumpFrames = 9;
		state = STAND_R;
		LastState = STAND_R;
		direction = RIGHT;
	}

	//Stop action
	void StopState(){
		if (direction == RIGHT){
			state = STAND_R;
		}else {
			state = STAND_L;
		}
		JumpFrames = 9;
	}

	//isFree?
	bool isFree(){
		if (state == STAND_R || state == STAND_L)
			return true;

		return false;
	}

	//isContinue
	bool isContinue0(){
		if (Time.timeSinceLevelLoad - LastAttack < 0.44f && Time.timeSinceLevelLoad - LastAttack > 0.23f)
			return true;

		return false;
	}

	bool isContinue1(){
		if (Time.timeSinceLevelLoad - LastAttack < 0.65f && Time.timeSinceLevelLoad - LastAttack > 0.5f)
			return true;

		return false;
	}

	//isDash?
	bool Dashing(){
		if (Time.timeSinceLevelLoad - LastGo < 0.2f && Time.timeSinceLevelLoad - LastGo > 0.1f)
			return true;

		return false;
	}

	//Attack?
	public bool Attacking(){
		if (isAttack0 || isAttack1 || isAttack2 || isAttack3 || isAttack4)
			return true;

		return false;
	}

	//Make false
	void FalseAttack(){
		isAttack0 = false;
		isAttack1 = false;
		isAttack2 = false;
		isAttack3 = false;
		isAttack4 = false;
	}

	void FalseAir(){
		DashJump = false;
	}

	// Update is called once per frame
	void Update () {
		//Get value
		if ((Input.GetKeyDown (KeyCode.D) || Input.GetKeyDown(KeyCode.A)) && !isDash) {
			LastGo = TempGo;
			TempGo = Time.timeSinceLevelLoad;
		}
		if (Input.GetKeyUp (KeyCode.D)) LastDirection = RIGHT;
		if (Input.GetKeyUp (KeyCode.A)) LastDirection = LEFT; 

		//State change
		if (Input.GetKeyDown (KeyCode.K) && !isJump && !isFree () && !Attacking () && !isDash) { // the more complex state is topper
			if (state == WALK_R)
				state = JUMP_TOWARD_R;
			else if (state == WALK_L)
				state = JUMP_TOWARD_L;
			
			IndepenIndex = 0;
			isJump = true;

		} else if (isJump) {                                              //Air Action
			if (Input.GetKeyDown (KeyCode.K) && !isAirJump){
				if (direction == LEFT){
					state = JUMP_TOWARD_L;
					IndepenIndex = 0;
					isAirJump = true;
				} else {
					state = JUMP_TOWARD_R;
					IndepenIndex = 0;
					isAirJump = true;
				}
			} else if (Input.GetKeyDown(KeyCode.D) && Dashing () && !Attacking () && !isDash && !isStop){
				if (direction == LastDirection){
					state = DASH_AIR_R_FRONT;
					IndepenIndex = 0;
					isDash = true;
					FalseAir();
				} else {
					state = DASH_AIR_L_BACK;
					IndepenIndex = 0;
					isDash = true;
					FalseAir();
				} 
			} else if (Input.GetKeyDown(KeyCode.A) && Dashing () && !Attacking () && !isDash && !isStop){
				if (direction == LastDirection){
					state = DASH_AIR_L_FRONT;
					IndepenIndex = 0;
					isDash = true;
					FalseAir();
				} else {
					state = DASH_AIR_R_BACK;
					IndepenIndex = 0;
					isDash = true;
					FalseAir();
				} 
			}
		} else if (Input.GetKeyDown (KeyCode.K) && isDash) {                                //Ground Action
			if (direction == RIGHT) {
				state = JUMP_TOWARD_R;
				IndepenIndex = 0;
				isJump = true;
				DashJump = true;
				isDash = false;
			} else {
				state = JUMP_TOWARD_L;
				IndepenIndex = 0;
				isJump = true;
				DashJump = true;
				isDash = false;
			}
		} else if (Input.GetKeyDown (KeyCode.J) && (LastState == ATTACK2_L || LastState == ATTACK2_R) && !isAttack3 && !isJump && isContinue1 ()) {
			if (direction == RIGHT)
				state = ATTACK3_R;
			else
				state = ATTACK3_L;
			
			FalseAttack ();
			isAttack3 = true;
			IndepenIndex = 0;
			LastAttack = Time.timeSinceLevelLoad;
			LastState = state;
		} else if (Input.GetKeyDown (KeyCode.J) && (LastState == ATTACK1_L || LastState == ATTACK1_R) && !isAttack2 && !isJump && isContinue0 ()) {
			if (direction == RIGHT)
				state = ATTACK2_R;
			else
				state = ATTACK2_L;
			
			FalseAttack ();
			isAttack2 = true;
			IndepenIndex = 0;
			LastAttack = Time.timeSinceLevelLoad;
			LastState = state;
		} else if (Input.GetKeyDown (KeyCode.J) && (LastState == ATTACK0_L || LastState == ATTACK0_R) && !isAttack1 && !isJump && isContinue0 ()) {
			if (direction == RIGHT)
				state = ATTACK1_R;
			else
				state = ATTACK1_L;
			
			FalseAttack ();
			isAttack1 = true;
			IndepenIndex = 0;
			LastAttack = Time.timeSinceLevelLoad;
			LastState = state;
		} else if (Input.GetKeyDown (KeyCode.J) && !isJump && !Attacking ()) {  
			if (direction == RIGHT)
				state = ATTACK0_R;
			else
				state = ATTACK0_L;
			
			isAttack0 = true;
			IndepenIndex = 0;
			LastAttack = Time.timeSinceLevelLoad;
			LastState = state;
		} else if (Input.GetKeyDown (KeyCode.D) && Dashing () && !Attacking () && !isDash && !isStop) {
			state = DASH_R;
			isDash = true;
			IndepenIndex = 0;
		} else if (Input.GetKeyDown (KeyCode.A) && Dashing () && !Attacking () && !isDash && !isStop) {
			state = DASH_L;
			isDash = true;
			IndepenIndex = Dash.Length / 2;
		} else if (Input.GetKey (KeyCode.A) && !isJump && !Attacking () && !isDash && !isStop) {
			state = WALK_L;
			direction = LEFT;
		} else if (Input.GetKey (KeyCode.D) && !isJump && !Attacking () && !isDash && !isStop) {
			state = WALK_R;
			direction = RIGHT;
		} else if (Input.GetKeyUp (KeyCode.D) && isDash && !isStop) {
			state = DASH_STOP_R;
			isDash = false;
			isStop = true;
			IndepenIndex = DASH_END + 1;
		} else if (Input.GetKeyUp (KeyCode.A) && isDash && !isStop) {
			state = DASH_STOP_L;
			isDash = false;
			isStop = true;
			IndepenIndex = DASH_END + 1;
		} else if (Input.GetKeyDown (KeyCode.K) && state == STAND_L && !isJump && !isDash && !isStop && !Attacking ()) {
			state = JUMP_L;
			IndepenIndex = 0;
			isJump = true;
		} else if (Input.GetKeyDown (KeyCode.K) && state == STAND_R && !isJump && !isDash && !isStop && !Attacking ()) {
			state = JUMP_R;
			IndepenIndex = 0;
			isJump = true;
		} else if (isJump || Attacking() || isDash || isStop) {
		
		} else {
			StopState ();
		}




		//Animation Play
		int index = (int)(Time.timeSinceLevelLoad * framesPersecond);
		if (state == STAND_R) {
			index = index % (Stand.Length / 2);
			PersonRenderer.sprite = Stand [index];
		}
		if (state == STAND_L) {
			index = index % (Stand.Length / 2) + (Stand.Length / 2);
			PersonRenderer.sprite = Stand [index];
		}
		if (state == WALK_R) {
			index = index % (Walk.Length / 2);
			PersonRenderer.sprite = Walk [index];
		}
		if (state == WALK_L) {
			index = index % (Walk.Length / 2) + (Walk.Length / 2);
			PersonRenderer.sprite = Walk [index];
		}
		if (state == JUMP_R) {
			IndepenIndex += Time.deltaTime * JumpFrames;
			PersonRenderer.sprite = Jump [(int)IndepenIndex];
			if (PersonRenderer.sprite == Jump [(Jump.Length / 2) - 1]) {
				StopState ();
				isJump = false;
			}
		}
		if (state == JUMP_L) {
			IndepenIndex += Time.deltaTime * JumpFrames;
			PersonRenderer.sprite = Jump [(int)IndepenIndex + (Jump.Length / 2)];
			if (PersonRenderer.sprite == Jump [Jump.Length - 1]) {
				StopState ();
				isJump = false;
			}
		}
		if (state == JUMP_TOWARD_R) {
			if (DashJump == true && JumpFrames == 9) JumpFrames -= 3;
			IndepenIndex += Time.deltaTime * JumpFrames;
			PersonRenderer.sprite = JumpToward[(int)IndepenIndex];
			if (PersonRenderer.sprite == JumpToward[(JumpToward.Length / 2) - 1]){
				StopState();
				isJump = false;
				if (DashJump){
					DashJump = false;
					JumpFrames += 3;
					if (Input.GetKey(KeyCode.D)) state = DASH_R;
					IndepenIndex = 0;
					isDash = true;
				}
				if (isAirJump){
					isAirJump = false;
				}
			}
		}
		if (state == JUMP_TOWARD_L) {
			if (DashJump == true && JumpFrames == 9) JumpFrames -= 3;
			IndepenIndex += Time.deltaTime * JumpFrames;
			PersonRenderer.sprite = JumpToward[(int)IndepenIndex + (JumpToward.Length / 2)];
			if (PersonRenderer.sprite == JumpToward[JumpToward.Length - 1]){
				StopState();
				isJump = false;
				if (DashJump){
					DashJump = false;
					JumpFrames += 3;
					if (Input.GetKey(KeyCode.A)) state = DASH_L;
					IndepenIndex = Dash.Length / 2;
					isDash = true;
				}
			}
		}
		if (state == DASH_R) {
			if ((int)IndepenIndex == DASH_END){
				IndepenIndex = DASH_BEGIN;
			}
			IndepenIndex += Time.deltaTime * framesPersecond;
			PersonRenderer.sprite = Dash[(int)IndepenIndex];
		}
		if (state == DASH_STOP_R) {
			IndepenIndex += Time.deltaTime * framesPersecond;
			PersonRenderer.sprite = Dash[(int)IndepenIndex];
			if (PersonRenderer.sprite == Dash[(Dash.Length / 2) - 1]){
				StopState ();
				isStop = false;
			}
		}
		if (state == DASH_L) {
			if ((int)IndepenIndex == DASH_END + (Dash.Length / 2)){
				IndepenIndex = DASH_BEGIN + (Dash.Length / 2);
			}
			IndepenIndex += Time.deltaTime * framesPersecond;
			PersonRenderer.sprite = Dash[(int)IndepenIndex];
		}
		if (state == DASH_STOP_L) {
			IndepenIndex += Time.deltaTime * framesPersecond;
			PersonRenderer.sprite = Dash[(int)IndepenIndex + (Dash.Length / 2)];
			if (PersonRenderer.sprite == Dash[Dash.Length - 1]){
				StopState ();
				isStop = false;
			}
		}
		if (state == DASH_AIR_R_FRONT) {
			IndepenIndex += Time.deltaTime * (framesPersecond - 8);
			PersonRenderer.sprite = DashAirF[(int)IndepenIndex];
			if (PersonRenderer.sprite == DashAirF[(DashAirF.Length / 2) - 1]){
				StopState();
				isDash = false;
				isJump = false;
			}
		}
		if (state == DASH_AIR_L_FRONT) {
			IndepenIndex += Time.deltaTime * (framesPersecond - 8);
			PersonRenderer.sprite = DashAirF[(int)IndepenIndex + (DashAirF.Length / 2)];
			if (PersonRenderer.sprite == DashAirF[DashAirF.Length - 1]){
				StopState();
				isDash = false;
				isJump = false;
			}
		}
		if (state == DASH_AIR_R_BACK) {
			IndepenIndex += Time.deltaTime * (framesPersecond - 10);
			PersonRenderer.sprite = DashAirB[(int)IndepenIndex];
			if (PersonRenderer.sprite == DashAirB[(DashAirB.Length / 2) - 1]){
				StopState();
				isDash = false;
				isJump = false;
			}
		}
		if (state == DASH_AIR_L_BACK) {
			IndepenIndex += Time.deltaTime * (framesPersecond - 10);
			PersonRenderer.sprite = DashAirB[(int)IndepenIndex + (DashAirB.Length / 2)];
			if (PersonRenderer.sprite == DashAirB[DashAirB.Length - 1]){
				StopState();
				isDash = false;
				isJump = false;
			}
		}
		if (state == ATTACK0_R) {
			IndepenIndex += Time.deltaTime * framesPersecond;
			PersonRenderer.sprite = Attack0 [(int)IndepenIndex];
			if (PersonRenderer.sprite == Attack0 [(Attack0.Length / 2) - 1]) {
				StopState ();
				FalseAttack();
			}
		}
		if (state == ATTACK0_L) {
			IndepenIndex += Time.deltaTime * framesPersecond;
			PersonRenderer.sprite = Attack0 [(int)IndepenIndex + (Attack0.Length / 2)];
			if (PersonRenderer.sprite == Attack0 [Attack0.Length - 1]) {
				StopState ();
				FalseAttack ();
			}
		}
		if (state == ATTACK1_R) {
			IndepenIndex += Time.deltaTime * framesPersecond;
			PersonRenderer.sprite = Attack1 [(int)IndepenIndex];
			if (PersonRenderer.sprite == Attack1 [(Attack1.Length / 2) - 1]) {
				StopState ();
				FalseAttack();
			}
		}
		if (state == ATTACK1_L) {
			IndepenIndex += Time.deltaTime * framesPersecond;
			PersonRenderer.sprite = Attack1 [(int)IndepenIndex + (Attack1.Length / 2)];
			if (PersonRenderer.sprite == Attack1 [Attack1.Length - 1]) {
				StopState ();
				FalseAttack ();
			}
		}
		if (state == ATTACK2_R) {
			IndepenIndex += Time.deltaTime * framesPersecond;
			PersonRenderer.sprite = Attack2 [(int)IndepenIndex];
			if (PersonRenderer.sprite == Attack2 [(Attack2.Length / 2) - 1]) {
				StopState ();
				FalseAttack();
			}
		}
		if (state == ATTACK2_L) {
			IndepenIndex += Time.deltaTime * framesPersecond;
			PersonRenderer.sprite = Attack2 [(int)IndepenIndex + (Attack2.Length / 2)];
			if (PersonRenderer.sprite == Attack2 [Attack2.Length - 1]) {
				StopState ();
				FalseAttack ();
			}
		}
		if (state == ATTACK3_R) {
			IndepenIndex += Time.deltaTime * framesPersecond;
			PersonRenderer.sprite = Attack3 [(int)IndepenIndex];
			if (PersonRenderer.sprite == Attack3 [(Attack3.Length / 2) - 1]) {
				StopState ();
				FalseAttack();
			}
		}
		if (state == ATTACK3_L) {
			IndepenIndex += Time.deltaTime * framesPersecond;
			PersonRenderer.sprite = Attack3 [(int)IndepenIndex + (Attack3.Length / 2)];
			if (PersonRenderer.sprite == Attack3 [Attack3.Length - 1]) {
				StopState ();
				FalseAttack ();
			}
		}
	}
}

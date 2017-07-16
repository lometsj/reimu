using UnityEngine;
using System.Collections; 



public class control : MonoBehaviour {
	public Sprite[] spritesStand, spritesWalkFront, spritesJump;
	
	private SpriteRenderer spriteRenderer;

	public int controlc = 1;

	public int state;//状态
	
	public int direction;//当前方向
	
	public float speed;//速度
	
	public int jump;//
	
	//static int toground;
	
	//下面定义一些常量，常量用大写好吧；
	
	private static int REIMU_MOVE = 1;
	
	private static int REIMU_JUMP = 2;
	
	private static int REIMU_STOP = 3;

	private static int REIMU_DASH = 4;

	private static int REIMU_LEFT = 1;
	
	private static int REIMU_RIGHT = 2;
	
	private static int REIMU_LEFTJUMP = 21;
	
	private static int REIMU_RIGHTJUMP = 22;
	
	private static int REIMU_HOMELEFTJUMP = 23;
	
	private static int REIMU_HOMERIGHTJUMP = 24;
	
	private const float REIMU_JUMPTIME = 1f;


	
	
	// Use this for initialization
	void Start () {
		speed = 0.03f;
		state = REIMU_STOP;
		direction = REIMU_RIGHT;
		spriteRenderer = GetComponent<Renderer>() as SpriteRenderer;
		jump = 0;
		//toground = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if(controlc == -1)return;

		if(state == REIMU_MOVE && GetComponent<PAinimation>().Attacking() == false)
		{
			if(direction == REIMU_LEFT)
			{
				transform.Translate(0-speed,0f,0f);
			}
			else
			{
				transform.Translate(speed,0f,0f);
			}
		}
		else if(GetComponent<PAinimation>().Attacking() == true)
		{
			if(direction == REIMU_LEFT)
			{
				transform.Translate( (0-speed)*0.1f ,0f,0f);
			}
			else if(direction == REIMU_RIGHT)
			{
				transform.Translate(speed*0.1f,0f,0f);
			}
		}
		
		
		if(state == REIMU_STOP || state == REIMU_MOVE ) 
		{
			if(Input.GetKeyDown (KeyCode.A))
			{
				state = REIMU_MOVE;
				direction = REIMU_LEFT;
			}
			else if(Input.GetKeyDown(KeyCode.D))
			{
				state = REIMU_MOVE;
				direction = REIMU_RIGHT;
			}
			else if(Input.GetKeyUp(KeyCode.A) && !Input.GetKey(KeyCode.D))
			{
				state = REIMU_STOP;
			}
			else if(Input.GetKeyUp(KeyCode.D) && !Input.GetKey(KeyCode.A))
			{
				state = REIMU_STOP;
			}
			else if(Input.GetKey(KeyCode.K))
			{
				if(jump == 0)
				{
					print("jump");
					//transform.Translate(0f,1f,0f);
					GetComponent<Rigidbody2D>().AddForce(new Vector2(0f,200f));
					jump = 1;
				}
				
				if(jump == 2)
				{
					print ("jump2");
					//transform.Translate(0f,1f,0f);
					GetComponent<Rigidbody2D>().AddForce(new Vector2(0f,300f));
					jump = 3;
				}

			}
			else if(Input.GetKeyUp(KeyCode.K))
			{
				if(jump == 1)jump = 2;
			}

			else if(GetComponent<PAinimation>().state == 41 || GetComponent<PAinimation>().state == 43 || GetComponent<PAinimation>().state == 46)
			{
				transform.Translate((0-speed)*2,0f,0f);
			}

			else if(GetComponent<PAinimation>().state == 40 || GetComponent<PAinimation>().state == 42 || GetComponent<PAinimation>().state == 47)
			{
				transform.Translate(speed*2,0f,0f);
			}

		}
	}

	void OnCollisionEnter2D( Collision2D col ) //此段已废
	{
		/*Debug.Log(col.gameObject.name);
		
		//-----getsome some message-------
		Vector3 reimu_postion = transform.position;
		float x1 = reimu_postion.y;
		Vector3 box_size = col.gameObject.GetComponent<BoxCollider2D>().size;
		Vector3 box_position = col.gameObject.transform.position;
		//-----落地判断------
		float x2 = box_size.y / 2 + box_position.y; //也就是说现在只能测试盒状碰撞器的落地
		if(x1 >= x2)
		{
			if(GetComponent<PAinimation>().direction == 1)GetComponent<PAinimation>().state = 11;
			if(GetComponent<PAinimation>().direction == 0)GetComponent<PAinimation>().state = 10;
			jump = 0;
		}*/
	}

}

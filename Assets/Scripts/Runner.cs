using UnityEngine;
using System.Collections;

public class Runner : MonoBehaviour {

	public enum Gamestate { Playing, SetUp, Lost, Won, Blank };

	public int level;
	public Gamestate gamestate;
	public int turns;

	public GameObject tick;
	public GameObject goal;
	public GameObject bg;
	public Sprite WinSprite;
	public Sprite LostSprite;

	private SpriteRenderer tickRenderer;
	private SpriteRenderer goalRenderer;
	private SpriteRenderer bgRenderer;
	private bool rotatingClockwise;
	private bool ignoreTrigger;

	private float oldTickLocation;
	private float oldGoalLocation;
	private GoalTriggerCheck gtc;


	// Use this for initialization
	void Start () {
		level = 1;
		gamestate = Gamestate.SetUp;
		oldTickLocation = 0;
		oldGoalLocation = 0;
		turns = 1;
		tickRenderer = tick.GetComponent<SpriteRenderer> ();
		goalRenderer = goal.GetComponent<SpriteRenderer> ();
		bgRenderer = bg.GetComponent<SpriteRenderer> ();
		ignoreTrigger = false;
		tickRenderer.enabled = false;
		goalRenderer.enabled = false;
		rotatingClockwise = false;
		gtc = goal.GetComponent<GoalTriggerCheck> ();
		Setup ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			OnMouseDown ();
		}
		if (gamestate == Gamestate.Playing) {
			tick.transform.RotateAround (tick.transform.parent.position, 
				new Vector3 (0, 0, 1 * (rotatingClockwise ? 1 : -1)), Time.deltaTime*120*((level/5 + 1)*.5f));
		}
	}

	public void Lost()
	{
		if (ignoreTrigger) {
			ignoreTrigger = false;
			return;
		}
		Debug.Log("lost game");
		gamestate = Gamestate.Lost;
		goalRenderer.enabled = false;
		bgRenderer.sprite = LostSprite;
	}

	public void Setup()
	{
		float goalLocation = Random.Range (0, 360);
		float tickOffset = Random.Range (90, 270);
		float tickLocation = (goalLocation +tickOffset) % 360;

		Debug.Log (tickOffset);

		if(Mathf.Abs(tickOffset) > 180)
		{rotatingClockwise = true;}
		else
		{rotatingClockwise = false;}

		tick.transform.RotateAround (tick.transform.parent.position, new Vector3(0,0,1), tickLocation);
		goal.transform.RotateAround (tick.transform.parent.position, new Vector3(0,0,1), goalLocation);


		tickRenderer.enabled = true;
		goalRenderer.enabled = true;
		turns = level;
		gtc.tickOverlap = false;
		ignoreTrigger = false;
	}

	public void OnMouseDown()
	{
		switch (gamestate) { 
		case(Gamestate.Playing):
			Debug.Log (gtc.tickOverlap);
			if (gtc.tickOverlap) {
				if (turns == 1) {
					gamestate = Gamestate.Won;
					Debug.Log ("won");
				} else {
					turns--;
					rotatingClockwise = !rotatingClockwise;
					float goalLocation = Random.Range (40, 170) * (rotatingClockwise ? 1 : -1);
					goal.transform.RotateAround (tick.transform.parent.position, new Vector3(0,0,1), goalLocation);
					ignoreTrigger = true;
				}
			} else {
				Lost ();
				gamestate = Gamestate.Lost;
			}
			break;

		case(Gamestate.SetUp):
			
			gamestate = Gamestate.Playing;
			break;
		case(Gamestate.Lost):
			gamestate = Gamestate.SetUp;
			bgRenderer.sprite = WinSprite;
			tickRenderer.enabled = false;
			goalRenderer.enabled = false;
			Setup ();
			break;
		case (Gamestate.Won):
			level++;
			gamestate = Gamestate.SetUp;
			tickRenderer.enabled = false;
			goalRenderer.enabled = false;
			Setup ();
			break;
		}
	}
}

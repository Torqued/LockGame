using UnityEngine;
using System.Collections;

public class GoalTriggerCheck : MonoBehaviour {

	public bool tickOverlap = false;
	Runner runner;

	public void Start()
	{
		runner = (Runner) FindObjectOfType(typeof(Runner));
	}

	void OnTriggerEnter(Collider coll)
	{
		if (coll.gameObject.tag == "Tick") {
			tickOverlap = true;
		}

	}

	void OnTriggerExit(Collider coll)
	{
		if (coll.gameObject.tag == "Tick" && runner.gamestate == Runner.Gamestate.Playing) {
			runner.Lost ();
			tickOverlap = false;
		}

	}
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TurnsUI : MonoBehaviour {

	Text turns;
	Runner runner;
	// Use this for initialization
	void Start () {
	
	}

	void Awake()
	{
		turns = GetComponent<Text> ();
		runner = (Runner) FindObjectOfType(typeof(Runner));
	}

	// Update is called once per frame
	void Update () {
		turns.text = runner.turns.ToString();
	}
}

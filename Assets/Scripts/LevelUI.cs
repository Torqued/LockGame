using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelUI : MonoBehaviour {

	Text level;
	Runner runner;
	// Use this for initialization
	void Start () {

	}

	void Awake()
	{
		level = GetComponent<Text> ();
		runner = (Runner) FindObjectOfType(typeof(Runner));
	}

	// Update is called once per frame
	void Update () {
		level.text = "Level " + runner.level.ToString();
	}
}

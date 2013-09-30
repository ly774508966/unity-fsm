using System.Collections;
using UnityEngine;

public class Context : MonoBehaviour {
	public bool debug;
	public bool paused;
	public bool alive = true;
	public GameObject owner;
	public Diagram diagram;

	void Awake(){
		owner = gameObject;
	}
}
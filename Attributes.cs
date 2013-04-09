using System;
using UnityEngine;

public class Attributes : MonoBehaviour {

// transforms

	public Transform target;

// bools	

	public bool navigating;	
	public bool targeting;
	public bool debug;
	public bool paused;
	public bool attacking;

// floats

	public float attackDuration;
	public float attackRange;
	public float speed;
	public float rotationSpeed;
	public float attackDelay;
}
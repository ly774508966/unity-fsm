using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State {

	public string name;
	Action enter;
	Action step;
	Action exit;

	Dictionary< string, Transition> transitions = new Dictionary< string, Transition >();

	public State(){
		this.name = "unnamed";
	}

	public State( string name ){
		this.name = name;
	}


	public void Enter(Context context){
		if( enter != null) enter.Execute(context);
	}

	public State Execute(Context context){
		if( step != null ) step.Execute(context);
		State next = ExecuteTransitions(context);
		Debug.Log("State>Execute : " + (next != null ? next.name : "null") );
		return next;
	}

	public void Exit(Context context){
		if(	exit != null) exit.Execute(context);
	}

	public void SetStepAction( Action action ){
		step = action;
	}

	public void AddTransition( State target, Condition condition, bool inverse){
		transitions.Add( target.GetType().Name, new Transition( target, condition, inverse) );
	}

	public State ExecuteTransitions(Context context){
		foreach( KeyValuePair<string, Transition> transition in transitions){
			State next = transition.Value.Execute(context);
			if( next != null ) return next;
		}
		return null;
	}
}

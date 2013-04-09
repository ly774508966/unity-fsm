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
		if(context.attrs.debug)  Debug.Log("State>Execute : " + (next != null ? next.name : "null") );
		return next;
	}

	public void Exit(Context context){
		if(	exit != null) exit.Execute(context);
	}

	public void SetAction( Action action, string mode){
		switch(mode){
			case "step":
				step = action;
				break;
			case "enter":
				enter = action;
				break;
			case "exit":
				exit = action;
				break;
		}
	}

	public void AddTransition( State target, Condition[] conditions, bool inverse){
		Transition transition = new Transition( target, conditions, inverse);
		string key = target.name;
		while( this.transitions.ContainsKey( key )){
			key += '_';
		};
		this.transitions.Add( key , transition );
	}

	public State ExecuteTransitions(Context context){
		foreach( KeyValuePair<string, Transition> transition in transitions){
			State next = transition.Value.Execute(context);
			if( next != null ) return next;
		}
		return null;
	}
}

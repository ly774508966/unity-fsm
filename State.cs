using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State {

	public string name;
	Dictionary< string, Action> enter = new Dictionary< string, Action >();
	Dictionary< string, Action> step = new Dictionary< string, Action >();
	Dictionary< string, Action> exit = new Dictionary< string, Action >();
	Dictionary< string, Transition> transitions = new Dictionary< string, Transition >();

	public State(){
		this.name = "unnamed";
	}

	public State( string name ){
		this.name = name;
	}


	public void Enter(Context context){
		if( enter != null) ExecuteActions(context, enter);
	}

	public void Step(Context context){
		if( enter != null) ExecuteActions(context, step);
	}

	public void Exit(Context context){
		if( enter != null) ExecuteActions(context, exit);
	}

	public State Execute(Context context){
		if( step != null ) Step(context);
		State next = ExecuteTransitions(context);
		return next;
	}

	private void ExecuteActions(Context context, Dictionary< string, Action> actions){
		foreach( KeyValuePair<string, Action> action in actions)
		{
			action.Value.Execute(context);
		}
	}

	public void SetAction( Action action, string mode){
		switch(mode){
			case "step":
				step.Add( action.GetType().Name, action);
				break;
			case "enter":
				enter.Add( action.GetType().Name, action);
				break;
			case "exit":
				exit.Add( action.GetType().Name, action);
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

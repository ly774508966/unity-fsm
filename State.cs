using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State {

	public string name;
	public delegate void OnEnter();
	public delegate void OnStep();
	public delegate void OnExit();
	public delegate void OnFinish();
	public OnEnter EnterActions;
	public OnStep StepActions;
	public OnExit  ExitActions;
	public OnFinish FinishActions;
	Dictionary< string, object> transitions = new Dictionary< string, object >();

// Getters for action dictionaries

///
	public State()
	{
		this.name = "unnamed";
	}

	public State( string name )
	{
		this.name = name;
	}


	public void Enter(Context context)
	{
		if(EnterActions != null)
		{
			EnterActions();
		}
	}

	public void Step(Context context)
	{
		if(StepActions != null)
		{
			StepActions();
		}
	}

	public void Exit(Context context)
	{
		if(ExitActions != null)
		{
			ExitActions();
		}
	}

	public State Execute(Context context)
	{
		Step(context);
		State next = ExecuteTransitions(context);
		return next;
	}

	public Transition AddTransition( State target) 
	{
		Transition transition = new Transition( target );
		string key = target.name;
		while( this.transitions.ContainsKey( key ))
		{
			key += '_';
		};
		this.transitions.Add( key, transition);
		return transition;
	}

	public State ExecuteTransitions(Context context)
	{
		foreach( KeyValuePair<string, object> _transition in transitions)
		{
			Transition transition = (Transition) _transition.Value;
			State next = transition.Execute();
			if( next != null ) return next;
		}
		return null;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State {

	public string name;
	private Dictionary< string, Action> _enter = new Dictionary< string, Action >();
	private Dictionary< string, Action> _step = new Dictionary< string, Action >();
	private Dictionary< string, Action> _exit = new Dictionary< string, Action >();
	private Dictionary< string, Action> _finish = new Dictionary< string, Action >();
	Dictionary< string, object> transitions = new Dictionary< string, object >();

// Getters for action dictionaries

	public Dictionary< string, Action > enter
	{ get { return _enter; } }

	public Dictionary< string, Action > step
	{ get { return _step; } }

	public Dictionary< string, Action > exit
	{ get { return _exit; } }

	public Dictionary< string, Action > finish 
	{ get { return _finish; } }
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
		if( _enter != null) ExecuteActions(context, _enter);
		if( _finish != null) ExecuteActions(context, _finish);
	}

	public void Step(Context context)
	{
		if( _step != null) ExecuteActions(context, _step);
	}

	public void Exit(Context context)
	{
		if( _exit != null) ExecuteActions(context, _exit);
	}

	public State Execute(Context context)
	{
		if( _step != null ) Step(context);
		State next = ExecuteTransitions(context);
		return next;
	}

	private void ExecuteActions(Context context, Dictionary< string, Action> actions)
	{
		foreach( KeyValuePair<string, Action> action in actions)
		{
			action.Value.Execute(context);
		}
	}

	public void SetAction( Action action, string mode)
	{
		string actionType = action.GetType().Name;
		string name;
		switch(mode)
		{
			case "enter":
				name = GetUniqueKey( actionType, _enter );
				_enter.Add( name, action );
				break;
			case "step":
				name = GetUniqueKey( actionType, _step );
				_step.Add( name, action );
				break;
			case "exit":
				name = GetUniqueKey( actionType, _exit );
				_exit.Add( name, action );
				break;
			case "finish":
				name = GetUniqueKey( actionType, _finish );
				_finish.Add( name, action);
				break;
		}
	}

	private string GetUniqueKey( string basename, Dictionary< string, Action> dict )
	{
		string name = basename;
		int i = 1;
		while( dict.ContainsKey( name ))
		{
			name = basename + i.ToString();
			i++;
		}
		return name;
	}

	public Dictionary< string, object> GetTransitions()
	{
		return transitions; 
	}

	public void AddTransition( State target, Condition[] conditions, bool inverse)
	{
		Transition transition = new Transition( target, conditions);
		string key = target.name;
		while( this.transitions.ContainsKey( key ))
		{
			key += '_';
		};
		this.transitions.Add( key , transition );
	}

	public State ExecuteTransitions(Context context)
	{
		foreach( KeyValuePair<string, object> _transition in transitions)
		{
			Transition transition = (Transition) _transition.Value;
			State next = transition.Execute(context);
			if( next != null ) return next;
		}
		return null;
	}
}

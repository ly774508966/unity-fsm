using System;
using System.Collections.Generic;
using UnityEngine;

public class Diagram {

//  const's
    const uint MaxStateQty = 10;


//  instance var's
    private Context _context;
    private uint nextFreeSlot;
    private Dictionary<string, State> states = new Dictionary<string, State>();
    private State currentState;
    private State previousState;
    private State globalState;
    private int step;

// getters
    public Context context {
        get { return _context; }
    }

// manage _context
    public void SetContext(Context _context)
    {
        _context.diagram = this;
        this._context = _context;
    }

//  manage states
    public void AddStates( string name)
    {
        _addState(name);
    }

    public void AddStates( string[] states )
    {
        foreach( string name in states )
        {
            _addState(name);
        }
    }

    public void SetCurrentState( string name )
    {
        currentState = states[name];
    }

    // Set the global state, which will respond to conditions independent of the current local state
    public void SetCurrentGlobal( string name )
    {
        globalState = states[name];
    }

    public string GetCurrent()
    {
        return currentState.name;
    }

    public void ChangeState( State nextState )
    {
        currentState.Exit(_context);
        currentState = nextState;
        currentState.Enter(_context);
    }

// manage transitions
    public void AddTransition( string origin, string target, Condition condition)
    {
        _addTransition( origin, target, new Condition[]{ condition } );
    }

    public void AddTransition( string origin, string target, Condition[] conditions )
    {
        _addTransition( origin, target, conditions );
    }

    public void AddGlobalTransition( string origin, string target, Condition condition ) 
    {

    }

    public void While( string state, Action action)
    {
        states[state].SetAction(action, "step");
    }

    public void OnEnter( string state, Action action)
    {
        states[state].SetAction(action, "enter");
    }

    public void OnExit( string state, Action action)
    {
        states[state].SetAction(action, "exit");
    }

    public void OnFinish( string state, Action action)
    {
        states[state].SetAction(action, "finish");
    }

// manage actions
    public void SetAction( string state, Action action)
    {
        states[state].SetAction(action, "step");
    }

    public void SetAction( string state, string mode, Action action )
    {
        states[state].SetAction(action, mode);
    }

// flow methods
    public void Step()
    {
        if( globalState != null)
        {
            State next = globalState.Execute(_context);
            if(next != null) ChangeState( next );
        }
        if( currentState != null)
        {
            State next = currentState.Execute(_context);
            if(next != null) ChangeState( next );
        }
    }

// getters
    public State GetState(string name)
    {
        return states[name];
    }

    public int GetSize()
    {
        return states.Count;
    }

    public object[] GetConditions(string origin, string target)
    {
        return new object[]{ new object()};
    }

// private methods
    private void _addState( string name )
    {
        this.states.Add( name, new State(name) );
        var length = this.states.Count;
        if( length == 1) SetCurrentState(name);
    }
    private void _addTransition( string origin, string target, Condition[] conditions )
    {
        states[origin].AddTransition( states[target], conditions, true );
    }
}

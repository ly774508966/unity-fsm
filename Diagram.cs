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
    public int step;

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
    public Transition AddTransition( string origin, string target)
    {
        return _addTransition( origin, target);
    }

    public void AddGlobalTransition( string origin, string target, Condition condition ) 
    {

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
        step++;
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
    private Transition _addTransition( string origin, string target)
    {
        return states[origin].AddTransition( states[target] );
    }
}

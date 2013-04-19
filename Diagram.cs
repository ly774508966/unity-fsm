using System;
using System.Collections.Generic;
using UnityEngine;

public class Diagram {

//  const's
    const uint MaxStateQty = 10;
    Context context;


//  instance var's
    uint nextFreeSlot;
    private Dictionary<string, State> states = new Dictionary<string, State>();
    State currentState;
    State previousState;
    State globalState;

    int step;

//  ctor's

    public void AddStates( string[] states ){
        foreach( string name in states ){
            AddState(name);
        }
    }

    public void SetContext(Context context){
        this.context = context;
    }

    private void AddState( string name ){
        this.states.Add( name, new State(name) );
        var i = 0;
        foreach( var state in this.states){
            i++;
        };
        if( i == 0) SetCurrentState(name);
    }


    public void AddTransition( string origin, string target, Condition condition){
        _addTransition( origin, target, new Condition[]{ condition } );
    }

    // add transition with multiple conditions
    public void AddTransition( string origin, string target, Condition[] conditions ){
        _addTransition( origin, target, conditions );
    }

    public void AddGlobalTransition( string origin, string target, Condition condition ) {

    }

    public void SetCurrentState( string name ) {
        currentState = states[name];
    }

    public void SetCurrentGlobal( string name ){
        globalState = states[name];
    }

    void _addTransition( string origin, string target, Condition[] conditions ){
        states[origin].AddTransition( states[target], conditions, true );
    }

    public void SetAction( string state, Action action){
        states[state].SetAction(action, "step");
    }

    public void SetAction( string state, string mode, Action action ){
        states[state].SetAction(action, mode);
    }

    public void While( string state, Action action){
        states[state].SetAction(action, "step");
    }

    public void OnEnter( string state, Action action){
        states[state].SetAction(action, "enter");
    }

    public void OnExit( string state, Action action){
        states[state].SetAction(action, "exit");
    }

    public void Step(){
        if( currentState != null){
            State next = currentState.Execute(context);
            if(next != null) ChangeState( next );
        }
        if( globalState != null){
            State next = globalState.Execute(context);
            if(next != null) ChangeState( next );
        }
    }

    public void ChangeState( State nextState ){
        currentState.Exit(context);
        currentState = nextState;
        currentState.Enter(context);
    }

    public string GetCurrent(){
        return currentState.name;
    }
}

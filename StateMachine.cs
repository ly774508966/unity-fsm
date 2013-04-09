using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine {

//  const's
    const uint MaxStateQty = 10;
    Context context;


//  instance var's
    uint nextFreeSlot;
    Dictionary<string, State> states = new Dictionary<string, State>();
    State currentState;
    State previousState;
    State globalState;

    int step;

//  ctor's
    public StateMachine ( Context context ) {
        this.context = context ;
    }

    public void AddStates( string[] states ){
        foreach( string name in states ){
            this.states.Add( name, new State(name) );
        }
    }


    public void AddTransition( string origin, string target, Condition condition){
        states[origin].AddTransition( states[target], new Condition[]{ condition }, true );
    }

    public void AddTransition( string origin, string target, Condition condition, bool inverse){
        states[origin].AddTransition( states[target], new Condition[]{ condition }, inverse );
    }

    // add transition with multiple conditions
    public void AddTransition( string origin, string target, Condition[] conditions ){
        states[origin].AddTransition( states[target], conditions , true );
    }

    public void SetCurrentState( string name ) {
        currentState = states[name];
    }

    public void SetAction( string state, Action action){
        states[state].SetAction(action, "step");
    }

    public void SetAction( string state, string mode, Action action ){
        states[state].SetAction(action, mode);
    }

    public void Step(){
        if(context.debug) Debug.Log("Step: " + step++);
        if( globalState != null){
            globalState.Execute(context);
        }
        if( currentState != null){
            State next = currentState.Execute(context);
            if(next != null) ChangeState( next );
            //Debug.Log(currentState.name);
        }
    }

    public void ChangeState( State nextState ){
        if(context.debug)  Debug.Log("ChangeState: " + nextState.name );
        currentState.Exit(context);
        currentState = nextState;
        currentState.Enter(context);
    }

    public string GetCurrent(){
        return currentState.name;
    }
}

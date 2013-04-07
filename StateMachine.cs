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
        states[origin].AddTransition( states[target], condition, true );
    }

    public void AddTransition( string origin, string target, Condition condition, bool inverse){
        states[origin].AddTransition( states[target], condition, inverse );
    }

    public void SetCurrentState( string name ) {
        currentState = states[name];
    }

    public void SetAction( string state, Action action){
        states[state].SetStepAction(action);
    }

    public void Step(){
        Debug.Log("Step: " + step++);
        if( globalState != null){
            globalState.Execute(context);
        }
        if( currentState != null){
            State next = currentState.Execute(context);
            if(next != null) ChangeState( next );
        }
    }

    public void ChangeState( State nextState ){
        Debug.Log("ChangeState: " + nextState.name );
        currentState.Exit(context);
        currentState = nextState;
        currentState.Enter(context);
    }
}

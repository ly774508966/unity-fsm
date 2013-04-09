using System.Collections;
using UnityEngine;

public class Transition {


	State target;	
	Condition[] conditions;
	bool inverse;

	public Transition( State target, Condition[] conditions, bool inverse ) {
		this.inverse = inverse;
		this.target = target;
		this.conditions = conditions;
	}

	public State Execute(Context context) {
		foreach( Condition condition in conditions){
			bool result = condition.Execute(context);
			//Debug.Log( condition.GetType().Name);
			if( result != condition.target ) return null;
		}
		return target;
	}
	
}

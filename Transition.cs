using System.Collections;
using UnityEngine;

public class Transition {


	State target;	
	Condition condition;
	bool inverse;

	public Transition( State target, Condition condition, bool inverse ) {
		this.inverse = inverse;
		this.target = target;
		this.condition = condition;
	}

	public State Execute(Context context) {
		bool result = condition.Execute(context);
		result = inverse ? result : !result;
		if( result ){
			return target;
		}
		return null;
	}
	
}

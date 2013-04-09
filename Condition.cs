using System;
using UnityEngine;

public abstract class Condition {
	public bool target;
	public Condition(){
		this.target = true;
	}
	public Condition( bool target ){
		this.target = target ;
	}

	public abstract bool Execute(Context context);
	public bool Exist( object[] fields ){
		foreach( object field in fields ){
			if( field == null ) return false;
		}
		return true;
	}
}
using System;
using UnityEngine;
using System.Reflection;

public abstract class Condition {
	public bool target;
	public FieldInfo info;
	public float floatVariable;
	public Condition(){
		this.target = true;
	}
	public Condition( bool target ){
		this.target = target ;
	}

	public Condition( float variable){
		this.floatVariable = variable;
	}

	public Condition( string fieldName, bool target, Context context){
		this.target = target;
		info = context.GetType().GetField(fieldName);
	}

	public abstract bool Execute(Context context);
	public bool Exist( Context[] fields ){
		foreach( Context field in fields ){
			if( field == null ) return false;
		}
		return true;
	}
}
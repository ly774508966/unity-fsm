using System;
using UnityEngine;
using System.Reflection;

public class Condition {
	public bool normal = true;
	public FieldInfo info;
	public float floatVariable;
	public Condition(){
		this.normal = true;
	}
	public Condition Invert()
	{
		// this is called to invert the desired condition

		this.normal =  false;
		return this;
	}
	public Condition SetFloat(float _floatVariable)
	{
		// this is called to set a float variable used for
		// the condition check.

		this.floatVariable = _floatVariable; 
		return this;
	}
	public Condition SetInfo( string fieldName, Context context)
	{
		this.info = context.GetType().GetField(fieldName);
		return this;
	}
	public virtual bool Execute(Context context){ return true; }
	public bool Exist( Context[] fields ){
		foreach( Context field in fields ){
			if( field == null ) return false;
		}
		return true;
	}
}
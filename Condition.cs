using System;
using UnityEngine;
using System.Reflection;

public class Condition {
	public bool normal = true;
	public FieldInfo info;
	public FieldInfo bool1;
	public float floatVariable;
	public delegate bool Check();
	public Check check;
	private Context context;

	private bool bool2;

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

	public Condition Equals( string fieldName, bool second)
	{
		this.bool1= context.GetType().GetField(fieldName);
		this.bool2 = second;
		check = this.CheckEquals;
		return this;
	}

	private bool CheckEquals()
	{
		return (bool)bool1.GetValue(context) == bool2;
	}
}
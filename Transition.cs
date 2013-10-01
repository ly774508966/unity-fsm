using System.Collections;
using System;
using UnityEngine;

public class Transition
{
	public State target;	
	public delegate bool CheckCondition();
	public CheckCondition conditions;

	public Transition ( State target)
	{
		this.target = target;
	}

	public State Execute()
	{
		if( conditions != null) 
		{
			foreach( CheckCondition condition in conditions.GetInvocationList() )
			{
				bool result = condition();
				if( result == false ) return null;
			}

			return target;
		}
		else 
		{
			return null;
		}
	}

	public Transition Equals( string fieldName, bool second)
	{
		Condition condition = new Condition().Equals(fieldName, second);

		return this;
	}

	public Transition Equals( ref int first, int second)
	{
		return this;
	}
	
}

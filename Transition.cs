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
}

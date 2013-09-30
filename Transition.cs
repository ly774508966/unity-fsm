using System.Collections;
using UnityEngine;

public class Transition
{
	public State target;	
	public Condition[] conditions;

	public Transition( State target, Condition[] conditions)
	{
		this.target = target;
		this.conditions = conditions;
	}

	public State Execute(Context context)
	{
		foreach( Condition condition in conditions)
		{
			bool result = condition.Execute(context);
			if( result != condition.normal ) return null;
		}
		return target;
	}

	public void AddCondition( Condition condition)
	{
		Condition[] current = conditions;	
		conditions = new Condition[conditions.Length + 1];
		for (int i = 0; i < current.Length; i++)
		{
			conditions[i] = current[i];
		}
		conditions[conditions.Length-1] = condition;
	}
	
}

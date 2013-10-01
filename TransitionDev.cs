using System.Collections;
using UnityEngine;

public class TransitionDev 
{
	public State target;	
	public delegate bool CheckCondition();
	CheckCondition conditions;

	public TransitionDev()
	{
		conditions = Delegate;
	}

	private bool Delegate()
	{
		return false;
	}

	public State Execute(Context context)
	{
		// foreach( Condition condition in conditions)
		// {
		// 	bool result = condition.Execute(context);
		// 	if( result != condition.normal ) return null;
		// }

		return target;
	}

	public bool Test()
	{
		return conditions();
	}

	public void AddCondition( Condition condition)
	{
		// Condition[] current = conditions;	
		// conditions = new Condition[conditions.Length + 1];
		// for (int i = 0; i < current.Length; i++)
		// {
		// 	conditions[i] = current[i];
		// }
		// conditions[conditions.Length-1] = condition;
	}
	
}

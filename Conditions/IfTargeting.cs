using System;
using UnityEngine;

public class IfTargeting : Condition {
	public override bool Execute(Context context){
		return context.attributes.navigating;
	}	
}
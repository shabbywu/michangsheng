using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime;

[Serializable]
public class SharedObject : SharedVariable<Object>
{
	public static explicit operator SharedObject(Object value)
	{
		return new SharedObject
		{
			mValue = value
		};
	}
}

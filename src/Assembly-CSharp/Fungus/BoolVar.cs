using System;
using UnityEngine;

namespace Fungus;

[Serializable]
public class BoolVar
{
	[SerializeField]
	protected string key;

	[SerializeField]
	protected bool value;

	public string Key
	{
		get
		{
			return key;
		}
		set
		{
			key = value;
		}
	}

	public bool Value
	{
		get
		{
			return value;
		}
		set
		{
			this.value = value;
		}
	}
}

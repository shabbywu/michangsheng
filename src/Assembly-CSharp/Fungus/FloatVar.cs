using System;
using UnityEngine;

namespace Fungus;

[Serializable]
public class FloatVar
{
	[SerializeField]
	protected string key;

	[SerializeField]
	protected float value;

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

	public float Value
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

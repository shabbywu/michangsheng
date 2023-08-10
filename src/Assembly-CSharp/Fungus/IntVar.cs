using System;
using UnityEngine;

namespace Fungus;

[Serializable]
public class IntVar
{
	[SerializeField]
	protected string key;

	[SerializeField]
	protected int value;

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

	public int Value
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

using System;
using UnityEngine;

namespace Fungus;

[Serializable]
public class StringVar
{
	[SerializeField]
	protected string key;

	[SerializeField]
	protected string value;

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

	public string Value
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

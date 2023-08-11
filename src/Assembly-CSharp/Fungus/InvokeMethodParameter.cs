using System;
using UnityEngine;

namespace Fungus;

[Serializable]
public class InvokeMethodParameter
{
	[SerializeField]
	public ObjectValue objValue;

	[SerializeField]
	public string variableKey;
}

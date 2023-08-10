using System;
using UnityEngine;

namespace Fungus;

public class ControlWithDisplay<TDisplayEnum> : Command
{
	[Tooltip("Display type")]
	[SerializeField]
	protected TDisplayEnum display;

	public virtual TDisplayEnum Display => display;

	protected virtual bool IsDisplayNone<TEnum>(TEnum enumValue)
	{
		return Enum.GetName(typeof(TEnum), enumValue) == "None";
	}
}

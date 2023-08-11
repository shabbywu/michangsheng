using System;
using UnityEngine;

namespace Fungus;

[EventHandlerInfo("MonoBehaviour", "Update", "The block will execute every chosen Update, or FixedUpdate or LateUpdate.")]
[AddComponentMenu("")]
public class UpdateTick : EventHandler
{
	[Flags]
	public enum UpdateMessageFlags
	{
		Update = 1,
		FixedUpdate = 2,
		LateUpdate = 4
	}

	[Tooltip("Which of the Update messages to trigger on.")]
	[SerializeField]
	[EnumFlag]
	protected UpdateMessageFlags FireOn = UpdateMessageFlags.Update;

	private void Update()
	{
		if ((FireOn & UpdateMessageFlags.Update) != 0)
		{
			ExecuteBlock();
		}
	}

	private void FixedUpdate()
	{
		if ((FireOn & UpdateMessageFlags.FixedUpdate) != 0)
		{
			ExecuteBlock();
		}
	}

	private void LateUpdate()
	{
		if ((FireOn & UpdateMessageFlags.LateUpdate) != 0)
		{
			ExecuteBlock();
		}
	}
}

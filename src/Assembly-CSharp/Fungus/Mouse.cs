using System;
using UnityEngine;

namespace Fungus;

[EventHandlerInfo("MonoBehaviour", "Mouse", "The block will execute when the desired OnMouse* message for the monobehaviour is received")]
[AddComponentMenu("")]
public class Mouse : EventHandler
{
	[Flags]
	public enum MouseMessageFlags
	{
		OnMouseDown = 1,
		OnMouseDrag = 2,
		OnMouseEnter = 4,
		OnMouseExit = 8,
		OnMouseOver = 0x10,
		OnMouseUp = 0x20,
		OnMouseUpAsButton = 0x40
	}

	[Tooltip("Which of the Mouse messages to trigger on.")]
	[SerializeField]
	[EnumFlag]
	protected MouseMessageFlags FireOn = MouseMessageFlags.OnMouseUpAsButton;

	private void OnMouseDown()
	{
		HandleTriggering(MouseMessageFlags.OnMouseDown);
	}

	private void OnMouseDrag()
	{
		HandleTriggering(MouseMessageFlags.OnMouseDrag);
	}

	private void OnMouseEnter()
	{
		HandleTriggering(MouseMessageFlags.OnMouseEnter);
	}

	private void OnMouseExit()
	{
		HandleTriggering(MouseMessageFlags.OnMouseExit);
	}

	private void OnMouseOver()
	{
		HandleTriggering(MouseMessageFlags.OnMouseOver);
	}

	private void OnMouseUp()
	{
		HandleTriggering(MouseMessageFlags.OnMouseUp);
	}

	private void OnMouseUpAsButton()
	{
		HandleTriggering(MouseMessageFlags.OnMouseUpAsButton);
	}

	private void HandleTriggering(MouseMessageFlags from)
	{
		if ((from & FireOn) != 0)
		{
			ExecuteBlock();
		}
	}
}

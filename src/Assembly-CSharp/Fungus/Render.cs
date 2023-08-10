using System;
using UnityEngine;

namespace Fungus;

[EventHandlerInfo("MonoBehaviour", "Render", "The block will execute when the desired Rendering related message for the monobehaviour is received.")]
[AddComponentMenu("")]
public class Render : EventHandler
{
	[Flags]
	public enum RenderMessageFlags
	{
		OnPostRender = 1,
		OnPreCull = 2,
		OnPreRender = 4,
		OnRenderObject = 0x10,
		OnWillRenderObject = 0x20,
		OnBecameInvisible = 0x40,
		OnBecameVisible = 0x80
	}

	[Tooltip("Which of the Rendering messages to trigger on.")]
	[SerializeField]
	[EnumFlag]
	protected RenderMessageFlags FireOn = RenderMessageFlags.OnWillRenderObject;

	private void OnPostRender()
	{
		if ((FireOn & RenderMessageFlags.OnPostRender) != 0)
		{
			ExecuteBlock();
		}
	}

	private void OnPreCull()
	{
		if ((FireOn & RenderMessageFlags.OnPreCull) != 0)
		{
			ExecuteBlock();
		}
	}

	private void OnPreRender()
	{
		if ((FireOn & RenderMessageFlags.OnPreRender) != 0)
		{
			ExecuteBlock();
		}
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
	}

	private void OnRenderObject()
	{
		if ((FireOn & RenderMessageFlags.OnRenderObject) != 0)
		{
			ExecuteBlock();
		}
	}

	private void OnWillRenderObject()
	{
		if ((FireOn & RenderMessageFlags.OnWillRenderObject) != 0)
		{
			ExecuteBlock();
		}
	}

	private void OnBecameInvisible()
	{
		if ((FireOn & RenderMessageFlags.OnBecameInvisible) != 0)
		{
			ExecuteBlock();
		}
	}

	private void OnBecameVisible()
	{
		if ((FireOn & RenderMessageFlags.OnBecameVisible) != 0)
		{
			ExecuteBlock();
		}
	}
}

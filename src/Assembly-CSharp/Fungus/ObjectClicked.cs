using System.Collections;
using UnityEngine;

namespace Fungus;

[EventHandlerInfo("Sprite", "Object Clicked", "The block will execute when the user clicks or taps on the clickable object.")]
[AddComponentMenu("")]
public class ObjectClicked : EventHandler
{
	public class ObjectClickedEvent
	{
		public Clickable2D ClickableObject;

		public ObjectClickedEvent(Clickable2D clickableObject)
		{
			ClickableObject = clickableObject;
		}
	}

	[Tooltip("Object that the user can click or tap on")]
	[SerializeField]
	protected Clickable2D clickableObject;

	[Tooltip("Wait for a number of frames before executing the block.")]
	[SerializeField]
	protected int waitFrames = 1;

	protected EventDispatcher eventDispatcher;

	protected virtual void OnEnable()
	{
		eventDispatcher = FungusManager.Instance.EventDispatcher;
		eventDispatcher.AddListener<ObjectClickedEvent>(OnObjectClickedEvent);
	}

	protected virtual void OnDisable()
	{
		eventDispatcher.RemoveListener<ObjectClickedEvent>(OnObjectClickedEvent);
		eventDispatcher = null;
	}

	private void OnObjectClickedEvent(ObjectClickedEvent evt)
	{
		OnObjectClicked(evt.ClickableObject);
	}

	protected virtual IEnumerator DoExecuteBlock(int numFrames)
	{
		if (numFrames == 0)
		{
			ExecuteBlock();
			yield break;
		}
		int count = Mathf.Max(waitFrames, 1);
		while (count > 0)
		{
			count--;
			yield return (object)new WaitForEndOfFrame();
		}
		ExecuteBlock();
	}

	public virtual void OnObjectClicked(Clickable2D clickableObject)
	{
		if ((Object)(object)clickableObject == (Object)(object)this.clickableObject)
		{
			((MonoBehaviour)this).StartCoroutine(DoExecuteBlock(waitFrames));
		}
	}

	public override string GetSummary()
	{
		if ((Object)(object)clickableObject != (Object)null)
		{
			return ((Object)clickableObject).name;
		}
		return "None";
	}
}

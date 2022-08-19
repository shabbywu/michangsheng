using System;
using System.Collections;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000EAF RID: 3759
	[EventHandlerInfo("Sprite", "Object Clicked", "The block will execute when the user clicks or taps on the clickable object.")]
	[AddComponentMenu("")]
	public class ObjectClicked : EventHandler
	{
		// Token: 0x06006A49 RID: 27209 RVA: 0x00292CDD File Offset: 0x00290EDD
		protected virtual void OnEnable()
		{
			this.eventDispatcher = FungusManager.Instance.EventDispatcher;
			this.eventDispatcher.AddListener<ObjectClicked.ObjectClickedEvent>(new EventDispatcher.TypedDelegate<ObjectClicked.ObjectClickedEvent>(this.OnObjectClickedEvent));
		}

		// Token: 0x06006A4A RID: 27210 RVA: 0x00292D06 File Offset: 0x00290F06
		protected virtual void OnDisable()
		{
			this.eventDispatcher.RemoveListener<ObjectClicked.ObjectClickedEvent>(new EventDispatcher.TypedDelegate<ObjectClicked.ObjectClickedEvent>(this.OnObjectClickedEvent));
			this.eventDispatcher = null;
		}

		// Token: 0x06006A4B RID: 27211 RVA: 0x00292D26 File Offset: 0x00290F26
		private void OnObjectClickedEvent(ObjectClicked.ObjectClickedEvent evt)
		{
			this.OnObjectClicked(evt.ClickableObject);
		}

		// Token: 0x06006A4C RID: 27212 RVA: 0x00292D34 File Offset: 0x00290F34
		protected virtual IEnumerator DoExecuteBlock(int numFrames)
		{
			if (numFrames == 0)
			{
				this.ExecuteBlock();
				yield break;
			}
			int count = Mathf.Max(this.waitFrames, 1);
			while (count > 0)
			{
				int num = count;
				count = num - 1;
				yield return new WaitForEndOfFrame();
			}
			this.ExecuteBlock();
			yield break;
		}

		// Token: 0x06006A4D RID: 27213 RVA: 0x00292D4A File Offset: 0x00290F4A
		public virtual void OnObjectClicked(Clickable2D clickableObject)
		{
			if (clickableObject == this.clickableObject)
			{
				base.StartCoroutine(this.DoExecuteBlock(this.waitFrames));
			}
		}

		// Token: 0x06006A4E RID: 27214 RVA: 0x00292D6D File Offset: 0x00290F6D
		public override string GetSummary()
		{
			if (this.clickableObject != null)
			{
				return this.clickableObject.name;
			}
			return "None";
		}

		// Token: 0x040059E3 RID: 23011
		[Tooltip("Object that the user can click or tap on")]
		[SerializeField]
		protected Clickable2D clickableObject;

		// Token: 0x040059E4 RID: 23012
		[Tooltip("Wait for a number of frames before executing the block.")]
		[SerializeField]
		protected int waitFrames = 1;

		// Token: 0x040059E5 RID: 23013
		protected EventDispatcher eventDispatcher;

		// Token: 0x020016FB RID: 5883
		public class ObjectClickedEvent
		{
			// Token: 0x060088A0 RID: 34976 RVA: 0x002E9A19 File Offset: 0x002E7C19
			public ObjectClickedEvent(Clickable2D clickableObject)
			{
				this.ClickableObject = clickableObject;
			}

			// Token: 0x0400749C RID: 29852
			public Clickable2D ClickableObject;
		}
	}
}

using System;
using System.Collections;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001336 RID: 4918
	[EventHandlerInfo("Sprite", "Object Clicked", "The block will execute when the user clicks or taps on the clickable object.")]
	[AddComponentMenu("")]
	public class ObjectClicked : EventHandler
	{
		// Token: 0x0600777F RID: 30591 RVA: 0x000517B5 File Offset: 0x0004F9B5
		protected virtual void OnEnable()
		{
			this.eventDispatcher = FungusManager.Instance.EventDispatcher;
			this.eventDispatcher.AddListener<ObjectClicked.ObjectClickedEvent>(new EventDispatcher.TypedDelegate<ObjectClicked.ObjectClickedEvent>(this.OnObjectClickedEvent));
		}

		// Token: 0x06007780 RID: 30592 RVA: 0x000517DE File Offset: 0x0004F9DE
		protected virtual void OnDisable()
		{
			this.eventDispatcher.RemoveListener<ObjectClicked.ObjectClickedEvent>(new EventDispatcher.TypedDelegate<ObjectClicked.ObjectClickedEvent>(this.OnObjectClickedEvent));
			this.eventDispatcher = null;
		}

		// Token: 0x06007781 RID: 30593 RVA: 0x000517FE File Offset: 0x0004F9FE
		private void OnObjectClickedEvent(ObjectClicked.ObjectClickedEvent evt)
		{
			this.OnObjectClicked(evt.ClickableObject);
		}

		// Token: 0x06007782 RID: 30594 RVA: 0x0005180C File Offset: 0x0004FA0C
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

		// Token: 0x06007783 RID: 30595 RVA: 0x00051822 File Offset: 0x0004FA22
		public virtual void OnObjectClicked(Clickable2D clickableObject)
		{
			if (clickableObject == this.clickableObject)
			{
				base.StartCoroutine(this.DoExecuteBlock(this.waitFrames));
			}
		}

		// Token: 0x06007784 RID: 30596 RVA: 0x00051845 File Offset: 0x0004FA45
		public override string GetSummary()
		{
			if (this.clickableObject != null)
			{
				return this.clickableObject.name;
			}
			return "None";
		}

		// Token: 0x04006824 RID: 26660
		[Tooltip("Object that the user can click or tap on")]
		[SerializeField]
		protected Clickable2D clickableObject;

		// Token: 0x04006825 RID: 26661
		[Tooltip("Wait for a number of frames before executing the block.")]
		[SerializeField]
		protected int waitFrames = 1;

		// Token: 0x04006826 RID: 26662
		protected EventDispatcher eventDispatcher;

		// Token: 0x02001337 RID: 4919
		public class ObjectClickedEvent
		{
			// Token: 0x06007786 RID: 30598 RVA: 0x00051875 File Offset: 0x0004FA75
			public ObjectClickedEvent(Clickable2D clickableObject)
			{
				this.ClickableObject = clickableObject;
			}

			// Token: 0x04006827 RID: 26663
			public Clickable2D ClickableObject;
		}
	}
}

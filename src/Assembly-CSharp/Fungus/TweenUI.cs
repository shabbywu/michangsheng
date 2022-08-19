using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E57 RID: 3671
	public abstract class TweenUI : Command
	{
		// Token: 0x0600671A RID: 26394 RVA: 0x00288CDC File Offset: 0x00286EDC
		protected virtual void ApplyTween()
		{
			for (int i = 0; i < this.targetObjects.Count; i++)
			{
				GameObject gameObject = this.targetObjects[i];
				if (!(gameObject == null))
				{
					this.ApplyTween(gameObject);
				}
			}
			if (this.waitUntilFinished)
			{
				LeanTween.value(base.gameObject, 0f, 1f, this.duration).setOnComplete(new Action(this.OnComplete));
			}
		}

		// Token: 0x0600671B RID: 26395
		protected abstract void ApplyTween(GameObject go);

		// Token: 0x0600671C RID: 26396 RVA: 0x0005E3AF File Offset: 0x0005C5AF
		protected virtual void OnComplete()
		{
			this.Continue();
		}

		// Token: 0x0600671D RID: 26397 RVA: 0x001D84A0 File Offset: 0x001D66A0
		protected virtual string GetSummaryValue()
		{
			return "";
		}

		// Token: 0x0600671E RID: 26398 RVA: 0x00288D5C File Offset: 0x00286F5C
		public override void OnEnter()
		{
			if (this.targetObjects.Count == 0)
			{
				this.Continue();
				return;
			}
			this.ApplyTween();
			if (!this.waitUntilFinished)
			{
				this.Continue();
			}
		}

		// Token: 0x0600671F RID: 26399 RVA: 0x00288D8B File Offset: 0x00286F8B
		public override void OnCommandAdded(Block parentBlock)
		{
			if (this.targetObjects.Count == 0)
			{
				this.targetObjects.Add(null);
			}
		}

		// Token: 0x06006720 RID: 26400 RVA: 0x00288DA8 File Offset: 0x00286FA8
		public override string GetSummary()
		{
			if (this.targetObjects.Count == 0)
			{
				return "Error: No targetObjects selected";
			}
			if (this.targetObjects.Count != 1)
			{
				string text = "";
				for (int i = 0; i < this.targetObjects.Count; i++)
				{
					GameObject gameObject = this.targetObjects[i];
					if (!(gameObject == null))
					{
						if (text == "")
						{
							text += gameObject.name;
						}
						else
						{
							text = text + ", " + gameObject.name;
						}
					}
				}
				return text + " = " + this.GetSummaryValue();
			}
			if (this.targetObjects[0] == null)
			{
				return "Error: No targetObjects selected";
			}
			return this.targetObjects[0].name + " = " + this.GetSummaryValue();
		}

		// Token: 0x06006721 RID: 26401 RVA: 0x002868C5 File Offset: 0x00284AC5
		public override Color GetButtonColor()
		{
			return new Color32(180, 250, 250, byte.MaxValue);
		}

		// Token: 0x06006722 RID: 26402 RVA: 0x002868F3 File Offset: 0x00284AF3
		public override bool IsReorderableArray(string propertyName)
		{
			return propertyName == "targetObjects";
		}

		// Token: 0x06006723 RID: 26403 RVA: 0x00288E83 File Offset: 0x00287083
		public override bool HasReference(Variable variable)
		{
			return this.waitUntilFinished.booleanRef == variable || this.duration.floatRef == variable || base.HasReference(variable);
		}

		// Token: 0x0400582F RID: 22575
		[Tooltip("List of objects to be affected by the tween")]
		[SerializeField]
		protected List<GameObject> targetObjects = new List<GameObject>();

		// Token: 0x04005830 RID: 22576
		[Tooltip("Type of tween easing to apply")]
		[SerializeField]
		protected LeanTweenType tweenType = LeanTweenType.easeOutQuad;

		// Token: 0x04005831 RID: 22577
		[Tooltip("Wait until this command completes before continuing execution")]
		[SerializeField]
		protected BooleanData waitUntilFinished = new BooleanData(true);

		// Token: 0x04005832 RID: 22578
		[Tooltip("Time for the tween to complete")]
		[SerializeField]
		protected FloatData duration = new FloatData(1f);
	}
}

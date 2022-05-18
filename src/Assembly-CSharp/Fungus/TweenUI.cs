using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020012AA RID: 4778
	public abstract class TweenUI : Command
	{
		// Token: 0x060073A8 RID: 29608 RVA: 0x002AB914 File Offset: 0x002A9B14
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

		// Token: 0x060073A9 RID: 29609
		protected abstract void ApplyTween(GameObject go);

		// Token: 0x060073AA RID: 29610 RVA: 0x00011424 File Offset: 0x0000F624
		protected virtual void OnComplete()
		{
			this.Continue();
		}

		// Token: 0x060073AB RID: 29611 RVA: 0x00032110 File Offset: 0x00030310
		protected virtual string GetSummaryValue()
		{
			return "";
		}

		// Token: 0x060073AC RID: 29612 RVA: 0x0004EED0 File Offset: 0x0004D0D0
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

		// Token: 0x060073AD RID: 29613 RVA: 0x0004EEFF File Offset: 0x0004D0FF
		public override void OnCommandAdded(Block parentBlock)
		{
			if (this.targetObjects.Count == 0)
			{
				this.targetObjects.Add(null);
			}
		}

		// Token: 0x060073AE RID: 29614 RVA: 0x002AB994 File Offset: 0x002A9B94
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

		// Token: 0x060073AF RID: 29615 RVA: 0x0004E668 File Offset: 0x0004C868
		public override Color GetButtonColor()
		{
			return new Color32(180, 250, 250, byte.MaxValue);
		}

		// Token: 0x060073B0 RID: 29616 RVA: 0x0004E696 File Offset: 0x0004C896
		public override bool IsReorderableArray(string propertyName)
		{
			return propertyName == "targetObjects";
		}

		// Token: 0x060073B1 RID: 29617 RVA: 0x0004EF1A File Offset: 0x0004D11A
		public override bool HasReference(Variable variable)
		{
			return this.waitUntilFinished.booleanRef == variable || this.duration.floatRef == variable || base.HasReference(variable);
		}

		// Token: 0x04006586 RID: 25990
		[Tooltip("List of objects to be affected by the tween")]
		[SerializeField]
		protected List<GameObject> targetObjects = new List<GameObject>();

		// Token: 0x04006587 RID: 25991
		[Tooltip("Type of tween easing to apply")]
		[SerializeField]
		protected LeanTweenType tweenType = LeanTweenType.easeOutQuad;

		// Token: 0x04006588 RID: 25992
		[Tooltip("Wait until this command completes before continuing execution")]
		[SerializeField]
		protected BooleanData waitUntilFinished = new BooleanData(true);

		// Token: 0x04006589 RID: 25993
		[Tooltip("Time for the tween to complete")]
		[SerializeField]
		protected FloatData duration = new FloatData(1f);
	}
}

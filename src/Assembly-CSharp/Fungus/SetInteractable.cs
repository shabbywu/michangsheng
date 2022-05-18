using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Fungus
{
	// Token: 0x02001289 RID: 4745
	[CommandInfo("UI", "Set Interactable", "Set the interactable state of selectable objects.", 0)]
	public class SetInteractable : Command
	{
		// Token: 0x0600730A RID: 29450 RVA: 0x002A9C08 File Offset: 0x002A7E08
		public override void OnEnter()
		{
			if (this.targetObjects.Count == 0)
			{
				this.Continue();
				return;
			}
			for (int i = 0; i < this.targetObjects.Count; i++)
			{
				Selectable[] components = this.targetObjects[i].GetComponents<Selectable>();
				for (int j = 0; j < components.Length; j++)
				{
					components[j].interactable = this.interactableState.Value;
				}
			}
			this.Continue();
		}

		// Token: 0x0600730B RID: 29451 RVA: 0x002A9C78 File Offset: 0x002A7E78
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
				return text + " = " + this.interactableState.Value.ToString();
			}
			if (this.targetObjects[0] == null)
			{
				return "Error: No targetObjects selected";
			}
			return this.targetObjects[0].name + " = " + this.interactableState.Value.ToString();
		}

		// Token: 0x0600730C RID: 29452 RVA: 0x0004E668 File Offset: 0x0004C868
		public override Color GetButtonColor()
		{
			return new Color32(180, 250, 250, byte.MaxValue);
		}

		// Token: 0x0600730D RID: 29453 RVA: 0x0004E688 File Offset: 0x0004C888
		public override void OnCommandAdded(Block parentBlock)
		{
			this.targetObjects.Add(null);
		}

		// Token: 0x0600730E RID: 29454 RVA: 0x0004E696 File Offset: 0x0004C896
		public override bool IsReorderableArray(string propertyName)
		{
			return propertyName == "targetObjects";
		}

		// Token: 0x0600730F RID: 29455 RVA: 0x0004E6A8 File Offset: 0x0004C8A8
		public override bool HasReference(Variable variable)
		{
			return this.interactableState.booleanRef == variable || base.HasReference(variable);
		}

		// Token: 0x04006518 RID: 25880
		[Tooltip("List of objects to be affected by the command")]
		[SerializeField]
		protected List<GameObject> targetObjects = new List<GameObject>();

		// Token: 0x04006519 RID: 25881
		[Tooltip("Controls if the selectable UI object be interactable or not")]
		[SerializeField]
		protected BooleanData interactableState = new BooleanData(true);
	}
}

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Fungus
{
	// Token: 0x02000E38 RID: 3640
	[CommandInfo("UI", "Set Interactable", "Set the interactable state of selectable objects.", 0)]
	public class SetInteractable : Command
	{
		// Token: 0x0600667C RID: 26236 RVA: 0x00286760 File Offset: 0x00284960
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

		// Token: 0x0600667D RID: 26237 RVA: 0x002867D0 File Offset: 0x002849D0
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

		// Token: 0x0600667E RID: 26238 RVA: 0x002868C5 File Offset: 0x00284AC5
		public override Color GetButtonColor()
		{
			return new Color32(180, 250, 250, byte.MaxValue);
		}

		// Token: 0x0600667F RID: 26239 RVA: 0x002868E5 File Offset: 0x00284AE5
		public override void OnCommandAdded(Block parentBlock)
		{
			this.targetObjects.Add(null);
		}

		// Token: 0x06006680 RID: 26240 RVA: 0x002868F3 File Offset: 0x00284AF3
		public override bool IsReorderableArray(string propertyName)
		{
			return propertyName == "targetObjects";
		}

		// Token: 0x06006681 RID: 26241 RVA: 0x00286905 File Offset: 0x00284B05
		public override bool HasReference(Variable variable)
		{
			return this.interactableState.booleanRef == variable || base.HasReference(variable);
		}

		// Token: 0x040057D4 RID: 22484
		[Tooltip("List of objects to be affected by the command")]
		[SerializeField]
		protected List<GameObject> targetObjects = new List<GameObject>();

		// Token: 0x040057D5 RID: 22485
		[Tooltip("Controls if the selectable UI object be interactable or not")]
		[SerializeField]
		protected BooleanData interactableState = new BooleanData(true);
	}
}

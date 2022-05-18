using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Fungus
{
	// Token: 0x02001319 RID: 4889
	[EventHandlerInfo("UI", "End Edit", "The block will execute when the user finishes editing the text in the input field.")]
	[AddComponentMenu("")]
	public class EndEdit : EventHandler
	{
		// Token: 0x0600772F RID: 30511 RVA: 0x00051366 File Offset: 0x0004F566
		protected virtual void Start()
		{
			this.targetInputField.onEndEdit.AddListener(new UnityAction<string>(this.OnEndEdit));
		}

		// Token: 0x06007730 RID: 30512 RVA: 0x00050FED File Offset: 0x0004F1ED
		protected virtual void OnEndEdit(string text)
		{
			this.ExecuteBlock();
		}

		// Token: 0x06007731 RID: 30513 RVA: 0x00051385 File Offset: 0x0004F585
		public override string GetSummary()
		{
			if (this.targetInputField != null)
			{
				return this.targetInputField.name;
			}
			return "None";
		}

		// Token: 0x040067E6 RID: 26598
		[Tooltip("The UI Input Field that the user can enter text into")]
		[SerializeField]
		protected InputField targetInputField;
	}
}

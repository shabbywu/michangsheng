using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Fungus
{
	// Token: 0x02000E9B RID: 3739
	[EventHandlerInfo("UI", "End Edit", "The block will execute when the user finishes editing the text in the input field.")]
	[AddComponentMenu("")]
	public class EndEdit : EventHandler
	{
		// Token: 0x060069FF RID: 27135 RVA: 0x00292727 File Offset: 0x00290927
		protected virtual void Start()
		{
			this.targetInputField.onEndEdit.AddListener(new UnityAction<string>(this.OnEndEdit));
		}

		// Token: 0x06006A00 RID: 27136 RVA: 0x002921CC File Offset: 0x002903CC
		protected virtual void OnEndEdit(string text)
		{
			this.ExecuteBlock();
		}

		// Token: 0x06006A01 RID: 27137 RVA: 0x00292746 File Offset: 0x00290946
		public override string GetSummary()
		{
			if (this.targetInputField != null)
			{
				return this.targetInputField.name;
			}
			return "None";
		}

		// Token: 0x040059D0 RID: 22992
		[Tooltip("The UI Input Field that the user can enter text into")]
		[SerializeField]
		protected InputField targetInputField;
	}
}

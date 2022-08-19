using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Fungus
{
	// Token: 0x02000DD5 RID: 3541
	[CommandInfo("UI", "Get Text", "Gets the text property from a UI Text object and stores it in a string variable.", 0)]
	[AddComponentMenu("")]
	public class GetText : Command
	{
		// Token: 0x06006495 RID: 25749 RVA: 0x0027F770 File Offset: 0x0027D970
		public override void OnEnter()
		{
			if (this.stringVariable == null)
			{
				this.Continue();
				return;
			}
			TextAdapter textAdapter = new TextAdapter();
			textAdapter.InitFromGameObject(this.targetTextObject, false);
			if (textAdapter.HasTextObject())
			{
				this.stringVariable.Value = textAdapter.Text;
			}
			this.Continue();
		}

		// Token: 0x06006496 RID: 25750 RVA: 0x0027F7C4 File Offset: 0x0027D9C4
		public override string GetSummary()
		{
			if (this.targetTextObject == null)
			{
				return "Error: No text object selected";
			}
			if (this.stringVariable == null)
			{
				return "Error: No variable selected";
			}
			return this.targetTextObject.name + " : " + this.stringVariable.name;
		}

		// Token: 0x06006497 RID: 25751 RVA: 0x0027D3DB File Offset: 0x0027B5DB
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x06006498 RID: 25752 RVA: 0x0027F819 File Offset: 0x0027DA19
		public override bool HasReference(Variable variable)
		{
			return this.stringVariable == variable || base.HasReference(variable);
		}

		// Token: 0x06006499 RID: 25753 RVA: 0x0027F832 File Offset: 0x0027DA32
		protected virtual void OnEnable()
		{
			if (this._textObjectObsolete != null)
			{
				this.targetTextObject = this._textObjectObsolete.gameObject;
			}
		}

		// Token: 0x0400566D RID: 22125
		[Tooltip("Text object to get text value from")]
		[SerializeField]
		protected GameObject targetTextObject;

		// Token: 0x0400566E RID: 22126
		[Tooltip("String variable to store the text value in")]
		[VariableProperty(new Type[]
		{
			typeof(StringVariable)
		})]
		[SerializeField]
		protected StringVariable stringVariable;

		// Token: 0x0400566F RID: 22127
		[HideInInspector]
		[FormerlySerializedAs("textObject")]
		public Text _textObjectObsolete;
	}
}

using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Fungus
{
	// Token: 0x02001210 RID: 4624
	[CommandInfo("UI", "Get Text", "Gets the text property from a UI Text object and stores it in a string variable.", 0)]
	[AddComponentMenu("")]
	public class GetText : Command
	{
		// Token: 0x06007117 RID: 28951 RVA: 0x002A4478 File Offset: 0x002A2678
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

		// Token: 0x06007118 RID: 28952 RVA: 0x002A44CC File Offset: 0x002A26CC
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

		// Token: 0x06007119 RID: 28953 RVA: 0x0004C5E0 File Offset: 0x0004A7E0
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x0600711A RID: 28954 RVA: 0x0004CD2B File Offset: 0x0004AF2B
		public override bool HasReference(Variable variable)
		{
			return this.stringVariable == variable || base.HasReference(variable);
		}

		// Token: 0x0600711B RID: 28955 RVA: 0x0004CD44 File Offset: 0x0004AF44
		protected virtual void OnEnable()
		{
			if (this._textObjectObsolete != null)
			{
				this.targetTextObject = this._textObjectObsolete.gameObject;
			}
		}

		// Token: 0x0400636D RID: 25453
		[Tooltip("Text object to get text value from")]
		[SerializeField]
		protected GameObject targetTextObject;

		// Token: 0x0400636E RID: 25454
		[Tooltip("String variable to store the text value in")]
		[VariableProperty(new Type[]
		{
			typeof(StringVariable)
		})]
		[SerializeField]
		protected StringVariable stringVariable;

		// Token: 0x0400636F RID: 25455
		[HideInInspector]
		[FormerlySerializedAs("textObject")]
		public Text _textObjectObsolete;
	}
}

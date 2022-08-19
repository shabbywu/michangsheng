using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Fungus
{
	// Token: 0x02000E42 RID: 3650
	[CommandInfo("UI", "Set Text", "Sets the text property on a UI Text object and/or an Input Field object.", 0)]
	[AddComponentMenu("")]
	public class SetText : Command, ILocalizable
	{
		// Token: 0x060066B4 RID: 26292 RVA: 0x00286E94 File Offset: 0x00285094
		public override void OnEnter()
		{
			string text = this.GetFlowchart().SubstituteVariables(this.text.Value);
			if (this.targetTextObject == null)
			{
				this.Continue();
				return;
			}
			TextAdapter textAdapter = new TextAdapter();
			textAdapter.InitFromGameObject(this.targetTextObject, false);
			if (textAdapter.HasTextObject())
			{
				textAdapter.Text = text;
			}
			this.Continue();
		}

		// Token: 0x060066B5 RID: 26293 RVA: 0x00286EF5 File Offset: 0x002850F5
		public override string GetSummary()
		{
			if (this.targetTextObject != null)
			{
				return this.targetTextObject.name + " : " + this.text.Value;
			}
			return "Error: No text object selected";
		}

		// Token: 0x060066B6 RID: 26294 RVA: 0x0027D3DB File Offset: 0x0027B5DB
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x060066B7 RID: 26295 RVA: 0x00286F2B File Offset: 0x0028512B
		public override bool HasReference(Variable variable)
		{
			return this.text.stringRef == variable || base.HasReference(variable);
		}

		// Token: 0x060066B8 RID: 26296 RVA: 0x00286F49 File Offset: 0x00285149
		public virtual string GetStandardText()
		{
			return this.text;
		}

		// Token: 0x060066B9 RID: 26297 RVA: 0x00286F56 File Offset: 0x00285156
		public virtual void SetStandardText(string standardText)
		{
			this.text.Value = standardText;
		}

		// Token: 0x060066BA RID: 26298 RVA: 0x00286F64 File Offset: 0x00285164
		public virtual string GetDescription()
		{
			return this.description;
		}

		// Token: 0x060066BB RID: 26299 RVA: 0x00286F6C File Offset: 0x0028516C
		public virtual string GetStringId()
		{
			return string.Concat(new object[]
			{
				"SETTEXT.",
				this.GetFlowchartLocalizationId(),
				".",
				this.itemId
			});
		}

		// Token: 0x060066BC RID: 26300 RVA: 0x00286FA0 File Offset: 0x002851A0
		protected virtual void OnEnable()
		{
			if (this._textObjectObsolete != null)
			{
				this.targetTextObject = this._textObjectObsolete.gameObject;
			}
		}

		// Token: 0x040057E9 RID: 22505
		[Tooltip("Text object to set text on. Can be a UI Text, Text Field or Text Mesh object.")]
		[SerializeField]
		protected GameObject targetTextObject;

		// Token: 0x040057EA RID: 22506
		[Tooltip("String value to assign to the text object")]
		[FormerlySerializedAs("stringData")]
		[SerializeField]
		protected StringDataMulti text;

		// Token: 0x040057EB RID: 22507
		[Tooltip("Notes about this story text for other authors, localization, etc.")]
		[SerializeField]
		protected string description;

		// Token: 0x040057EC RID: 22508
		[HideInInspector]
		[FormerlySerializedAs("textObject")]
		public Text _textObjectObsolete;
	}
}

using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Fungus
{
	// Token: 0x02001293 RID: 4755
	[CommandInfo("UI", "Set Text", "Sets the text property on a UI Text object and/or an Input Field object.", 0)]
	[AddComponentMenu("")]
	public class SetText : Command, ILocalizable
	{
		// Token: 0x06007342 RID: 29506 RVA: 0x002A9FC8 File Offset: 0x002A81C8
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

		// Token: 0x06007343 RID: 29507 RVA: 0x0004E9D9 File Offset: 0x0004CBD9
		public override string GetSummary()
		{
			if (this.targetTextObject != null)
			{
				return this.targetTextObject.name + " : " + this.text.Value;
			}
			return "Error: No text object selected";
		}

		// Token: 0x06007344 RID: 29508 RVA: 0x0004C5E0 File Offset: 0x0004A7E0
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x06007345 RID: 29509 RVA: 0x0004EA0F File Offset: 0x0004CC0F
		public override bool HasReference(Variable variable)
		{
			return this.text.stringRef == variable || base.HasReference(variable);
		}

		// Token: 0x06007346 RID: 29510 RVA: 0x0004EA2D File Offset: 0x0004CC2D
		public virtual string GetStandardText()
		{
			return this.text;
		}

		// Token: 0x06007347 RID: 29511 RVA: 0x0004EA3A File Offset: 0x0004CC3A
		public virtual void SetStandardText(string standardText)
		{
			this.text.Value = standardText;
		}

		// Token: 0x06007348 RID: 29512 RVA: 0x0004EA48 File Offset: 0x0004CC48
		public virtual string GetDescription()
		{
			return this.description;
		}

		// Token: 0x06007349 RID: 29513 RVA: 0x0004EA50 File Offset: 0x0004CC50
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

		// Token: 0x0600734A RID: 29514 RVA: 0x0004EA84 File Offset: 0x0004CC84
		protected virtual void OnEnable()
		{
			if (this._textObjectObsolete != null)
			{
				this.targetTextObject = this._textObjectObsolete.gameObject;
			}
		}

		// Token: 0x0400652D RID: 25901
		[Tooltip("Text object to set text on. Can be a UI Text, Text Field or Text Mesh object.")]
		[SerializeField]
		protected GameObject targetTextObject;

		// Token: 0x0400652E RID: 25902
		[Tooltip("String value to assign to the text object")]
		[FormerlySerializedAs("stringData")]
		[SerializeField]
		protected StringDataMulti text;

		// Token: 0x0400652F RID: 25903
		[Tooltip("Notes about this story text for other authors, localization, etc.")]
		[SerializeField]
		protected string description;

		// Token: 0x04006530 RID: 25904
		[HideInInspector]
		[FormerlySerializedAs("textObject")]
		public Text _textObjectObsolete;
	}
}

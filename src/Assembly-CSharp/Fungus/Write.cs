using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020012B7 RID: 4791
	[CommandInfo("UI", "Write", "Writes content to a UI Text or Text Mesh object.", 0)]
	[AddComponentMenu("")]
	public class Write : Command, ILocalizable
	{
		// Token: 0x060073E6 RID: 29670 RVA: 0x002ACA08 File Offset: 0x002AAC08
		protected Writer GetWriter()
		{
			Writer writer = this.textObject.GetComponent<Writer>();
			if (writer == null)
			{
				writer = this.textObject.AddComponent<Writer>();
			}
			return writer;
		}

		// Token: 0x060073E7 RID: 29671 RVA: 0x002ACA38 File Offset: 0x002AAC38
		public override void OnEnter()
		{
			if (this.textObject == null)
			{
				this.Continue();
				return;
			}
			Writer writer = this.GetWriter();
			if (writer == null)
			{
				this.Continue();
				return;
			}
			switch (this.textColor)
			{
			case TextColor.SetVisible:
				writer.SetTextAlpha(1f);
				break;
			case TextColor.SetAlpha:
				writer.SetTextAlpha(this.setAlpha);
				break;
			case TextColor.SetColor:
				writer.SetTextColor(this.setColor);
				break;
			}
			string content = this.GetFlowchart().SubstituteVariables(this.text.Value);
			if (!this.waitUntilFinished)
			{
				base.StartCoroutine(writer.Write(content, this.clearText, false, true, false, null, null));
				this.Continue();
				return;
			}
			base.StartCoroutine(writer.Write(content, this.clearText, false, true, false, null, delegate
			{
				this.Continue();
			}));
		}

		// Token: 0x060073E8 RID: 29672 RVA: 0x0004F17D File Offset: 0x0004D37D
		public override string GetSummary()
		{
			if (this.textObject != null)
			{
				return this.textObject.name + " : " + this.text.Value;
			}
			return "Error: No text object selected";
		}

		// Token: 0x060073E9 RID: 29673 RVA: 0x0004C5E0 File Offset: 0x0004A7E0
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x060073EA RID: 29674 RVA: 0x0004F1B3 File Offset: 0x0004D3B3
		public override void OnStopExecuting()
		{
			this.GetWriter().Stop();
		}

		// Token: 0x060073EB RID: 29675 RVA: 0x0004F1C0 File Offset: 0x0004D3C0
		public virtual string GetStandardText()
		{
			return this.text;
		}

		// Token: 0x060073EC RID: 29676 RVA: 0x0004F1CD File Offset: 0x0004D3CD
		public virtual void SetStandardText(string standardText)
		{
			this.text.Value = standardText;
		}

		// Token: 0x060073ED RID: 29677 RVA: 0x0004F1DB File Offset: 0x0004D3DB
		public virtual string GetDescription()
		{
			return this.description;
		}

		// Token: 0x060073EE RID: 29678 RVA: 0x0004F1E3 File Offset: 0x0004D3E3
		public virtual string GetStringId()
		{
			return string.Concat(new object[]
			{
				"WRITE.",
				this.GetFlowchartLocalizationId(),
				".",
				this.itemId
			});
		}

		// Token: 0x060073EF RID: 29679 RVA: 0x002ACB24 File Offset: 0x002AAD24
		public override bool HasReference(Variable variable)
		{
			return this.text.stringRef == variable || this.setAlpha.floatRef == variable || this.setColor.colorRef == variable || base.HasReference(variable);
		}

		// Token: 0x040065BE RID: 26046
		[Tooltip("Text object to set text on. Text, Input Field and Text Mesh objects are supported.")]
		[SerializeField]
		protected GameObject textObject;

		// Token: 0x040065BF RID: 26047
		[Tooltip("String value to assign to the text object")]
		[SerializeField]
		protected StringDataMulti text;

		// Token: 0x040065C0 RID: 26048
		[Tooltip("Notes about this story text for other authors, localization, etc.")]
		[SerializeField]
		protected string description;

		// Token: 0x040065C1 RID: 26049
		[Tooltip("Clear existing text before writing new text")]
		[SerializeField]
		protected bool clearText = true;

		// Token: 0x040065C2 RID: 26050
		[Tooltip("Wait until this command finishes before executing the next command")]
		[SerializeField]
		protected bool waitUntilFinished = true;

		// Token: 0x040065C3 RID: 26051
		[Tooltip("Color mode to apply to the text.")]
		[SerializeField]
		protected TextColor textColor;

		// Token: 0x040065C4 RID: 26052
		[Tooltip("Alpha to apply to the text.")]
		[SerializeField]
		protected FloatData setAlpha = new FloatData(1f);

		// Token: 0x040065C5 RID: 26053
		[Tooltip("Color to apply to the text.")]
		[SerializeField]
		protected ColorData setColor = new ColorData(Color.white);
	}
}

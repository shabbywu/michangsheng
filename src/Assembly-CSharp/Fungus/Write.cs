using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E61 RID: 3681
	[CommandInfo("UI", "Write", "Writes content to a UI Text or Text Mesh object.", 0)]
	[AddComponentMenu("")]
	public class Write : Command, ILocalizable
	{
		// Token: 0x06006752 RID: 26450 RVA: 0x00289FE8 File Offset: 0x002881E8
		protected Writer GetWriter()
		{
			Writer writer = this.textObject.GetComponent<Writer>();
			if (writer == null)
			{
				writer = this.textObject.AddComponent<Writer>();
			}
			return writer;
		}

		// Token: 0x06006753 RID: 26451 RVA: 0x0028A018 File Offset: 0x00288218
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

		// Token: 0x06006754 RID: 26452 RVA: 0x0028A102 File Offset: 0x00288302
		public override string GetSummary()
		{
			if (this.textObject != null)
			{
				return this.textObject.name + " : " + this.text.Value;
			}
			return "Error: No text object selected";
		}

		// Token: 0x06006755 RID: 26453 RVA: 0x0027D3DB File Offset: 0x0027B5DB
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x06006756 RID: 26454 RVA: 0x0028A138 File Offset: 0x00288338
		public override void OnStopExecuting()
		{
			this.GetWriter().Stop();
		}

		// Token: 0x06006757 RID: 26455 RVA: 0x0028A145 File Offset: 0x00288345
		public virtual string GetStandardText()
		{
			return this.text;
		}

		// Token: 0x06006758 RID: 26456 RVA: 0x0028A152 File Offset: 0x00288352
		public virtual void SetStandardText(string standardText)
		{
			this.text.Value = standardText;
		}

		// Token: 0x06006759 RID: 26457 RVA: 0x0028A160 File Offset: 0x00288360
		public virtual string GetDescription()
		{
			return this.description;
		}

		// Token: 0x0600675A RID: 26458 RVA: 0x0028A168 File Offset: 0x00288368
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

		// Token: 0x0600675B RID: 26459 RVA: 0x0028A19C File Offset: 0x0028839C
		public override bool HasReference(Variable variable)
		{
			return this.text.stringRef == variable || this.setAlpha.floatRef == variable || this.setColor.colorRef == variable || base.HasReference(variable);
		}

		// Token: 0x0400585B RID: 22619
		[Tooltip("Text object to set text on. Text, Input Field and Text Mesh objects are supported.")]
		[SerializeField]
		protected GameObject textObject;

		// Token: 0x0400585C RID: 22620
		[Tooltip("String value to assign to the text object")]
		[SerializeField]
		protected StringDataMulti text;

		// Token: 0x0400585D RID: 22621
		[Tooltip("Notes about this story text for other authors, localization, etc.")]
		[SerializeField]
		protected string description;

		// Token: 0x0400585E RID: 22622
		[Tooltip("Clear existing text before writing new text")]
		[SerializeField]
		protected bool clearText = true;

		// Token: 0x0400585F RID: 22623
		[Tooltip("Wait until this command finishes before executing the next command")]
		[SerializeField]
		protected bool waitUntilFinished = true;

		// Token: 0x04005860 RID: 22624
		[Tooltip("Color mode to apply to the text.")]
		[SerializeField]
		protected TextColor textColor;

		// Token: 0x04005861 RID: 22625
		[Tooltip("Alpha to apply to the text.")]
		[SerializeField]
		protected FloatData setAlpha = new FloatData(1f);

		// Token: 0x04005862 RID: 22626
		[Tooltip("Color to apply to the text.")]
		[SerializeField]
		protected ColorData setColor = new ColorData(Color.white);
	}
}

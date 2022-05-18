using System;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Fungus
{
	// Token: 0x0200136C RID: 4972
	public class TextAdapter : IWriterTextDestination
	{
		// Token: 0x0600789A RID: 30874 RVA: 0x002B6EF0 File Offset: 0x002B50F0
		public void InitFromGameObject(GameObject go, bool includeChildren = false)
		{
			if (go == null)
			{
				return;
			}
			if (!includeChildren)
			{
				this.textUI = go.GetComponent<Text>();
				this.inputField = go.GetComponent<InputField>();
				this.textMesh = go.GetComponent<TextMesh>();
				this.tmpro = go.GetComponent<TMP_Text>();
				this.writerTextDestination = go.GetComponent<IWriterTextDestination>();
			}
			else
			{
				this.textUI = go.GetComponentInChildren<Text>();
				this.inputField = go.GetComponentInChildren<InputField>();
				this.textMesh = go.GetComponentInChildren<TextMesh>();
				this.tmpro = go.GetComponentInChildren<TMP_Text>();
				this.writerTextDestination = go.GetComponentInChildren<IWriterTextDestination>();
			}
			if (this.textUI == null && this.inputField == null && this.textMesh == null && this.writerTextDestination == null)
			{
				Component[] array;
				if (!includeChildren)
				{
					array = go.GetComponents<Component>();
				}
				else
				{
					array = go.GetComponentsInChildren<Component>();
				}
				foreach (Component component in array)
				{
					this.textProperty = component.GetType().GetProperty("text");
					if (this.textProperty != null)
					{
						this.textComponent = component;
						return;
					}
				}
			}
		}

		// Token: 0x0600789B RID: 30875 RVA: 0x002B700C File Offset: 0x002B520C
		public void ForceRichText()
		{
			if (this.textUI != null)
			{
				this.textUI.supportRichText = true;
			}
			if (this.textMesh != null)
			{
				this.textMesh.richText = true;
			}
			if (this.tmpro != null)
			{
				this.tmpro.richText = true;
			}
			if (this.writerTextDestination != null)
			{
				this.writerTextDestination.ForceRichText();
			}
		}

		// Token: 0x0600789C RID: 30876 RVA: 0x002B707C File Offset: 0x002B527C
		public void SetTextColor(Color textColor)
		{
			if (this.textUI != null)
			{
				this.textUI.color = textColor;
				return;
			}
			if (this.inputField != null)
			{
				if (this.inputField.textComponent != null)
				{
					this.inputField.textComponent.color = textColor;
					return;
				}
			}
			else
			{
				if (this.textMesh != null)
				{
					this.textMesh.color = textColor;
					return;
				}
				if (this.tmpro != null)
				{
					this.tmpro.color = textColor;
					return;
				}
				if (this.writerTextDestination != null)
				{
					this.writerTextDestination.SetTextColor(textColor);
				}
			}
		}

		// Token: 0x0600789D RID: 30877 RVA: 0x002B7124 File Offset: 0x002B5324
		public void SetTextAlpha(float textAlpha)
		{
			if (this.textUI != null)
			{
				Color color = this.textUI.color;
				color.a = textAlpha;
				this.textUI.color = color;
				return;
			}
			if (this.inputField != null)
			{
				if (this.inputField.textComponent != null)
				{
					Color color2 = this.inputField.textComponent.color;
					color2.a = textAlpha;
					this.inputField.textComponent.color = color2;
					return;
				}
			}
			else
			{
				if (this.textMesh != null)
				{
					Color color3 = this.textMesh.color;
					color3.a = textAlpha;
					this.textMesh.color = color3;
					return;
				}
				if (this.tmpro != null)
				{
					this.tmpro.alpha = textAlpha;
					return;
				}
				if (this.writerTextDestination != null)
				{
					this.writerTextDestination.SetTextAlpha(textAlpha);
				}
			}
		}

		// Token: 0x0600789E RID: 30878 RVA: 0x00051EB0 File Offset: 0x000500B0
		public void SetTextSize(int size)
		{
			if (this.textUI != null)
			{
				this.textUI.fontSize = size;
			}
		}

		// Token: 0x0600789F RID: 30879 RVA: 0x002B7210 File Offset: 0x002B5410
		public bool HasTextObject()
		{
			return this.textUI != null || this.inputField != null || this.textMesh != null || this.textComponent != null || this.tmpro != null || this.writerTextDestination != null;
		}

		// Token: 0x060078A0 RID: 30880 RVA: 0x002B7270 File Offset: 0x002B5470
		public bool SupportsRichText()
		{
			if (this.textUI != null)
			{
				return this.textUI.supportRichText;
			}
			if (this.inputField != null)
			{
				return false;
			}
			if (this.textMesh != null)
			{
				return this.textMesh.richText;
			}
			return this.tmpro != null || (this.writerTextDestination != null && this.writerTextDestination.SupportsRichText());
		}

		// Token: 0x17000B54 RID: 2900
		// (get) Token: 0x060078A1 RID: 30881 RVA: 0x002B72E8 File Offset: 0x002B54E8
		// (set) Token: 0x060078A2 RID: 30882 RVA: 0x002B7398 File Offset: 0x002B5598
		public virtual string Text
		{
			get
			{
				if (this.textUI != null)
				{
					return this.textUI.text;
				}
				if (this.inputField != null)
				{
					return this.inputField.text;
				}
				if (this.writerTextDestination != null)
				{
					return this.Text;
				}
				if (this.textMesh != null)
				{
					return this.textMesh.text;
				}
				if (this.tmpro != null)
				{
					return this.tmpro.text;
				}
				if (this.textProperty != null)
				{
					return this.textProperty.GetValue(this.textComponent, null) as string;
				}
				return "";
			}
			set
			{
				if (this.textUI != null)
				{
					this.textUI.text = value;
					return;
				}
				if (this.inputField != null)
				{
					this.inputField.text = value;
					return;
				}
				if (this.writerTextDestination != null)
				{
					this.Text = value;
					return;
				}
				if (this.textMesh != null)
				{
					this.textMesh.text = value;
					return;
				}
				if (this.tmpro != null)
				{
					this.tmpro.text = value;
					return;
				}
				if (this.textProperty != null)
				{
					this.textProperty.SetValue(this.textComponent, value, null);
				}
			}
		}

		// Token: 0x0400689B RID: 26779
		protected Text textUI;

		// Token: 0x0400689C RID: 26780
		protected InputField inputField;

		// Token: 0x0400689D RID: 26781
		protected TextMesh textMesh;

		// Token: 0x0400689E RID: 26782
		protected TMP_Text tmpro;

		// Token: 0x0400689F RID: 26783
		protected Component textComponent;

		// Token: 0x040068A0 RID: 26784
		protected PropertyInfo textProperty;

		// Token: 0x040068A1 RID: 26785
		protected IWriterTextDestination writerTextDestination;
	}
}

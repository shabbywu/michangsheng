using System;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Fungus
{
	// Token: 0x02000ECD RID: 3789
	public class TextAdapter : IWriterTextDestination
	{
		// Token: 0x06006AFD RID: 27389 RVA: 0x00294C24 File Offset: 0x00292E24
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

		// Token: 0x06006AFE RID: 27390 RVA: 0x00294D40 File Offset: 0x00292F40
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

		// Token: 0x06006AFF RID: 27391 RVA: 0x00294DB0 File Offset: 0x00292FB0
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

		// Token: 0x06006B00 RID: 27392 RVA: 0x00294E58 File Offset: 0x00293058
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

		// Token: 0x06006B01 RID: 27393 RVA: 0x00294F41 File Offset: 0x00293141
		public void SetTextSize(int size)
		{
			if (this.textUI != null)
			{
				this.textUI.fontSize = size;
			}
		}

		// Token: 0x06006B02 RID: 27394 RVA: 0x00294F60 File Offset: 0x00293160
		public bool HasTextObject()
		{
			return this.textUI != null || this.inputField != null || this.textMesh != null || this.textComponent != null || this.tmpro != null || this.writerTextDestination != null;
		}

		// Token: 0x06006B03 RID: 27395 RVA: 0x00294FC0 File Offset: 0x002931C0
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

		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x06006B04 RID: 27396 RVA: 0x00295038 File Offset: 0x00293238
		// (set) Token: 0x06006B05 RID: 27397 RVA: 0x002950E8 File Offset: 0x002932E8
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

		// Token: 0x04005A3A RID: 23098
		protected Text textUI;

		// Token: 0x04005A3B RID: 23099
		protected InputField inputField;

		// Token: 0x04005A3C RID: 23100
		protected TextMesh textMesh;

		// Token: 0x04005A3D RID: 23101
		protected TMP_Text tmpro;

		// Token: 0x04005A3E RID: 23102
		protected Component textComponent;

		// Token: 0x04005A3F RID: 23103
		protected PropertyInfo textProperty;

		// Token: 0x04005A40 RID: 23104
		protected IWriterTextDestination writerTextDestination;
	}
}

using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000161 RID: 353
public abstract class ModalBox : MonoBehaviour
{
	// Token: 0x06000F5E RID: 3934 RVA: 0x0005C7C0 File Offset: 0x0005A9C0
	private int stringHeight(string str)
	{
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < str.Length; i++)
		{
			num2++;
			if (str[i] == '\n' || num2 == 20)
			{
				num2 = 0;
				num++;
			}
		}
		num++;
		return num * 17 + 15;
	}

	// Token: 0x06000F5F RID: 3935 RVA: 0x0005C808 File Offset: 0x0005AA08
	private void FixedUpdate()
	{
		if (!this.isSetup)
		{
			this.isSetup = true;
			if (this.Title != null)
			{
				this.Title.GetComponent<LayoutElement>().minHeight = (float)this.stringHeight(this.Title.text);
			}
			if (this.Message != null)
			{
				this.Message.GetComponent<LayoutElement>().minHeight = this.Message.preferredHeight;
				if (this.Message.preferredHeight < 20f)
				{
					this.Message.alignment = 1;
				}
			}
			if (this.buttonParent != null)
			{
				foreach (object obj in this.buttonParent)
				{
					Transform transform = (Transform)obj;
					Text componentInChildren = transform.GetComponentInChildren<Text>();
					if (componentInChildren != null)
					{
						LayoutElement component = transform.GetComponent<LayoutElement>();
						if (component != null)
						{
							component.minHeight = (float)this.stringHeight(componentInChildren.text);
						}
					}
				}
			}
		}
	}

	// Token: 0x06000F60 RID: 3936 RVA: 0x0005C928 File Offset: 0x0005AB28
	public virtual void Close()
	{
		Object.Destroy(base.gameObject);
	}

	// Token: 0x06000F61 RID: 3937 RVA: 0x0005C938 File Offset: 0x0005AB38
	protected void SetText(string message, string title)
	{
		if (this.Button != null)
		{
			this.buttonParent = this.Button.transform.parent;
		}
		if (this.Title != null)
		{
			if (!string.IsNullOrEmpty(title))
			{
				this.Title.text = title;
			}
			else
			{
				Object.Destroy(this.Title.gameObject);
				this.Title = null;
			}
		}
		if (this.Message != null)
		{
			if (!string.IsNullOrEmpty(message))
			{
				this.Message.text = (MessageBox.LocalizeTitleAndMessage ? MessageBox.Localize(message) : message);
				return;
			}
			Object.Destroy(this.Message.gameObject);
			this.Message = null;
		}
	}

	// Token: 0x04000B77 RID: 2935
	[Tooltip("The title text object that will be used to show the title of the dialog.")]
	public Text Title;

	// Token: 0x04000B78 RID: 2936
	[Tooltip("The message text object that will be used to show the message of the dialog.")]
	public Text Message;

	// Token: 0x04000B79 RID: 2937
	[Tooltip("The botton object that will be used to interact with the dialog. This will be cloned to produce additional options.")]
	public Button Button;

	// Token: 0x04000B7A RID: 2938
	[Tooltip("The RectTransform of the panel that contains the frame of the dialog window. This is needed so that it can be centered correctly after it's size is adjusted to the dialogs contents.")]
	public RectTransform Panel;

	// Token: 0x04000B7B RID: 2939
	private Transform buttonParent;

	// Token: 0x04000B7C RID: 2940
	private bool isSetup;
}

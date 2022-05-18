using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200023C RID: 572
public abstract class ModalBox : MonoBehaviour
{
	// Token: 0x060011B8 RID: 4536 RVA: 0x000AC474 File Offset: 0x000AA674
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

	// Token: 0x060011B9 RID: 4537 RVA: 0x000AC4BC File Offset: 0x000AA6BC
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

	// Token: 0x060011BA RID: 4538 RVA: 0x000111B3 File Offset: 0x0000F3B3
	public virtual void Close()
	{
		Object.Destroy(base.gameObject);
	}

	// Token: 0x060011BB RID: 4539 RVA: 0x000AC5DC File Offset: 0x000AA7DC
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

	// Token: 0x04000E44 RID: 3652
	[Tooltip("The title text object that will be used to show the title of the dialog.")]
	public Text Title;

	// Token: 0x04000E45 RID: 3653
	[Tooltip("The message text object that will be used to show the message of the dialog.")]
	public Text Message;

	// Token: 0x04000E46 RID: 3654
	[Tooltip("The botton object that will be used to interact with the dialog. This will be cloned to produce additional options.")]
	public Button Button;

	// Token: 0x04000E47 RID: 3655
	[Tooltip("The RectTransform of the panel that contains the frame of the dialog window. This is needed so that it can be centered correctly after it's size is adjusted to the dialogs contents.")]
	public RectTransform Panel;

	// Token: 0x04000E48 RID: 3656
	private Transform buttonParent;

	// Token: 0x04000E49 RID: 3657
	private bool isSetup;
}

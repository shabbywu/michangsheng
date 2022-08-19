using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200015E RID: 350
public class MessageBox : ModalBox
{
	// Token: 0x06000F3C RID: 3900 RVA: 0x0005BD13 File Offset: 0x00059F13
	public static MessageBox Show(string message, Action<DialogResult> onFinished, MessageBoxButtons buttons = MessageBoxButtons.OK)
	{
		return MessageBox.Show(message, null, onFinished, buttons);
	}

	// Token: 0x06000F3D RID: 3901 RVA: 0x0005BD1E File Offset: 0x00059F1E
	public static MessageBox Show(string message, string title = null, Action<DialogResult> onFinished = null, MessageBoxButtons buttons = MessageBoxButtons.OK)
	{
		MessageBox component = Object.Instantiate<GameObject>(Resources.Load<GameObject>(MessageBox.PrefabResourceName)).GetComponent<MessageBox>();
		component.onFinish = onFinished;
		component.SetUpButtons(buttons);
		component.SetText(message, title);
		return component;
	}

	// Token: 0x06000F3E RID: 3902 RVA: 0x0005BD4C File Offset: 0x00059F4C
	private void SetUpButtons(MessageBoxButtons buttons)
	{
		GameObject gameObject = this.Button.gameObject;
		switch (buttons)
		{
		case MessageBoxButtons.OK:
			gameObject.GetComponentInChildren<Text>().text = MessageBox.Localize("OK");
			gameObject.GetComponent<Button>().onClick.AddListener(delegate()
			{
				this.result = DialogResult.OK;
				this.Close();
			});
			return;
		case MessageBoxButtons.OKCancel:
			gameObject.GetComponentInChildren<Text>().text = MessageBox.Localize("OK");
			gameObject.GetComponent<Button>().onClick.AddListener(delegate()
			{
				this.result = DialogResult.OK;
				this.Close();
			});
			this.CreateButton(gameObject, MessageBox.Localize("Cancel"), delegate
			{
				this.result = DialogResult.Cancel;
				this.Close();
			});
			return;
		case MessageBoxButtons.YesNo:
			gameObject.GetComponentInChildren<Text>().text = MessageBox.Localize("Yes");
			gameObject.GetComponent<Button>().onClick.AddListener(delegate()
			{
				this.result = DialogResult.Yes;
				this.Close();
			});
			this.CreateButton(gameObject, MessageBox.Localize("No"), delegate
			{
				this.result = DialogResult.No;
				this.Close();
			});
			return;
		case MessageBoxButtons.YesNoCancel:
			gameObject.GetComponentInChildren<Text>().text = MessageBox.Localize("Yes");
			gameObject.GetComponent<Button>().onClick.AddListener(delegate()
			{
				this.result = DialogResult.Yes;
				this.Close();
			});
			this.CreateButton(gameObject, MessageBox.Localize("No"), delegate
			{
				this.result = DialogResult.No;
				this.Close();
			});
			this.CreateButton(gameObject, MessageBox.Localize("Cancel"), delegate
			{
				this.result = DialogResult.Cancel;
				this.Close();
			});
			return;
		case MessageBoxButtons.RetryCancel:
			gameObject.GetComponentInChildren<Text>().text = MessageBox.Localize("Retry");
			gameObject.GetComponent<Button>().onClick.AddListener(delegate()
			{
				this.result = DialogResult.Retry;
				this.Close();
			});
			this.CreateButton(gameObject, MessageBox.Localize("Cancel"), delegate
			{
				this.result = DialogResult.Cancel;
				this.Close();
			});
			return;
		case MessageBoxButtons.AbortRetryIgnore:
			gameObject.GetComponentInChildren<Text>().text = MessageBox.Localize("Abort");
			gameObject.GetComponent<Button>().onClick.AddListener(delegate()
			{
				this.result = DialogResult.Abort;
				this.Close();
			});
			this.CreateButton(gameObject, MessageBox.Localize("Retry"), delegate
			{
				this.result = DialogResult.Retry;
				this.Close();
			});
			this.CreateButton(gameObject, MessageBox.Localize("Ignore"), delegate
			{
				this.result = DialogResult.Ignore;
				this.Close();
			});
			return;
		default:
			return;
		}
	}

	// Token: 0x06000F3F RID: 3903 RVA: 0x0005BA8E File Offset: 0x00059C8E
	private GameObject CreateButton(GameObject buttonToClone, string label, UnityAction target)
	{
		GameObject gameObject = Object.Instantiate<GameObject>(buttonToClone);
		gameObject.transform.SetParent(buttonToClone.transform.parent, false);
		gameObject.GetComponentInChildren<Text>().text = label;
		gameObject.GetComponent<Button>().onClick.AddListener(target);
		return gameObject;
	}

	// Token: 0x06000F40 RID: 3904 RVA: 0x0005BFC2 File Offset: 0x0005A1C2
	public override void Close()
	{
		if (this.onFinish != null)
		{
			this.onFinish(this.result);
		}
		base.Close();
	}

	// Token: 0x04000B6B RID: 2923
	[Tooltip("Set this to the name of the prefab that should be loaded when a menu box is shown.")]
	public static string PrefabResourceName = "Message Box";

	// Token: 0x04000B6C RID: 2924
	[Tooltip("Set this to a custom function that will be used to localize the button texts.")]
	public static Func<string, string> Localize = (string sourceString) => sourceString;

	// Token: 0x04000B6D RID: 2925
	[Tooltip("Set to true to send the title and message of message boxes and menus thru the Localize function.")]
	public static bool LocalizeTitleAndMessage = false;

	// Token: 0x04000B6E RID: 2926
	private DialogResult result;

	// Token: 0x04000B6F RID: 2927
	private Action<DialogResult> onFinish;
}

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000236 RID: 566
public class MessageBox : ModalBox
{
	// Token: 0x0600116B RID: 4459 RVA: 0x00010E31 File Offset: 0x0000F031
	public static MessageBox Show(string message, Action<DialogResult> onFinished, MessageBoxButtons buttons = MessageBoxButtons.OK)
	{
		return MessageBox.Show(message, null, onFinished, buttons);
	}

	// Token: 0x0600116C RID: 4460 RVA: 0x00010E3C File Offset: 0x0000F03C
	public static MessageBox Show(string message, string title = null, Action<DialogResult> onFinished = null, MessageBoxButtons buttons = MessageBoxButtons.OK)
	{
		MessageBox component = Object.Instantiate<GameObject>(Resources.Load<GameObject>(MessageBox.PrefabResourceName)).GetComponent<MessageBox>();
		component.onFinish = onFinished;
		component.SetUpButtons(buttons);
		component.SetText(message, title);
		return component;
	}

	// Token: 0x0600116D RID: 4461 RVA: 0x000ABC00 File Offset: 0x000A9E00
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

	// Token: 0x0600116E RID: 4462 RVA: 0x00010CC8 File Offset: 0x0000EEC8
	private GameObject CreateButton(GameObject buttonToClone, string label, UnityAction target)
	{
		GameObject gameObject = Object.Instantiate<GameObject>(buttonToClone);
		gameObject.transform.SetParent(buttonToClone.transform.parent, false);
		gameObject.GetComponentInChildren<Text>().text = label;
		gameObject.GetComponent<Button>().onClick.AddListener(target);
		return gameObject;
	}

	// Token: 0x0600116F RID: 4463 RVA: 0x00010E68 File Offset: 0x0000F068
	public override void Close()
	{
		if (this.onFinish != null)
		{
			this.onFinish(this.result);
		}
		base.Close();
	}

	// Token: 0x04000E11 RID: 3601
	[Tooltip("Set this to the name of the prefab that should be loaded when a menu box is shown.")]
	public static string PrefabResourceName = "Message Box";

	// Token: 0x04000E12 RID: 3602
	[Tooltip("Set this to a custom function that will be used to localize the button texts.")]
	public static Func<string, string> Localize = (string sourceString) => sourceString;

	// Token: 0x04000E13 RID: 3603
	[Tooltip("Set to true to send the title and message of message boxes and menus thru the Localize function.")]
	public static bool LocalizeTitleAndMessage = false;

	// Token: 0x04000E14 RID: 3604
	private DialogResult result;

	// Token: 0x04000E15 RID: 3605
	private Action<DialogResult> onFinish;
}

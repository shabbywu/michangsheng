using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000233 RID: 563
public class MapMessageBox : ModalBox
{
	// Token: 0x0600114C RID: 4428 RVA: 0x00010C91 File Offset: 0x0000EE91
	public static MapMessageBox Show(string message, Action<DialogResult> onFinished, MessageBoxButtons buttons = MessageBoxButtons.OK)
	{
		return MapMessageBox.Show(message, null, onFinished, buttons);
	}

	// Token: 0x0600114D RID: 4429 RVA: 0x00010C9C File Offset: 0x0000EE9C
	public static MapMessageBox Show(string message, string title = null, Action<DialogResult> onFinished = null, MessageBoxButtons buttons = MessageBoxButtons.OK)
	{
		MapMessageBox component = Object.Instantiate<GameObject>(Resources.Load<GameObject>(MapMessageBox.PrefabResourceName)).GetComponent<MapMessageBox>();
		component.onFinish = onFinished;
		component.SetUpButtons(buttons);
		component.SetText(message, title);
		return component;
	}

	// Token: 0x0600114E RID: 4430 RVA: 0x000AB85C File Offset: 0x000A9A5C
	private void SetUpButtons(MessageBoxButtons buttons)
	{
		GameObject gameObject = this.Button.gameObject;
		switch (buttons)
		{
		case MessageBoxButtons.OK:
			gameObject.GetComponentInChildren<Text>().text = MapMessageBox.Localize("OK");
			gameObject.GetComponent<Button>().onClick.AddListener(delegate()
			{
				this.result = DialogResult.OK;
				this.Close();
			});
			return;
		case MessageBoxButtons.OKCancel:
			gameObject.GetComponentInChildren<Text>().text = MapMessageBox.Localize("OK");
			gameObject.GetComponent<Button>().onClick.AddListener(delegate()
			{
				this.result = DialogResult.OK;
				this.Close();
			});
			this.CreateButton(gameObject, MapMessageBox.Localize("Cancel"), delegate
			{
				this.result = DialogResult.Cancel;
				this.Close();
			});
			return;
		case MessageBoxButtons.YesNo:
			gameObject.GetComponentInChildren<Text>().text = MapMessageBox.Localize("Yes");
			gameObject.GetComponent<Button>().onClick.AddListener(delegate()
			{
				this.result = DialogResult.Yes;
				this.Close();
			});
			this.CreateButton(gameObject, MapMessageBox.Localize("No"), delegate
			{
				this.result = DialogResult.No;
				this.Close();
			});
			return;
		case MessageBoxButtons.YesNoCancel:
			gameObject.GetComponentInChildren<Text>().text = MapMessageBox.Localize("Yes");
			gameObject.GetComponent<Button>().onClick.AddListener(delegate()
			{
				this.result = DialogResult.Yes;
				this.Close();
			});
			this.CreateButton(gameObject, MapMessageBox.Localize("No"), delegate
			{
				this.result = DialogResult.No;
				this.Close();
			});
			this.CreateButton(gameObject, MapMessageBox.Localize("Cancel"), delegate
			{
				this.result = DialogResult.Cancel;
				this.Close();
			});
			return;
		case MessageBoxButtons.RetryCancel:
			gameObject.GetComponentInChildren<Text>().text = MapMessageBox.Localize("Retry");
			gameObject.GetComponent<Button>().onClick.AddListener(delegate()
			{
				this.result = DialogResult.Retry;
				this.Close();
			});
			this.CreateButton(gameObject, MapMessageBox.Localize("Cancel"), delegate
			{
				this.result = DialogResult.Cancel;
				this.Close();
			});
			return;
		case MessageBoxButtons.AbortRetryIgnore:
			gameObject.GetComponentInChildren<Text>().text = MapMessageBox.Localize("Abort");
			gameObject.GetComponent<Button>().onClick.AddListener(delegate()
			{
				this.result = DialogResult.Abort;
				this.Close();
			});
			this.CreateButton(gameObject, MapMessageBox.Localize("Retry"), delegate
			{
				this.result = DialogResult.Retry;
				this.Close();
			});
			this.CreateButton(gameObject, MapMessageBox.Localize("Ignore"), delegate
			{
				this.result = DialogResult.Ignore;
				this.Close();
			});
			return;
		default:
			return;
		}
	}

	// Token: 0x0600114F RID: 4431 RVA: 0x00010CC8 File Offset: 0x0000EEC8
	private GameObject CreateButton(GameObject buttonToClone, string label, UnityAction target)
	{
		GameObject gameObject = Object.Instantiate<GameObject>(buttonToClone);
		gameObject.transform.SetParent(buttonToClone.transform.parent, false);
		gameObject.GetComponentInChildren<Text>().text = label;
		gameObject.GetComponent<Button>().onClick.AddListener(target);
		return gameObject;
	}

	// Token: 0x06001150 RID: 4432 RVA: 0x00010D04 File Offset: 0x0000EF04
	public override void Close()
	{
		if (this.onFinish != null)
		{
			this.onFinish(this.result);
		}
		base.Close();
	}

	// Token: 0x04000E09 RID: 3593
	[Tooltip("Set this to the name of the prefab that should be loaded when a menu box is shown.")]
	public static string PrefabResourceName = "MapMessageBox";

	// Token: 0x04000E0A RID: 3594
	[Tooltip("Set this to a custom function that will be used to localize the button texts.")]
	public static Func<string, string> Localize = (string sourceString) => sourceString;

	// Token: 0x04000E0B RID: 3595
	[Tooltip("Set to true to send the title and message of message boxes and menus thru the Localize function.")]
	public static bool LocalizeTitleAndMessage = false;

	// Token: 0x04000E0C RID: 3596
	private DialogResult result;

	// Token: 0x04000E0D RID: 3597
	private Action<DialogResult> onFinish;
}

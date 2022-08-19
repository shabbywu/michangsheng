using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200015C RID: 348
public class MapMessageBox : ModalBox
{
	// Token: 0x06000F20 RID: 3872 RVA: 0x0005B7E0 File Offset: 0x000599E0
	public static MapMessageBox Show(string message, Action<DialogResult> onFinished, MessageBoxButtons buttons = MessageBoxButtons.OK)
	{
		return MapMessageBox.Show(message, null, onFinished, buttons);
	}

	// Token: 0x06000F21 RID: 3873 RVA: 0x0005B7EB File Offset: 0x000599EB
	public static MapMessageBox Show(string message, string title = null, Action<DialogResult> onFinished = null, MessageBoxButtons buttons = MessageBoxButtons.OK)
	{
		MapMessageBox component = Object.Instantiate<GameObject>(Resources.Load<GameObject>(MapMessageBox.PrefabResourceName)).GetComponent<MapMessageBox>();
		component.onFinish = onFinished;
		component.SetUpButtons(buttons);
		component.SetText(message, title);
		return component;
	}

	// Token: 0x06000F22 RID: 3874 RVA: 0x0005B818 File Offset: 0x00059A18
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

	// Token: 0x06000F23 RID: 3875 RVA: 0x0005BA8E File Offset: 0x00059C8E
	private GameObject CreateButton(GameObject buttonToClone, string label, UnityAction target)
	{
		GameObject gameObject = Object.Instantiate<GameObject>(buttonToClone);
		gameObject.transform.SetParent(buttonToClone.transform.parent, false);
		gameObject.GetComponentInChildren<Text>().text = label;
		gameObject.GetComponent<Button>().onClick.AddListener(target);
		return gameObject;
	}

	// Token: 0x06000F24 RID: 3876 RVA: 0x0005BACA File Offset: 0x00059CCA
	public override void Close()
	{
		if (this.onFinish != null)
		{
			this.onFinish(this.result);
		}
		base.Close();
	}

	// Token: 0x04000B64 RID: 2916
	[Tooltip("Set this to the name of the prefab that should be loaded when a menu box is shown.")]
	public static string PrefabResourceName = "MapMessageBox";

	// Token: 0x04000B65 RID: 2917
	[Tooltip("Set this to a custom function that will be used to localize the button texts.")]
	public static Func<string, string> Localize = (string sourceString) => sourceString;

	// Token: 0x04000B66 RID: 2918
	[Tooltip("Set to true to send the title and message of message boxes and menus thru the Localize function.")]
	public static bool LocalizeTitleAndMessage = false;

	// Token: 0x04000B67 RID: 2919
	private DialogResult result;

	// Token: 0x04000B68 RID: 2920
	private Action<DialogResult> onFinish;
}

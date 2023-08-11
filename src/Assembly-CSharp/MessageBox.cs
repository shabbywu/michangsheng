using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MessageBox : ModalBox
{
	[Tooltip("Set this to the name of the prefab that should be loaded when a menu box is shown.")]
	public static string PrefabResourceName = "Message Box";

	[Tooltip("Set this to a custom function that will be used to localize the button texts.")]
	public static Func<string, string> Localize = (string sourceString) => sourceString;

	[Tooltip("Set to true to send the title and message of message boxes and menus thru the Localize function.")]
	public static bool LocalizeTitleAndMessage = false;

	private DialogResult result;

	private Action<DialogResult> onFinish;

	public static MessageBox Show(string message, Action<DialogResult> onFinished, MessageBoxButtons buttons = MessageBoxButtons.OK)
	{
		return Show(message, null, onFinished, buttons);
	}

	public static MessageBox Show(string message, string title = null, Action<DialogResult> onFinished = null, MessageBoxButtons buttons = MessageBoxButtons.OK)
	{
		MessageBox component = Object.Instantiate<GameObject>(Resources.Load<GameObject>(PrefabResourceName)).GetComponent<MessageBox>();
		component.onFinish = onFinished;
		component.SetUpButtons(buttons);
		component.SetText(message, title);
		return component;
	}

	private void SetUpButtons(MessageBoxButtons buttons)
	{
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Expected O, but got Unknown
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Expected O, but got Unknown
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Expected O, but got Unknown
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Expected O, but got Unknown
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Expected O, but got Unknown
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Expected O, but got Unknown
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Expected O, but got Unknown
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Expected O, but got Unknown
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Expected O, but got Unknown
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Expected O, but got Unknown
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Expected O, but got Unknown
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Expected O, but got Unknown
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Expected O, but got Unknown
		GameObject gameObject = ((Component)Button).gameObject;
		switch (buttons)
		{
		case MessageBoxButtons.OK:
			gameObject.GetComponentInChildren<Text>().text = Localize("OK");
			((UnityEvent)gameObject.GetComponent<Button>().onClick).AddListener((UnityAction)delegate
			{
				result = DialogResult.OK;
				Close();
			});
			break;
		case MessageBoxButtons.OKCancel:
			gameObject.GetComponentInChildren<Text>().text = Localize("OK");
			((UnityEvent)gameObject.GetComponent<Button>().onClick).AddListener((UnityAction)delegate
			{
				result = DialogResult.OK;
				Close();
			});
			CreateButton(gameObject, Localize("Cancel"), (UnityAction)delegate
			{
				result = DialogResult.Cancel;
				Close();
			});
			break;
		case MessageBoxButtons.YesNo:
			gameObject.GetComponentInChildren<Text>().text = Localize("Yes");
			((UnityEvent)gameObject.GetComponent<Button>().onClick).AddListener((UnityAction)delegate
			{
				result = DialogResult.Yes;
				Close();
			});
			CreateButton(gameObject, Localize("No"), (UnityAction)delegate
			{
				result = DialogResult.No;
				Close();
			});
			break;
		case MessageBoxButtons.RetryCancel:
			gameObject.GetComponentInChildren<Text>().text = Localize("Retry");
			((UnityEvent)gameObject.GetComponent<Button>().onClick).AddListener((UnityAction)delegate
			{
				result = DialogResult.Retry;
				Close();
			});
			CreateButton(gameObject, Localize("Cancel"), (UnityAction)delegate
			{
				result = DialogResult.Cancel;
				Close();
			});
			break;
		case MessageBoxButtons.YesNoCancel:
			gameObject.GetComponentInChildren<Text>().text = Localize("Yes");
			((UnityEvent)gameObject.GetComponent<Button>().onClick).AddListener((UnityAction)delegate
			{
				result = DialogResult.Yes;
				Close();
			});
			CreateButton(gameObject, Localize("No"), (UnityAction)delegate
			{
				result = DialogResult.No;
				Close();
			});
			CreateButton(gameObject, Localize("Cancel"), (UnityAction)delegate
			{
				result = DialogResult.Cancel;
				Close();
			});
			break;
		case MessageBoxButtons.AbortRetryIgnore:
			gameObject.GetComponentInChildren<Text>().text = Localize("Abort");
			((UnityEvent)gameObject.GetComponent<Button>().onClick).AddListener((UnityAction)delegate
			{
				result = DialogResult.Abort;
				Close();
			});
			CreateButton(gameObject, Localize("Retry"), (UnityAction)delegate
			{
				result = DialogResult.Retry;
				Close();
			});
			CreateButton(gameObject, Localize("Ignore"), (UnityAction)delegate
			{
				result = DialogResult.Ignore;
				Close();
			});
			break;
		}
	}

	private GameObject CreateButton(GameObject buttonToClone, string label, UnityAction target)
	{
		GameObject obj = Object.Instantiate<GameObject>(buttonToClone);
		obj.transform.SetParent(buttonToClone.transform.parent, false);
		obj.GetComponentInChildren<Text>().text = label;
		((UnityEvent)obj.GetComponent<Button>().onClick).AddListener(target);
		return obj;
	}

	public override void Close()
	{
		if (onFinish != null)
		{
			onFinish(result);
		}
		base.Close();
	}
}

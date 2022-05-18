using System;
using UnityEngine;

// Token: 0x0200009B RID: 155
[AddComponentMenu("NGUI/Interaction/Saved Option")]
public class UISavedOption : MonoBehaviour
{
	// Token: 0x170000AB RID: 171
	// (get) Token: 0x06000611 RID: 1553 RVA: 0x00009772 File Offset: 0x00007972
	private string key
	{
		get
		{
			if (!string.IsNullOrEmpty(this.keyName))
			{
				return this.keyName;
			}
			return "NGUI State: " + base.name;
		}
	}

	// Token: 0x06000612 RID: 1554 RVA: 0x00009798 File Offset: 0x00007998
	private void Awake()
	{
		this.mList = base.GetComponent<UIPopupList>();
		this.mCheck = base.GetComponent<UIToggle>();
	}

	// Token: 0x06000613 RID: 1555 RVA: 0x00075424 File Offset: 0x00073624
	private void OnEnable()
	{
		if (this.mList != null)
		{
			EventDelegate.Add(this.mList.onChange, new EventDelegate.Callback(this.SaveSelection));
		}
		if (this.mCheck != null)
		{
			EventDelegate.Add(this.mCheck.onChange, new EventDelegate.Callback(this.SaveState));
		}
		if (this.mList != null)
		{
			string @string = PlayerPrefs.GetString(this.key);
			if (!string.IsNullOrEmpty(@string))
			{
				this.mList.value = @string;
			}
			return;
		}
		if (this.mCheck != null)
		{
			this.mCheck.value = (PlayerPrefs.GetInt(this.key, 1) != 0);
			return;
		}
		string string2 = PlayerPrefs.GetString(this.key);
		UIToggle[] componentsInChildren = base.GetComponentsInChildren<UIToggle>(true);
		int i = 0;
		int num = componentsInChildren.Length;
		while (i < num)
		{
			UIToggle uitoggle = componentsInChildren[i];
			uitoggle.value = (uitoggle.name == string2);
			i++;
		}
	}

	// Token: 0x06000614 RID: 1556 RVA: 0x0007551C File Offset: 0x0007371C
	private void OnDisable()
	{
		if (this.mCheck != null)
		{
			EventDelegate.Remove(this.mCheck.onChange, new EventDelegate.Callback(this.SaveState));
		}
		if (this.mList != null)
		{
			EventDelegate.Remove(this.mList.onChange, new EventDelegate.Callback(this.SaveSelection));
		}
		if (this.mCheck == null && this.mList == null)
		{
			UIToggle[] componentsInChildren = base.GetComponentsInChildren<UIToggle>(true);
			int i = 0;
			int num = componentsInChildren.Length;
			while (i < num)
			{
				UIToggle uitoggle = componentsInChildren[i];
				if (uitoggle.value)
				{
					PlayerPrefs.SetString(this.key, uitoggle.name);
					return;
				}
				i++;
			}
		}
	}

	// Token: 0x06000615 RID: 1557 RVA: 0x000097B2 File Offset: 0x000079B2
	public void SaveSelection()
	{
		PlayerPrefs.SetString(this.key, UIPopupList.current.value);
	}

	// Token: 0x06000616 RID: 1558 RVA: 0x000097C9 File Offset: 0x000079C9
	public void SaveState()
	{
		PlayerPrefs.SetInt(this.key, UIToggle.current.value ? 1 : 0);
	}

	// Token: 0x04000474 RID: 1140
	public string keyName;

	// Token: 0x04000475 RID: 1141
	private UIPopupList mList;

	// Token: 0x04000476 RID: 1142
	private UIToggle mCheck;
}

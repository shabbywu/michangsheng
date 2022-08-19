using System;
using UnityEngine;

// Token: 0x02000071 RID: 113
[AddComponentMenu("NGUI/Interaction/Saved Option")]
public class UISavedOption : MonoBehaviour
{
	// Token: 0x1700009B RID: 155
	// (get) Token: 0x060005AD RID: 1453 RVA: 0x0001F556 File Offset: 0x0001D756
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

	// Token: 0x060005AE RID: 1454 RVA: 0x0001F57C File Offset: 0x0001D77C
	private void Awake()
	{
		this.mList = base.GetComponent<UIPopupList>();
		this.mCheck = base.GetComponent<UIToggle>();
	}

	// Token: 0x060005AF RID: 1455 RVA: 0x0001F598 File Offset: 0x0001D798
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

	// Token: 0x060005B0 RID: 1456 RVA: 0x0001F690 File Offset: 0x0001D890
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

	// Token: 0x060005B1 RID: 1457 RVA: 0x0001F745 File Offset: 0x0001D945
	public void SaveSelection()
	{
		PlayerPrefs.SetString(this.key, UIPopupList.current.value);
	}

	// Token: 0x060005B2 RID: 1458 RVA: 0x0001F75C File Offset: 0x0001D95C
	public void SaveState()
	{
		PlayerPrefs.SetInt(this.key, UIToggle.current.value ? 1 : 0);
	}

	// Token: 0x040003C2 RID: 962
	public string keyName;

	// Token: 0x040003C3 RID: 963
	private UIPopupList mList;

	// Token: 0x040003C4 RID: 964
	private UIToggle mCheck;
}

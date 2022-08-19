using System;
using UnityEngine;

// Token: 0x02000053 RID: 83
[RequireComponent(typeof(UIPopupList))]
[AddComponentMenu("NGUI/Interaction/Language Selection")]
public class LanguageSelection : MonoBehaviour
{
	// Token: 0x06000482 RID: 1154 RVA: 0x00018F4C File Offset: 0x0001714C
	private void Start()
	{
		this.mList = base.GetComponent<UIPopupList>();
		if (Localization.knownLanguages != null)
		{
			this.mList.items.Clear();
			int i = 0;
			int num = Localization.knownLanguages.Length;
			while (i < num)
			{
				this.mList.items.Add(Localization.knownLanguages[i]);
				i++;
			}
			this.mList.value = Localization.language;
		}
		EventDelegate.Add(this.mList.onChange, new EventDelegate.Callback(this.OnChange));
	}

	// Token: 0x06000483 RID: 1155 RVA: 0x00018FD4 File Offset: 0x000171D4
	private void OnChange()
	{
		Localization.language = UIPopupList.current.value;
	}

	// Token: 0x040002B5 RID: 693
	private UIPopupList mList;
}

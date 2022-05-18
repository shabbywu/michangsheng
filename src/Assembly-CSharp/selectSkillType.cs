using System;
using GUIPackage;

// Token: 0x020002A1 RID: 673
public class selectSkillType : selectSkillConfig
{
	// Token: 0x06001487 RID: 5255 RVA: 0x00012F42 File Offset: 0x00011142
	private void Awake()
	{
		this.mList = base.GetComponent<UIPopupList>();
	}

	// Token: 0x06001488 RID: 5256 RVA: 0x00012F50 File Offset: 0x00011150
	private void Start()
	{
		EventDelegate.Add(this.mList.onChange, new EventDelegate.Callback(this.OnChange));
	}

	// Token: 0x06001489 RID: 5257 RVA: 0x000B9E60 File Offset: 0x000B8060
	public int getInputID1(string name)
	{
		int num = 0;
		foreach (string b in this.mList.items)
		{
			if (name == b)
			{
				break;
			}
			num++;
		}
		return num;
	}

	// Token: 0x0600148A RID: 5258 RVA: 0x000B9F34 File Offset: 0x000B8134
	private void OnChange()
	{
		Tools.instance.getPlayer();
		int inputID = this.getInputID1(this.mList.value);
		Singleton.skillUI.ShowType = inputID;
	}
}

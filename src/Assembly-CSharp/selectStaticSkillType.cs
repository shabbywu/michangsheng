using System;
using GUIPackage;

// Token: 0x020002A3 RID: 675
public class selectStaticSkillType : selectSkillConfig
{
	// Token: 0x06001493 RID: 5267 RVA: 0x00012F42 File Offset: 0x00011142
	private void Awake()
	{
		this.mList = base.GetComponent<UIPopupList>();
	}

	// Token: 0x06001494 RID: 5268 RVA: 0x00012F8B File Offset: 0x0001118B
	private void Start()
	{
		EventDelegate.Add(this.mList.onChange, new EventDelegate.Callback(this.OnChange));
	}

	// Token: 0x06001495 RID: 5269 RVA: 0x000B9E60 File Offset: 0x000B8060
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

	// Token: 0x06001496 RID: 5270 RVA: 0x000BA018 File Offset: 0x000B8218
	private void OnChange()
	{
		Tools.instance.getPlayer();
		int inputID = this.getInputID1(this.mList.value);
		Singleton.skillUI2.ShowType = inputID;
	}
}

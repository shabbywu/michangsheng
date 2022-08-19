using System;
using GUIPackage;

// Token: 0x020001A6 RID: 422
public class selectStaticSkillType : selectSkillConfig
{
	// Token: 0x060011EC RID: 4588 RVA: 0x0006BCF8 File Offset: 0x00069EF8
	private void Awake()
	{
		this.mList = base.GetComponent<UIPopupList>();
	}

	// Token: 0x060011ED RID: 4589 RVA: 0x0006C020 File Offset: 0x0006A220
	private void Start()
	{
		EventDelegate.Add(this.mList.onChange, new EventDelegate.Callback(this.OnChange));
	}

	// Token: 0x060011EE RID: 4590 RVA: 0x0006C040 File Offset: 0x0006A240
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

	// Token: 0x060011EF RID: 4591 RVA: 0x0006C0A4 File Offset: 0x0006A2A4
	private void OnChange()
	{
		Tools.instance.getPlayer();
		int inputID = this.getInputID1(this.mList.value);
		Singleton.skillUI2.ShowType = inputID;
	}
}

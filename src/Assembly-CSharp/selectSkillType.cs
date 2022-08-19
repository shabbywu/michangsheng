using System;
using GUIPackage;

// Token: 0x020001A4 RID: 420
public class selectSkillType : selectSkillConfig
{
	// Token: 0x060011E0 RID: 4576 RVA: 0x0006BCF8 File Offset: 0x00069EF8
	private void Awake()
	{
		this.mList = base.GetComponent<UIPopupList>();
	}

	// Token: 0x060011E1 RID: 4577 RVA: 0x0006BE9B File Offset: 0x0006A09B
	private void Start()
	{
		EventDelegate.Add(this.mList.onChange, new EventDelegate.Callback(this.OnChange));
	}

	// Token: 0x060011E2 RID: 4578 RVA: 0x0006BEBC File Offset: 0x0006A0BC
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

	// Token: 0x060011E3 RID: 4579 RVA: 0x0006BF20 File Offset: 0x0006A120
	private void OnChange()
	{
		Tools.instance.getPlayer();
		int inputID = this.getInputID1(this.mList.value);
		Singleton.skillUI.ShowType = inputID;
	}
}

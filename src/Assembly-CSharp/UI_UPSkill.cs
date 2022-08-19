using System;
using UnityEngine;

// Token: 0x02000417 RID: 1047
public class UI_UPSkill : MonoBehaviour
{
	// Token: 0x060021B6 RID: 8630 RVA: 0x00004095 File Offset: 0x00002295
	public void initSkill(int StaticSkillID)
	{
	}

	// Token: 0x060021B7 RID: 8631 RVA: 0x000E9841 File Offset: 0x000E7A41
	public void close()
	{
		base.transform.localScale = Vector3.zero;
	}

	// Token: 0x060021B8 RID: 8632 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x060021B9 RID: 8633 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x04001B31 RID: 6961
	public UILabel label;

	// Token: 0x04001B32 RID: 6962
	public UILabel label1;

	// Token: 0x04001B33 RID: 6963
	public UILabel label2;

	// Token: 0x04001B34 RID: 6964
	public UILabel label3;

	// Token: 0x04001B35 RID: 6965
	public UILabel label4;

	// Token: 0x04001B36 RID: 6966
	public UIButton btn;

	// Token: 0x04001B37 RID: 6967
	public GameObject obj1;

	// Token: 0x04001B38 RID: 6968
	public GameObject grid;
}

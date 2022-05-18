using System;
using UnityEngine;

// Token: 0x020005CA RID: 1482
public class UI_UPSkill : MonoBehaviour
{
	// Token: 0x06002570 RID: 9584 RVA: 0x000042DD File Offset: 0x000024DD
	public void initSkill(int StaticSkillID)
	{
	}

	// Token: 0x06002571 RID: 9585 RVA: 0x0001E04E File Offset: 0x0001C24E
	public void close()
	{
		base.transform.localScale = Vector3.zero;
	}

	// Token: 0x06002572 RID: 9586 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x06002573 RID: 9587 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x04001FF2 RID: 8178
	public UILabel label;

	// Token: 0x04001FF3 RID: 8179
	public UILabel label1;

	// Token: 0x04001FF4 RID: 8180
	public UILabel label2;

	// Token: 0x04001FF5 RID: 8181
	public UILabel label3;

	// Token: 0x04001FF6 RID: 8182
	public UILabel label4;

	// Token: 0x04001FF7 RID: 8183
	public UIButton btn;

	// Token: 0x04001FF8 RID: 8184
	public GameObject obj1;

	// Token: 0x04001FF9 RID: 8185
	public GameObject grid;
}

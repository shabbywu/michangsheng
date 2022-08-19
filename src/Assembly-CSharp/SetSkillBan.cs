using System;
using UnityEngine;

// Token: 0x02000467 RID: 1127
public class SetSkillBan : MonoBehaviour
{
	// Token: 0x06002359 RID: 9049 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x0600235A RID: 9050 RVA: 0x000F1EF6 File Offset: 0x000F00F6
	public void OnValueChenge(bool b)
	{
		if (b)
		{
			this.Show();
			return;
		}
		this.Hide();
	}

	// Token: 0x0600235B RID: 9051 RVA: 0x000F1F08 File Offset: 0x000F0108
	public void Show()
	{
		RectTransform component = base.transform.parent.GetComponent<RectTransform>();
		component.sizeDelta = new Vector2(component.sizeDelta.x, 100f);
	}

	// Token: 0x0600235C RID: 9052 RVA: 0x000F1F34 File Offset: 0x000F0134
	public void Hide()
	{
		RectTransform component = base.transform.parent.GetComponent<RectTransform>();
		component.sizeDelta = new Vector2(component.sizeDelta.x, -70.7f);
	}
}

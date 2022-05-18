using System;
using UnityEngine;

// Token: 0x02000623 RID: 1571
public class SetSkillBan : MonoBehaviour
{
	// Token: 0x0600270F RID: 9999 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x06002710 RID: 10000 RVA: 0x0001F09D File Offset: 0x0001D29D
	public void OnValueChenge(bool b)
	{
		if (b)
		{
			this.Show();
			return;
		}
		this.Hide();
	}

	// Token: 0x06002711 RID: 10001 RVA: 0x0001F0AF File Offset: 0x0001D2AF
	public void Show()
	{
		RectTransform component = base.transform.parent.GetComponent<RectTransform>();
		component.sizeDelta = new Vector2(component.sizeDelta.x, 100f);
	}

	// Token: 0x06002712 RID: 10002 RVA: 0x0001F0DB File Offset: 0x0001D2DB
	public void Hide()
	{
		RectTransform component = base.transform.parent.GetComponent<RectTransform>();
		component.sizeDelta = new Vector2(component.sizeDelta.x, -70.7f);
	}
}

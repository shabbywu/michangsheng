using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000479 RID: 1145
public class helptext : MonoBehaviour
{
	// Token: 0x060023C1 RID: 9153 RVA: 0x000F4B21 File Offset: 0x000F2D21
	private void Awake()
	{
		helptext.instence = this;
		this.helptextinfo = base.transform.Find("Text").GetComponent<Text>();
		this.heloanimation = base.gameObject.GetComponent<Animation>();
	}

	// Token: 0x060023C2 RID: 9154 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x060023C3 RID: 9155 RVA: 0x000F4B58 File Offset: 0x000F2D58
	public void open()
	{
		this.heloanimation["helptxt"].speed = -1f;
		this.heloanimation["helptxt"].time = this.heloanimation["helptxt"].clip.length;
		this.heloanimation.CrossFade("helptxt");
	}

	// Token: 0x060023C4 RID: 9156 RVA: 0x000F4BC0 File Offset: 0x000F2DC0
	public void close()
	{
		this.heloanimation["helptxt"].speed = 1f;
		this.heloanimation["helptxt"].time = 0f;
		this.heloanimation.CrossFade("helptxt");
	}

	// Token: 0x060023C5 RID: 9157 RVA: 0x000F4C11 File Offset: 0x000F2E11
	public void showhelp(string text)
	{
		this.helptextinfo.text = text;
		this.open();
	}

	// Token: 0x04001C96 RID: 7318
	public static helptext instence;

	// Token: 0x04001C97 RID: 7319
	public Text helptextinfo;

	// Token: 0x04001C98 RID: 7320
	public Animation heloanimation;
}

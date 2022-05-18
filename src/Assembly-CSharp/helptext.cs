using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000637 RID: 1591
public class helptext : MonoBehaviour
{
	// Token: 0x0600277E RID: 10110 RVA: 0x0001F450 File Offset: 0x0001D650
	private void Awake()
	{
		helptext.instence = this;
		this.helptextinfo = base.transform.Find("Text").GetComponent<Text>();
		this.heloanimation = base.gameObject.GetComponent<Animation>();
	}

	// Token: 0x0600277F RID: 10111 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x06002780 RID: 10112 RVA: 0x00134A40 File Offset: 0x00132C40
	public void open()
	{
		this.heloanimation["helptxt"].speed = -1f;
		this.heloanimation["helptxt"].time = this.heloanimation["helptxt"].clip.length;
		this.heloanimation.CrossFade("helptxt");
	}

	// Token: 0x06002781 RID: 10113 RVA: 0x00134AA8 File Offset: 0x00132CA8
	public void close()
	{
		this.heloanimation["helptxt"].speed = 1f;
		this.heloanimation["helptxt"].time = 0f;
		this.heloanimation.CrossFade("helptxt");
	}

	// Token: 0x06002782 RID: 10114 RVA: 0x0001F484 File Offset: 0x0001D684
	public void showhelp(string text)
	{
		this.helptextinfo.text = text;
		this.open();
	}

	// Token: 0x04002171 RID: 8561
	public static helptext instence;

	// Token: 0x04002172 RID: 8562
	public Text helptextinfo;

	// Token: 0x04002173 RID: 8563
	public Animation heloanimation;
}

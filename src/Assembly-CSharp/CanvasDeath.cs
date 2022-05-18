using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YSGame;

// Token: 0x02000798 RID: 1944
public class CanvasDeath : MonoBehaviour
{
	// Token: 0x06003170 RID: 12656 RVA: 0x000042DD File Offset: 0x000024DD
	private void Awake()
	{
	}

	// Token: 0x06003171 RID: 12657 RVA: 0x0002428D File Offset: 0x0002248D
	public void showDeath()
	{
		base.transform.Find("Win").localScale = Vector3.one;
	}

	// Token: 0x06003172 RID: 12658 RVA: 0x000242A9 File Offset: 0x000224A9
	private void Start()
	{
		MusicMag.instance.PlayEffectMusic(8, 1f);
	}

	// Token: 0x06003173 RID: 12659 RVA: 0x000242BB File Offset: 0x000224BB
	public void Update()
	{
		if (Input.anyKeyDown)
		{
			this.backToHome();
		}
	}

	// Token: 0x06003174 RID: 12660 RVA: 0x0018A3D8 File Offset: 0x001885D8
	public void setText(int type)
	{
		this.text = base.transform.Find("Win/desc").GetComponent<Text>();
		foreach (GameObject gameObject in this.WenZiObj)
		{
			gameObject.gameObject.SetActive(false);
		}
		this.WenZiObj[type - 1].SetActive(true);
	}

	// Token: 0x06003175 RID: 12661 RVA: 0x0018A460 File Offset: 0x00188660
	public void backToHome()
	{
		if (this.isclick)
		{
			return;
		}
		this.isclick = true;
		YSSaveGame.Reset();
		KBEngineApp.app.entities[10] = null;
		KBEngineApp.app.entities.Remove(10);
		SceneManager.LoadScene("MainMenu");
	}

	// Token: 0x04002DBC RID: 11708
	private Text text;

	// Token: 0x04002DBD RID: 11709
	public List<Sprite> sprites;

	// Token: 0x04002DBE RID: 11710
	public Image image;

	// Token: 0x04002DBF RID: 11711
	private bool isclick;

	// Token: 0x04002DC0 RID: 11712
	public List<GameObject> WenZiObj;
}

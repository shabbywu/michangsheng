using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YSGame;

// Token: 0x02000507 RID: 1287
public class CanvasDeath : MonoBehaviour
{
	// Token: 0x0600296B RID: 10603 RVA: 0x00004095 File Offset: 0x00002295
	private void Awake()
	{
	}

	// Token: 0x0600296C RID: 10604 RVA: 0x0013CF38 File Offset: 0x0013B138
	public void showDeath()
	{
		base.transform.Find("Win").localScale = Vector3.one;
	}

	// Token: 0x0600296D RID: 10605 RVA: 0x0013CF54 File Offset: 0x0013B154
	private void Start()
	{
		MusicMag.instance.PlayEffectMusic(8, 1f);
	}

	// Token: 0x0600296E RID: 10606 RVA: 0x0013CF66 File Offset: 0x0013B166
	public void Update()
	{
		if (Input.anyKeyDown)
		{
			this.backToHome();
		}
	}

	// Token: 0x0600296F RID: 10607 RVA: 0x0013CF78 File Offset: 0x0013B178
	public void setText(int type)
	{
		this.text = base.transform.Find("Win/desc").GetComponent<Text>();
		foreach (GameObject gameObject in this.WenZiObj)
		{
			gameObject.gameObject.SetActive(false);
		}
		this.WenZiObj[type - 1].SetActive(true);
	}

	// Token: 0x06002970 RID: 10608 RVA: 0x0013D000 File Offset: 0x0013B200
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

	// Token: 0x040025D4 RID: 9684
	private Text text;

	// Token: 0x040025D5 RID: 9685
	public List<Sprite> sprites;

	// Token: 0x040025D6 RID: 9686
	public Image image;

	// Token: 0x040025D7 RID: 9687
	private bool isclick;

	// Token: 0x040025D8 RID: 9688
	public List<GameObject> WenZiObj;
}

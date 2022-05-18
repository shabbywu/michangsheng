using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000682 RID: 1666
public class CollectCoin : MonoBehaviour
{
	// Token: 0x060029A4 RID: 10660 RVA: 0x00142E20 File Offset: 0x00141020
	private void Awake()
	{
		this.gameManager = GameObject.Find("_GameManager");
		this.manage = this.gameManager.GetComponent<Manage>();
		this.Monkey = GameObject.FindGameObjectWithTag("Monkey").transform;
		this.controller = this.Monkey.GetComponent<MonkeyController2D>();
		this.orgPos = base.transform.localPosition;
		this.novcicMeshRenderer = base.transform.Find("NovcicNovi").GetChild(1).GetComponent<Renderer>();
	}

	// Token: 0x060029A5 RID: 10661 RVA: 0x0002053B File Offset: 0x0001E73B
	private void Start()
	{
		this.coinsCollectedText = this.manage.coinsCollectedText;
		this.effects = this.coinsCollectedText.GetComponent<TextMeshEffects>();
	}

	// Token: 0x060029A6 RID: 10662 RVA: 0x00142EA8 File Offset: 0x001410A8
	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Monkey" && this.controller.state != MonkeyController2D.State.wasted)
		{
			if (Manage.coinsCollected % 3 == 0)
			{
				if (PlaySounds.soundOn)
				{
					PlaySounds.Play_CollectCoin_3rd();
				}
			}
			else if (Manage.coinsCollected % 2 == 0)
			{
				if (PlaySounds.soundOn)
				{
					PlaySounds.Play_CollectCoin_2nd();
				}
			}
			else if (PlaySounds.soundOn)
			{
				PlaySounds.Play_CollectCoin();
			}
			this.coinSparkle.Play();
			this.coinWave.Play();
			this.coinSparkle1.Play();
			if (!this.magnetDrag)
			{
				base.GetComponent<Animation>().Play();
				base.Invoke("DisableRenderer", 1f);
				base.Invoke("WaitAndTurnOff", 5f);
			}
			else
			{
				base.Invoke("WaitAndTurnOff", 0.5f);
			}
			base.GetComponent<Collider2D>().enabled = false;
			if (this.manage.PowerUp_doubleCoins)
			{
				Manage.coinsCollected += 2;
				MissionManager.Instance.CoinEvent(Manage.coinsCollected);
			}
			else
			{
				Manage.coinsCollected++;
				MissionManager.Instance.CoinEvent(Manage.coinsCollected);
			}
			this.coinsCollectedText.text = Manage.coinsCollected.ToString();
			this.effects.RefreshTextOutline(false, true, true);
			Manage.points += 10;
			Manage.pointsText.text = Manage.points.ToString();
			Manage.pointsEffects.RefreshTextOutline(false, true, true);
			this.magnetDrag = false;
			return;
		}
		if (col.tag == "Magnet")
		{
			this.magnetDrag = true;
			base.StartCoroutine(this.magnetWorking());
			base.GetComponent<Collider2D>().enabled = false;
		}
	}

	// Token: 0x060029A7 RID: 10663 RVA: 0x0002055F File Offset: 0x0001E75F
	private void DisableRenderer()
	{
		this.novcicMeshRenderer.enabled = false;
		base.transform.localScale = Vector3.one;
		this.magnetAnimacija = false;
	}

	// Token: 0x060029A8 RID: 10664 RVA: 0x00020584 File Offset: 0x0001E784
	private IEnumerator magnetWorking()
	{
		this.orgPos = base.transform.localPosition;
		float t = 0f;
		while (t < 0.25f)
		{
			if (base.transform.position.x < this.Monkey.position.x + 3f && base.transform.position.x >= this.Monkey.position.x - 2f && !this.magnetAnimacija)
			{
				this.magnetAnimacija = true;
				base.GetComponent<Animation>().Play("CoinCollectedWithMagnet");
			}
			base.transform.position = Vector3.Lerp(base.transform.position, this.Monkey.position + new Vector3(1f, 1f, 0f), t);
			t += Time.deltaTime / 3f;
			yield return null;
		}
		if (Manage.coinsCollected % 3 == 0)
		{
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_CollectCoin_3rd();
			}
		}
		else if (Manage.coinsCollected % 2 == 0)
		{
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_CollectCoin_2nd();
			}
		}
		else if (PlaySounds.soundOn)
		{
			PlaySounds.Play_CollectCoin();
		}
		if (this.manage.PowerUp_doubleCoins)
		{
			Manage.coinsCollected += 2;
			MissionManager.Instance.CoinEvent(Manage.coinsCollected);
		}
		else
		{
			Manage.coinsCollected++;
			MissionManager.Instance.CoinEvent(Manage.coinsCollected);
		}
		this.coinsCollectedText.text = Manage.coinsCollected.ToString();
		this.effects.RefreshTextOutline(false, true, true);
		Manage.points += 10;
		Manage.pointsText.text = Manage.points.ToString();
		Manage.pointsEffects.RefreshTextOutline(false, true, true);
		this.magnetDrag = false;
		base.Invoke("DisableRenderer", 1f);
		base.Invoke("WaitAndTurnOff", 5f);
		yield break;
	}

	// Token: 0x060029A9 RID: 10665 RVA: 0x00020593 File Offset: 0x0001E793
	private void WaitAndTurnOff()
	{
		if (MonkeyController2D.canRespawnThings)
		{
			base.transform.localPosition = this.orgPos;
			this.novcicMeshRenderer.enabled = true;
			base.GetComponent<Collider2D>().enabled = true;
		}
	}

	// Token: 0x04002345 RID: 9029
	public ParticleSystem coinSparkle;

	// Token: 0x04002346 RID: 9030
	public ParticleSystem coinSparkle1;

	// Token: 0x04002347 RID: 9031
	public ParticleSystem coinWave;

	// Token: 0x04002348 RID: 9032
	private GameObject gameManager;

	// Token: 0x04002349 RID: 9033
	private TextMesh coinsCollectedText;

	// Token: 0x0400234A RID: 9034
	private Manage manage;

	// Token: 0x0400234B RID: 9035
	private Animation coinRotate;

	// Token: 0x0400234C RID: 9036
	private bool magnetDrag;

	// Token: 0x0400234D RID: 9037
	private Transform Monkey;

	// Token: 0x0400234E RID: 9038
	private MonkeyController2D controller;

	// Token: 0x0400234F RID: 9039
	private Renderer novcicMeshRenderer;

	// Token: 0x04002350 RID: 9040
	private Vector3 orgPos;

	// Token: 0x04002351 RID: 9041
	private TextMeshEffects effects;

	// Token: 0x04002352 RID: 9042
	private bool magnetAnimacija;
}

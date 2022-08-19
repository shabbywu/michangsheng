using System;
using System.Collections;
using UnityEngine;

// Token: 0x020004A3 RID: 1187
public class CollectCoin : MonoBehaviour
{
	// Token: 0x0600256E RID: 9582 RVA: 0x00103230 File Offset: 0x00101430
	private void Awake()
	{
		this.gameManager = GameObject.Find("_GameManager");
		this.manage = this.gameManager.GetComponent<Manage>();
		this.Monkey = GameObject.FindGameObjectWithTag("Monkey").transform;
		this.controller = this.Monkey.GetComponent<MonkeyController2D>();
		this.orgPos = base.transform.localPosition;
		this.novcicMeshRenderer = base.transform.Find("NovcicNovi").GetChild(1).GetComponent<Renderer>();
	}

	// Token: 0x0600256F RID: 9583 RVA: 0x001032B6 File Offset: 0x001014B6
	private void Start()
	{
		this.coinsCollectedText = this.manage.coinsCollectedText;
		this.effects = this.coinsCollectedText.GetComponent<TextMeshEffects>();
	}

	// Token: 0x06002570 RID: 9584 RVA: 0x001032DC File Offset: 0x001014DC
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

	// Token: 0x06002571 RID: 9585 RVA: 0x0010348C File Offset: 0x0010168C
	private void DisableRenderer()
	{
		this.novcicMeshRenderer.enabled = false;
		base.transform.localScale = Vector3.one;
		this.magnetAnimacija = false;
	}

	// Token: 0x06002572 RID: 9586 RVA: 0x001034B1 File Offset: 0x001016B1
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

	// Token: 0x06002573 RID: 9587 RVA: 0x001034C0 File Offset: 0x001016C0
	private void WaitAndTurnOff()
	{
		if (MonkeyController2D.canRespawnThings)
		{
			base.transform.localPosition = this.orgPos;
			this.novcicMeshRenderer.enabled = true;
			base.GetComponent<Collider2D>().enabled = true;
		}
	}

	// Token: 0x04001E23 RID: 7715
	public ParticleSystem coinSparkle;

	// Token: 0x04001E24 RID: 7716
	public ParticleSystem coinSparkle1;

	// Token: 0x04001E25 RID: 7717
	public ParticleSystem coinWave;

	// Token: 0x04001E26 RID: 7718
	private GameObject gameManager;

	// Token: 0x04001E27 RID: 7719
	private TextMesh coinsCollectedText;

	// Token: 0x04001E28 RID: 7720
	private Manage manage;

	// Token: 0x04001E29 RID: 7721
	private Animation coinRotate;

	// Token: 0x04001E2A RID: 7722
	private bool magnetDrag;

	// Token: 0x04001E2B RID: 7723
	private Transform Monkey;

	// Token: 0x04001E2C RID: 7724
	private MonkeyController2D controller;

	// Token: 0x04001E2D RID: 7725
	private Renderer novcicMeshRenderer;

	// Token: 0x04001E2E RID: 7726
	private Vector3 orgPos;

	// Token: 0x04001E2F RID: 7727
	private TextMeshEffects effects;

	// Token: 0x04001E30 RID: 7728
	private bool magnetAnimacija;
}

using System.Collections;
using UnityEngine;

public class CollectCoin : MonoBehaviour
{
	public ParticleSystem coinSparkle;

	public ParticleSystem coinSparkle1;

	public ParticleSystem coinWave;

	private GameObject gameManager;

	private TextMesh coinsCollectedText;

	private Manage manage;

	private Animation coinRotate;

	private bool magnetDrag;

	private Transform Monkey;

	private MonkeyController2D controller;

	private Renderer novcicMeshRenderer;

	private Vector3 orgPos;

	private TextMeshEffects effects;

	private bool magnetAnimacija;

	private void Awake()
	{
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		gameManager = GameObject.Find("_GameManager");
		manage = gameManager.GetComponent<Manage>();
		Monkey = GameObject.FindGameObjectWithTag("Monkey").transform;
		controller = ((Component)Monkey).GetComponent<MonkeyController2D>();
		orgPos = ((Component)this).transform.localPosition;
		novcicMeshRenderer = ((Component)((Component)this).transform.Find("NovcicNovi").GetChild(1)).GetComponent<Renderer>();
	}

	private void Start()
	{
		coinsCollectedText = manage.coinsCollectedText;
		effects = ((Component)coinsCollectedText).GetComponent<TextMeshEffects>();
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (((Component)col).tag == "Monkey" && controller.state != MonkeyController2D.State.wasted)
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
			coinSparkle.Play();
			coinWave.Play();
			coinSparkle1.Play();
			if (!magnetDrag)
			{
				((Component)this).GetComponent<Animation>().Play();
				((MonoBehaviour)this).Invoke("DisableRenderer", 1f);
				((MonoBehaviour)this).Invoke("WaitAndTurnOff", 5f);
			}
			else
			{
				((MonoBehaviour)this).Invoke("WaitAndTurnOff", 0.5f);
			}
			((Behaviour)((Component)this).GetComponent<Collider2D>()).enabled = false;
			if (manage.PowerUp_doubleCoins)
			{
				Manage.coinsCollected += 2;
				MissionManager.Instance.CoinEvent(Manage.coinsCollected);
			}
			else
			{
				Manage.coinsCollected++;
				MissionManager.Instance.CoinEvent(Manage.coinsCollected);
			}
			coinsCollectedText.text = Manage.coinsCollected.ToString();
			effects.RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
			Manage.points += 10;
			Manage.pointsText.text = Manage.points.ToString();
			Manage.pointsEffects.RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
			magnetDrag = false;
		}
		else if (((Component)col).tag == "Magnet")
		{
			magnetDrag = true;
			((MonoBehaviour)this).StartCoroutine(magnetWorking());
			((Behaviour)((Component)this).GetComponent<Collider2D>()).enabled = false;
		}
	}

	private void DisableRenderer()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		novcicMeshRenderer.enabled = false;
		((Component)this).transform.localScale = Vector3.one;
		magnetAnimacija = false;
	}

	private IEnumerator magnetWorking()
	{
		orgPos = ((Component)this).transform.localPosition;
		float t = 0f;
		while (t < 0.25f)
		{
			if (((Component)this).transform.position.x < Monkey.position.x + 3f && ((Component)this).transform.position.x >= Monkey.position.x - 2f && !magnetAnimacija)
			{
				magnetAnimacija = true;
				((Component)this).GetComponent<Animation>().Play("CoinCollectedWithMagnet");
			}
			((Component)this).transform.position = Vector3.Lerp(((Component)this).transform.position, Monkey.position + new Vector3(1f, 1f, 0f), t);
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
		if (manage.PowerUp_doubleCoins)
		{
			Manage.coinsCollected += 2;
			MissionManager.Instance.CoinEvent(Manage.coinsCollected);
		}
		else
		{
			Manage.coinsCollected++;
			MissionManager.Instance.CoinEvent(Manage.coinsCollected);
		}
		coinsCollectedText.text = Manage.coinsCollected.ToString();
		effects.RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
		Manage.points += 10;
		Manage.pointsText.text = Manage.points.ToString();
		Manage.pointsEffects.RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
		magnetDrag = false;
		((MonoBehaviour)this).Invoke("DisableRenderer", 1f);
		((MonoBehaviour)this).Invoke("WaitAndTurnOff", 5f);
	}

	private void WaitAndTurnOff()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		if (MonkeyController2D.canRespawnThings)
		{
			((Component)this).transform.localPosition = orgPos;
			novcicMeshRenderer.enabled = true;
			((Behaviour)((Component)this).GetComponent<Collider2D>()).enabled = true;
		}
	}
}

using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YSGame;

public class CanvasDeath : MonoBehaviour
{
	private Text text;

	public List<Sprite> sprites;

	public Image image;

	private bool isclick;

	public List<GameObject> WenZiObj;

	private void Awake()
	{
	}

	public void showDeath()
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		((Component)this).transform.Find("Win").localScale = Vector3.one;
	}

	private void Start()
	{
		MusicMag.instance.PlayEffectMusic(8);
	}

	public void Update()
	{
		if (Input.anyKeyDown)
		{
			backToHome();
		}
	}

	public void setText(int type)
	{
		text = ((Component)((Component)this).transform.Find("Win/desc")).GetComponent<Text>();
		foreach (GameObject item in WenZiObj)
		{
			item.gameObject.SetActive(false);
		}
		WenZiObj[type - 1].SetActive(true);
	}

	public void backToHome()
	{
		if (!isclick)
		{
			isclick = true;
			YSSaveGame.Reset();
			KBEngineApp.app.entities[10] = null;
			KBEngineApp.app.entities.Remove(10);
			SceneManager.LoadScene("MainMenu");
		}
	}
}

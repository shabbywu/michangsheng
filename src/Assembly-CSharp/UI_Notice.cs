using UnityEngine;

public class UI_Notice : MonoBehaviour
{
	public GameObject NoticeText;

	public Animation heloanimation;

	private int opentype;

	private void Awake()
	{
		heloanimation = NoticeText.gameObject.GetComponent<Animation>();
	}

	private void Start()
	{
	}

	public void open()
	{
		heloanimation["notice2"].speed = 1f;
		heloanimation["notice2"].time = 0f;
		heloanimation.CrossFade("notice2");
		NoticeText.SetActive(true);
		opentype = 1;
	}

	public void close()
	{
		heloanimation["notice2"].speed = -1f;
		heloanimation["notice2"].time = heloanimation["notice2"].clip.length;
		heloanimation.CrossFade("notice2");
		opentype = 0;
	}

	public void buttondwon()
	{
		if (opentype == 0)
		{
			open();
		}
		else
		{
			close();
		}
	}
}

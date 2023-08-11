using UnityEngine;
using UnityEngine.UI;

public class helptext : MonoBehaviour
{
	public static helptext instence;

	public Text helptextinfo;

	public Animation heloanimation;

	private void Awake()
	{
		instence = this;
		helptextinfo = ((Component)((Component)this).transform.Find("Text")).GetComponent<Text>();
		heloanimation = ((Component)this).gameObject.GetComponent<Animation>();
	}

	private void Start()
	{
	}

	public void open()
	{
		heloanimation["helptxt"].speed = -1f;
		heloanimation["helptxt"].time = heloanimation["helptxt"].clip.length;
		heloanimation.CrossFade("helptxt");
	}

	public void close()
	{
		heloanimation["helptxt"].speed = 1f;
		heloanimation["helptxt"].time = 0f;
		heloanimation.CrossFade("helptxt");
	}

	public void showhelp(string text)
	{
		helptextinfo.text = text;
		open();
	}
}

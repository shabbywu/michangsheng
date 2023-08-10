using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ErrorHint : MonoBehaviour
{
	protected delegate void showNextDelegate(string aa);

	private static UI_ErrorHint instance;

	protected List<showNextDelegate> delegateList = new List<showNextDelegate>();

	protected bool closequen;

	public List<Sprite> spriteList;

	public Image spritUI;

	public bool isShowing;

	public Text text_content;

	protected Animator ani_control;

	public string anmaitonName = "Error";

	public static UI_ErrorHint _instance
	{
		get
		{
			if ((Object)(object)instance == (Object)null)
			{
				instance = ((Component)Object.Instantiate<GameObject>(jsonData.instance.TextError).transform.Find("Text_error")).GetComponent<UI_ErrorHint>();
				instance.ani_control = ((Component)instance).gameObject.GetComponent<Animator>();
			}
			return instance;
		}
	}

	private void Awake()
	{
		instance = this;
		ani_control = ((Component)this).gameObject.GetComponent<Animator>();
	}

	private void OnDestroy()
	{
		instance = null;
	}

	public virtual void errorShow(string str, int showType = 0)
	{
		delegateList.Add(delegate
		{
			isShowing = true;
			text_content.text = str;
			if ((Object)(object)spritUI != (Object)null && spriteList != null)
			{
				spritUI.sprite = spriteList[showType];
			}
			ani_control.SetTrigger(anmaitonName);
		});
		if (delegateList.Count == 1)
		{
			delegateList[0](str);
		}
	}

	public virtual void animationFinsh()
	{
		isShowing = false;
		delegateList.RemoveAt(0);
		if (delegateList.Count > 0)
		{
			delegateList[0]("");
		}
	}
}

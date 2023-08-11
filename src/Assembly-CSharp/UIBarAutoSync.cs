using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UIBarAutoSync : MonoBehaviour
{
	public Image Target;

	public float DelayTime = 0.1f;

	private Image me;

	private bool nowAnim;

	private void Awake()
	{
		me = ((Component)this).GetComponent<Image>();
	}

	private void Update()
	{
		if ((Object)(object)Target != (Object)null && !nowAnim && me.fillAmount != Target.fillAmount)
		{
			nowAnim = true;
			((MonoBehaviour)this).StartCoroutine(MoveAnim());
		}
	}

	private IEnumerator MoveAnim()
	{
		yield return (object)new WaitForSeconds(DelayTime);
		((Tween)DOTweenModuleUI.DOFillAmount(me, Target.fillAmount, 0.3f)).onComplete = (TweenCallback)delegate
		{
			nowAnim = false;
		};
	}
}

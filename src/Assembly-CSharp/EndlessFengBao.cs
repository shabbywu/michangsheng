using System.Collections;
using DG.Tweening;
using Spine.Unity;
using UnityEngine;

public class EndlessFengBao : MonoBehaviour
{
	public int id;

	public int index;

	public int lv;

	public int seaid;

	private MeshRenderer mr;

	private SkeletonAnimation anim;

	private void Awake()
	{
		mr = ((Component)this).GetComponentInChildren<MeshRenderer>();
		anim = ((Component)this).GetComponentInChildren<SkeletonAnimation>();
	}

	public void Show()
	{
		((Component)anim).gameObject.SetActive(false);
		if (lv == 1)
		{
			if (SceneEx.NowSceneName == "Sea2")
			{
				anim.AnimationName = $"fengbao1_1_{PlayerEx.Player.RandomSeedNext() % 2 + 1}";
			}
			else
			{
				anim.AnimationName = $"fengbao1_2_{PlayerEx.Player.RandomSeedNext() % 2 + 1}";
			}
		}
		else
		{
			anim.AnimationName = $"fengbao{lv}";
		}
		((Renderer)mr).sortingOrder = -1000;
		((MonoBehaviour)this).StartCoroutine("RandomPlay");
	}

	private IEnumerator RandomPlay()
	{
		float num = ((lv != 1) ? Random.Range(0f, 0.5f) : Random.Range(0f, 3f));
		yield return (object)new WaitForSeconds(num);
		((Component)anim).gameObject.SetActive(true);
		yield return (object)new WaitForEndOfFrame();
		((Renderer)mr).sortingOrder = lv;
	}

	public void Move(Vector3 endPositon)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		ShortcutExtensions.DOMove(((Component)this).transform, endPositon, 1f, false);
	}
}

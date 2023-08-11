using System.Collections;
using Spine.Unity;
using UnityEngine;

public class LangHuaItem : MonoBehaviour
{
	public SkeletonAnimation SpineAnim;

	private MeshRenderer r;

	private int order;

	public void Show()
	{
		r = ((Component)this).GetComponent<MeshRenderer>();
		order = ((Renderer)r).sortingOrder;
		((Renderer)r).sortingOrder = -1000;
		((Component)this).gameObject.SetActive(true);
		((MonoBehaviour)this).StartCoroutine("ChangeOrder");
	}

	private IEnumerator ChangeOrder()
	{
		yield return (object)new WaitForEndOfFrame();
		((Renderer)r).sortingOrder = order;
	}
}

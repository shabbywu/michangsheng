using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class ResRefHolder : MonoBehaviour
{
	public static ResRefHolder Inst;

	public List<SkeletonDataAsset> SeaJiZhiRes;

	private void Awake()
	{
		Inst = this;
	}
}

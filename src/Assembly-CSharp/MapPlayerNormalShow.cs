using System;
using JSONClass;
using Spine.Unity;
using UnityEngine;

// Token: 0x02000195 RID: 405
public class MapPlayerNormalShow : MonoBehaviour
{
	// Token: 0x0600113E RID: 4414 RVA: 0x00067B57 File Offset: 0x00065D57
	public void Init(MapPlayerController controller)
	{
		this.controller = controller;
		this.lastx = base.transform.position.x;
	}

	// Token: 0x0600113F RID: 4415 RVA: 0x00067B78 File Offset: 0x00065D78
	private void Update()
	{
		if (this.controller.ShowType == MapPlayerShowType.普通人物 || this.controller.ShowType == MapPlayerShowType.遁术)
		{
			float num = base.transform.position.x - this.lastx;
			if (Mathf.Abs(num) > 0.01f)
			{
				this.PlayerSpine.transform.localScale = ((num > 0f) ? Vector3.one : MapPlayerNormalShow.fanXiang);
				this.lastx = base.transform.position.x;
			}
		}
	}

	// Token: 0x06001140 RID: 4416 RVA: 0x00067C00 File Offset: 0x00065E00
	public void Refresh()
	{
		if (this.meshRenderer == null)
		{
			this.meshRenderer = this.PlayerSpine.GetComponent<MeshRenderer>();
		}
		if (this.controller.IsOnSea)
		{
			this.meshRenderer.sortingOrder = 1;
		}
		MapPlayerShowType showType = this.controller.ShowType;
		if (showType == MapPlayerShowType.普通人物)
		{
			this.PlayerObj.SetActive(true);
			this.LoadSpine("MapPlayerWalk");
			return;
		}
		if (showType != MapPlayerShowType.遁术)
		{
			this.PlayerObj.SetActive(false);
			return;
		}
		this.PlayerObj.SetActive(true);
		this.LoadSpine(this.NowDunShuSpineSeid.Spine);
	}

	// Token: 0x06001141 RID: 4417 RVA: 0x00067C9C File Offset: 0x00065E9C
	public void LoadSpine(string spine)
	{
		if (this.nowSpineName != spine)
		{
			this.nowSpineName = spine;
			SkeletonDataAsset skeletonDataAsset = Resources.Load<SkeletonDataAsset>(string.Concat(new string[]
			{
				"Spine/MapPlayer/",
				spine,
				"/",
				spine,
				"_SkeletonData"
			}));
			this.PlayerSpine.skeletonDataAsset = skeletonDataAsset;
			string initialSkinName = this.controller.IsNan ? "男" : "女";
			this.PlayerSpine.initialSkinName = initialSkinName;
			this.PlayerSpine.Initialize(true);
		}
	}

	// Token: 0x04000C63 RID: 3171
	public GameObject PlayerObj;

	// Token: 0x04000C64 RID: 3172
	public SkeletonAnimation PlayerSpine;

	// Token: 0x04000C65 RID: 3173
	public Animator Anim;

	// Token: 0x04000C66 RID: 3174
	[HideInInspector]
	public StaticSkillSeidJsonData9 NowDunShuSpineSeid;

	// Token: 0x04000C67 RID: 3175
	private MapPlayerController controller;

	// Token: 0x04000C68 RID: 3176
	private string nowSpineName = "";

	// Token: 0x04000C69 RID: 3177
	private float lastx;

	// Token: 0x04000C6A RID: 3178
	private static Vector3 fanXiang = new Vector3(-1f, 1f, 1f);

	// Token: 0x04000C6B RID: 3179
	private MeshRenderer meshRenderer;
}

using JSONClass;
using Spine.Unity;
using UnityEngine;

public class MapPlayerNormalShow : MonoBehaviour
{
	public GameObject PlayerObj;

	public SkeletonAnimation PlayerSpine;

	public Animator Anim;

	[HideInInspector]
	public StaticSkillSeidJsonData9 NowDunShuSpineSeid;

	private MapPlayerController controller;

	private string nowSpineName = "";

	private float lastx;

	private static Vector3 fanXiang = new Vector3(-1f, 1f, 1f);

	private MeshRenderer meshRenderer;

	public void Init(MapPlayerController controller)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		this.controller = controller;
		lastx = ((Component)this).transform.position.x;
	}

	private void Update()
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		if (controller.ShowType == MapPlayerShowType.普通人物 || controller.ShowType == MapPlayerShowType.遁术)
		{
			float num = ((Component)this).transform.position.x - lastx;
			if (Mathf.Abs(num) > 0.01f)
			{
				((Component)PlayerSpine).transform.localScale = ((num > 0f) ? Vector3.one : fanXiang);
				lastx = ((Component)this).transform.position.x;
			}
		}
	}

	public void Refresh()
	{
		if ((Object)(object)meshRenderer == (Object)null)
		{
			meshRenderer = ((Component)PlayerSpine).GetComponent<MeshRenderer>();
		}
		if (controller.IsOnSea)
		{
			((Renderer)meshRenderer).sortingOrder = 1;
		}
		switch (controller.ShowType)
		{
		case MapPlayerShowType.普通人物:
			PlayerObj.SetActive(true);
			LoadSpine("MapPlayerWalk");
			break;
		case MapPlayerShowType.遁术:
			PlayerObj.SetActive(true);
			LoadSpine(NowDunShuSpineSeid.Spine);
			break;
		default:
			PlayerObj.SetActive(false);
			break;
		}
	}

	public void LoadSpine(string spine)
	{
		if (nowSpineName != spine)
		{
			nowSpineName = spine;
			SkeletonDataAsset skeletonDataAsset = Resources.Load<SkeletonDataAsset>("Spine/MapPlayer/" + spine + "/" + spine + "_SkeletonData");
			((SkeletonRenderer)PlayerSpine).skeletonDataAsset = skeletonDataAsset;
			string initialSkinName = (controller.IsNan ? "男" : "女");
			((SkeletonRenderer)PlayerSpine).initialSkinName = initialSkinName;
			((SkeletonRenderer)PlayerSpine).Initialize(true);
		}
	}
}

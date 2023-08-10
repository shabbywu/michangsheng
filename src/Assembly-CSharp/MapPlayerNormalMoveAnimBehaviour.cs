using System;
using System.Collections;
using JSONClass;
using MoonSharp.Interpreter;
using Spine;
using Spine.Unity;
using UnityEngine;

public class MapPlayerNormalMoveAnimBehaviour : StateMachineBehaviour
{
	private SkeletonAnimation skeAnim;

	private ExposedList<Animation> spineAnims;

	private Animation nowSpineAnim;

	private float stateTimer;

	private float lastTime;

	private bool animLoopStart;

	private Script luaScript;

	private string onMoveEnterScript = "";

	private string onMoveExitScript = "";

	private string onLoopMoveEnterScript = "";

	private string onLoopMoveExitScript = "";

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		if (MapPlayerController.Inst.NormalShow.NowDunShuSpineSeid == null)
		{
			return;
		}
		luaScript = new Script();
		luaScript.Globals["CreateObj"] = new Action<string>(CreateObj);
		StaticSkillSeidJsonData9 nowDunShuSpineSeid = MapPlayerController.Inst.NormalShow.NowDunShuSpineSeid;
		onMoveEnterScript = nowDunShuSpineSeid.OnMoveEnter;
		onMoveExitScript = nowDunShuSpineSeid.OnMoveExit;
		onLoopMoveEnterScript = nowDunShuSpineSeid.OnLoopMoveEnter;
		onLoopMoveExitScript = nowDunShuSpineSeid.OnLoopMoveExit;
		skeAnim = GetSkeAnim(animator);
		spineAnims = ((SkeletonRenderer)skeAnim).skeletonDataAsset.GetSkeletonData(false).Animations;
		nowSpineAnim = null;
		Enumerator<Animation> enumerator = spineAnims.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Animation current = enumerator.Current;
				if (((AnimatorStateInfo)(ref stateInfo)).IsName(current.Name))
				{
					nowSpineAnim = current;
					break;
				}
			}
		}
		finally
		{
			((IDisposable)enumerator).Dispose();
		}
		stateTimer = 0f;
		lastTime = Time.time;
		animLoopStart = true;
		if (!string.IsNullOrWhiteSpace(onMoveEnterScript))
		{
			luaScript.DoString(onMoveEnterScript);
		}
	}

	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (MapPlayerController.Inst.NormalShow.NowDunShuSpineSeid != null && !string.IsNullOrWhiteSpace(onMoveExitScript))
		{
			luaScript.DoString(onMoveExitScript);
		}
	}

	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (MapPlayerController.Inst.NormalShow.NowDunShuSpineSeid == null || nowSpineAnim == null)
		{
			return;
		}
		if (animLoopStart)
		{
			animLoopStart = false;
			if (!string.IsNullOrWhiteSpace(onLoopMoveEnterScript))
			{
				luaScript.DoString(onLoopMoveEnterScript);
			}
		}
		stateTimer += Time.time - lastTime;
		lastTime = Time.time;
		if (stateTimer >= nowSpineAnim.Duration)
		{
			stateTimer -= nowSpineAnim.Duration;
			if (!string.IsNullOrWhiteSpace(onLoopMoveExitScript))
			{
				luaScript.DoString(onLoopMoveExitScript);
			}
			animLoopStart = true;
		}
	}

	private static SkeletonAnimation GetSkeAnim(Animator animator)
	{
		SkeletonAnimation val = ((Component)animator).GetComponent<SkeletonAnimation>();
		if ((Object)(object)val == (Object)null)
		{
			val = ((Component)animator).GetComponentInChildren<SkeletonAnimation>();
		}
		return val;
	}

	private IEnumerator DelaySetObjTransform(GameObject obj, Transform target)
	{
		obj.transform.position = new Vector3(999999f, 999999f, 0f);
		yield return (object)new WaitForEndOfFrame();
		obj.transform.position = target.position;
		obj.transform.rotation = target.rotation;
		obj.transform.localScale = target.localScale;
	}

	public void CreateObj(string path)
	{
		GameObject val = Resources.Load<GameObject>(path);
		if ((Object)(object)val == (Object)null)
		{
			Debug.LogError((object)("在遁术的绑定脚本中出现错误，CreateObj指令找不到预制体，path:" + path));
			return;
		}
		GameObject obj = Object.Instantiate<GameObject>(val);
		((MonoBehaviour)MapPlayerController.Inst).StartCoroutine(DelaySetObjTransform(obj, ((Component)MapPlayerController.Inst.NormalShow.Anim).transform));
	}
}

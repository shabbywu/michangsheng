using System;
using System.Collections;
using JSONClass;
using MoonSharp.Interpreter;
using Spine;
using Spine.Unity;
using UnityEngine;

// Token: 0x02000194 RID: 404
public class MapPlayerNormalMoveAnimBehaviour : StateMachineBehaviour
{
	// Token: 0x06001137 RID: 4407 RVA: 0x00067830 File Offset: 0x00065A30
	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (MapPlayerController.Inst.NormalShow.NowDunShuSpineSeid == null)
		{
			return;
		}
		this.luaScript = new Script();
		this.luaScript.Globals["CreateObj"] = new Action<string>(this.CreateObj);
		StaticSkillSeidJsonData9 nowDunShuSpineSeid = MapPlayerController.Inst.NormalShow.NowDunShuSpineSeid;
		this.onMoveEnterScript = nowDunShuSpineSeid.OnMoveEnter;
		this.onMoveExitScript = nowDunShuSpineSeid.OnMoveExit;
		this.onLoopMoveEnterScript = nowDunShuSpineSeid.OnLoopMoveEnter;
		this.onLoopMoveExitScript = nowDunShuSpineSeid.OnLoopMoveExit;
		this.skeAnim = MapPlayerNormalMoveAnimBehaviour.GetSkeAnim(animator);
		this.spineAnims = this.skeAnim.skeletonDataAsset.GetSkeletonData(false).Animations;
		this.nowSpineAnim = null;
		foreach (Animation animation in this.spineAnims)
		{
			if (stateInfo.IsName(animation.Name))
			{
				this.nowSpineAnim = animation;
				break;
			}
		}
		this.stateTimer = 0f;
		this.lastTime = Time.time;
		this.animLoopStart = true;
		if (!string.IsNullOrWhiteSpace(this.onMoveEnterScript))
		{
			this.luaScript.DoString(this.onMoveEnterScript, null, null);
		}
	}

	// Token: 0x06001138 RID: 4408 RVA: 0x00067980 File Offset: 0x00065B80
	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (MapPlayerController.Inst.NormalShow.NowDunShuSpineSeid == null)
		{
			return;
		}
		if (!string.IsNullOrWhiteSpace(this.onMoveExitScript))
		{
			this.luaScript.DoString(this.onMoveExitScript, null, null);
		}
	}

	// Token: 0x06001139 RID: 4409 RVA: 0x000679B8 File Offset: 0x00065BB8
	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (MapPlayerController.Inst.NormalShow.NowDunShuSpineSeid == null)
		{
			return;
		}
		if (this.nowSpineAnim != null)
		{
			if (this.animLoopStart)
			{
				this.animLoopStart = false;
				if (!string.IsNullOrWhiteSpace(this.onLoopMoveEnterScript))
				{
					this.luaScript.DoString(this.onLoopMoveEnterScript, null, null);
				}
			}
			this.stateTimer += Time.time - this.lastTime;
			this.lastTime = Time.time;
			if (this.stateTimer >= this.nowSpineAnim.Duration)
			{
				this.stateTimer -= this.nowSpineAnim.Duration;
				if (!string.IsNullOrWhiteSpace(this.onLoopMoveExitScript))
				{
					this.luaScript.DoString(this.onLoopMoveExitScript, null, null);
				}
				this.animLoopStart = true;
			}
		}
	}

	// Token: 0x0600113A RID: 4410 RVA: 0x00067A8C File Offset: 0x00065C8C
	private static SkeletonAnimation GetSkeAnim(Animator animator)
	{
		SkeletonAnimation skeletonAnimation = animator.GetComponent<SkeletonAnimation>();
		if (skeletonAnimation == null)
		{
			skeletonAnimation = animator.GetComponentInChildren<SkeletonAnimation>();
		}
		return skeletonAnimation;
	}

	// Token: 0x0600113B RID: 4411 RVA: 0x00067AB1 File Offset: 0x00065CB1
	private IEnumerator DelaySetObjTransform(GameObject obj, Transform target)
	{
		obj.transform.position = new Vector3(999999f, 999999f, 0f);
		yield return new WaitForEndOfFrame();
		obj.transform.position = target.position;
		obj.transform.rotation = target.rotation;
		obj.transform.localScale = target.localScale;
		yield break;
	}

	// Token: 0x0600113C RID: 4412 RVA: 0x00067AC8 File Offset: 0x00065CC8
	public void CreateObj(string path)
	{
		GameObject gameObject = Resources.Load<GameObject>(path);
		if (gameObject == null)
		{
			Debug.LogError("在遁术的绑定脚本中出现错误，CreateObj指令找不到预制体，path:" + path);
			return;
		}
		GameObject obj = Object.Instantiate<GameObject>(gameObject);
		MapPlayerController.Inst.StartCoroutine(this.DelaySetObjTransform(obj, MapPlayerController.Inst.NormalShow.Anim.transform));
	}

	// Token: 0x04000C58 RID: 3160
	private SkeletonAnimation skeAnim;

	// Token: 0x04000C59 RID: 3161
	private ExposedList<Animation> spineAnims;

	// Token: 0x04000C5A RID: 3162
	private Animation nowSpineAnim;

	// Token: 0x04000C5B RID: 3163
	private float stateTimer;

	// Token: 0x04000C5C RID: 3164
	private float lastTime;

	// Token: 0x04000C5D RID: 3165
	private bool animLoopStart;

	// Token: 0x04000C5E RID: 3166
	private Script luaScript;

	// Token: 0x04000C5F RID: 3167
	private string onMoveEnterScript = "";

	// Token: 0x04000C60 RID: 3168
	private string onMoveExitScript = "";

	// Token: 0x04000C61 RID: 3169
	private string onLoopMoveEnterScript = "";

	// Token: 0x04000C62 RID: 3170
	private string onLoopMoveExitScript = "";
}

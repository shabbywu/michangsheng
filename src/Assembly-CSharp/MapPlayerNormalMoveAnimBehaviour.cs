using System;
using System.Collections;
using JSONClass;
using MoonSharp.Interpreter;
using Spine;
using Spine.Unity;
using UnityEngine;

// Token: 0x02000284 RID: 644
public class MapPlayerNormalMoveAnimBehaviour : StateMachineBehaviour
{
	// Token: 0x060013BA RID: 5050 RVA: 0x000B5DE0 File Offset: 0x000B3FE0
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

	// Token: 0x060013BB RID: 5051 RVA: 0x00012723 File Offset: 0x00010923
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

	// Token: 0x060013BC RID: 5052 RVA: 0x000B5F30 File Offset: 0x000B4130
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

	// Token: 0x060013BD RID: 5053 RVA: 0x000B6004 File Offset: 0x000B4204
	private static SkeletonAnimation GetSkeAnim(Animator animator)
	{
		SkeletonAnimation skeletonAnimation = animator.GetComponent<SkeletonAnimation>();
		if (skeletonAnimation == null)
		{
			skeletonAnimation = animator.GetComponentInChildren<SkeletonAnimation>();
		}
		return skeletonAnimation;
	}

	// Token: 0x060013BE RID: 5054 RVA: 0x00012758 File Offset: 0x00010958
	private IEnumerator DelaySetObjTransform(GameObject obj, Transform target)
	{
		obj.transform.position = new Vector3(999999f, 999999f, 0f);
		yield return new WaitForEndOfFrame();
		obj.transform.position = target.position;
		obj.transform.rotation = target.rotation;
		obj.transform.localScale = target.localScale;
		yield break;
	}

	// Token: 0x060013BF RID: 5055 RVA: 0x000B602C File Offset: 0x000B422C
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

	// Token: 0x04000F58 RID: 3928
	private SkeletonAnimation skeAnim;

	// Token: 0x04000F59 RID: 3929
	private ExposedList<Animation> spineAnims;

	// Token: 0x04000F5A RID: 3930
	private Animation nowSpineAnim;

	// Token: 0x04000F5B RID: 3931
	private float stateTimer;

	// Token: 0x04000F5C RID: 3932
	private float lastTime;

	// Token: 0x04000F5D RID: 3933
	private bool animLoopStart;

	// Token: 0x04000F5E RID: 3934
	private Script luaScript;

	// Token: 0x04000F5F RID: 3935
	private string onMoveEnterScript = "";

	// Token: 0x04000F60 RID: 3936
	private string onMoveExitScript = "";

	// Token: 0x04000F61 RID: 3937
	private string onLoopMoveEnterScript = "";

	// Token: 0x04000F62 RID: 3938
	private string onLoopMoveExitScript = "";
}

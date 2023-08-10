using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace YSGame.Fight;

public class UIFightMoveLingQi : MonoBehaviour
{
	private static List<Color> pColors = new List<Color>
	{
		new Color(1f, 0.8862745f, 0.02745098f),
		new Color(1f / 3f, 1f, 0.02745098f),
		new Color(0.02745098f, 11f / 15f, 1f),
		new Color(1f, 5f / 51f, 0.02745098f),
		new Color(0.36078432f, 0.20392157f, 0.0627451f),
		new Color(0.2784314f, 0f, 1f / 3f)
	};

	public particleAttractorLinear PAL;

	public ParticleSystem Particle1;

	public ParticleSystem Particle2;

	public Image LingQiImage;

	public Transform ParticleEnd;

	private Action OnMoveEnd;

	private float cd;

	[HideInInspector]
	public bool IsStart;

	private bool isShowAnim;

	public LingQiType LastLingQiType;

	public int LastLingQiCount;

	private float deadTime;

	private float particleAnimTime = 0.66f;

	private void Update()
	{
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		if (IsStart)
		{
			cd -= Time.deltaTime;
			if (isShowAnim)
			{
				float num = Mathf.Lerp(0f, 1f, Mathf.Clamp(1f - cd, 0f, 1f));
				((Graphic)LingQiImage).color = new Color(1f, 1f, 1f, num);
				((Component)LingQiImage).transform.localScale = new Vector3(num, num, num);
			}
			if (cd < 0f)
			{
				OnEnd();
			}
		}
		else
		{
			deadTime -= Time.deltaTime;
			if (deadTime < 0f && UIFightPanel.Inst.MoveLingQiList.Count > 10)
			{
				UIFightPanel.Inst.MoveLingQiList.Remove(this);
				Object.Destroy((Object)(object)((Component)this).gameObject);
			}
		}
	}

	public void SetData(LingQiType lingQiType, Vector3 start, Vector3 end, Action callback = null, int count = 1, bool showAnim = false, float particleSpeed = 5f)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		if (lingQiType == LingQiType.Count)
		{
			Debug.LogError((object)"灵气动画异常：尝试设置空灵气动画");
			return;
		}
		LastLingQiType = lingQiType;
		LastLingQiCount = count;
		Particle2.startColor = pColors[(int)lingQiType];
		EmissionModule emission = Particle2.emission;
		((EmissionModule)(ref emission)).rateOverTime = MinMaxCurve.op_Implicit((float)Mathf.Clamp(count * 5, 0, 100));
		((Component)this).transform.position = new Vector3(start.x, start.y, start.z - 5f);
		ParticleEnd.position = end;
		ParticleEnd.localPosition = new Vector3(ParticleEnd.localPosition.x, ParticleEnd.localPosition.y, 0f);
		isShowAnim = showAnim;
		if (isShowAnim)
		{
			LingQiImage.sprite = UIFightPanel.Inst.LingQiImageDatas[(int)lingQiType].Normal;
			((Component)LingQiImage).transform.position = ParticleEnd.position;
		}
		OnMoveEnd = callback;
		cd = 0.2f;
		PAL.speed = particleSpeed;
		if (particleSpeed >= 0f)
		{
			MainModule main = Particle2.main;
			MinMaxCurve startLifetime = ((MainModule)(ref main)).startLifetime;
			((MinMaxCurve)(ref startLifetime)).constant = particleAnimTime;
			((MainModule)(ref main)).startLifetime = startLifetime;
		}
		else
		{
			MainModule main2 = Particle2.main;
			MinMaxCurve startLifetime2 = ((MainModule)(ref main2)).startLifetime;
			((MinMaxCurve)(ref startLifetime2)).constant = particleAnimTime / 2f;
			((MainModule)(ref main2)).startLifetime = startLifetime2;
		}
		Particle1.Play();
		IsStart = true;
	}

	private void OnEnd()
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		IsStart = false;
		deadTime = 10f;
		((Graphic)LingQiImage).color = new Color(1f, 1f, 1f, 0f);
		((Component)LingQiImage).transform.localScale = Vector3.zero;
		if (OnMoveEnd != null)
		{
			OnMoveEnd();
		}
	}
}

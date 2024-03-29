using KBEngine;

public static class NPCTalkCmdHelper
{
	public static string ReplaceTalkWord(this string str, UINPCData npc)
	{
		Avatar player = Tools.instance.getPlayer();
		str = str.ReplaceTalkWord();
		string newValue = "前辈";
		string newValue2 = "道友";
		if (player.menPai != 0 && npc.MenPai == player.menPai)
		{
			newValue2 = ((player.getLevelType() == 5 && npc.BigLevel < 5) ? "师祖" : ((player.Sex != 1) ? ((player.level >= npc.Level) ? "师姐" : "师妹") : ((player.level >= npc.Level) ? "师兄" : "师弟")));
		}
		if (PlayerEx.IsBrother(npc.ID))
		{
			if (player.Sex == 1)
			{
				newValue2 = "兄弟";
				newValue = "兄弟";
			}
			else
			{
				newValue2 = "姑娘";
				newValue = "姑娘";
			}
		}
		if (PlayerEx.IsTheather(npc.ID))
		{
			newValue2 = player.lastName;
			newValue = player.lastName;
		}
		if (PlayerEx.IsDaoLv(npc.ID))
		{
			if (player.DaoLvChengHu.HasField(npc.ID.ToString()))
			{
				newValue2 = player.DaoLvChengHu[npc.ID.ToString()].Str;
				newValue = player.DaoLvChengHu[npc.ID.ToString()].Str;
			}
			else
			{
				newValue2 = player.lastName;
				newValue = player.lastName;
			}
		}
		if (PlayerEx.IsTuDi(npc.ID))
		{
			newValue2 = player.lastName;
			newValue = player.lastName;
		}
		str = str.Replace("{daoyou}", newValue2);
		str = str.Replace("{qianbei}", newValue);
		string newValue3 = "小友";
		if (player.menPai != 0 && npc.MenPai == player.menPai)
		{
			newValue3 = "师侄";
		}
		if (PlayerEx.IsBrother(npc.ID))
		{
			newValue3 = ((player.Sex != 1) ? "姑娘" : "兄弟");
		}
		if (PlayerEx.IsTheather(npc.ID))
		{
			newValue3 = player.lastName;
		}
		if (PlayerEx.IsDaoLv(npc.ID) && player.DaoLvChengHu.HasField(npc.ID.ToString()))
		{
			newValue3 = player.DaoLvChengHu[npc.ID.ToString()].Str;
		}
		str = str.Replace("{xiaoyou}", newValue3);
		if (UINPCJiaoHu.Inst.JiaoHuItemID > 0)
		{
			str = str.Replace("{item}", UINPCJiaoHu.Inst.JiaoHuItemID.ItemJson()["name"].Str);
		}
		if (UINPCJiaoHu.Inst.ZengLiArg != null && UINPCJiaoHu.Inst.ZengLiArg.Item != null)
		{
			str = str.Replace("{item}", UINPCJiaoHu.Inst.ZengLiArg.Item.itemName);
		}
		if (UINPCJiaoHu.Inst.WeiXieArg != null)
		{
			str = str.Replace("{shengwang}", UINPCJiaoHu.Inst.WeiXieArg.ShengWang);
			str = str.Replace("{diyu}", UINPCJiaoHu.Inst.WeiXieArg.DiYu);
			str = str.Replace("{friendid}", UINPCJiaoHu.Inst.WeiXieArg.FriendName);
			str = str.Replace("{friendlevel}", UINPCJiaoHu.Inst.WeiXieArg.FriendBigLevel);
			str = str.Replace("{SpecRel}", UINPCJiaoHu.Inst.WeiXieArg.SpecRel);
		}
		str = str.Replace("{miji}", UINPCJiaoHu.Inst.QingJiaoName);
		return str;
	}

	public static string ReplaceTalkWord(this string str)
	{
		Avatar player = Tools.instance.getPlayer();
		str = str.Replace("{gongzi}", (player.Sex == 1) ? "公子" : "姑娘");
		str = str.Replace("{xiongdi}", (player.Sex == 1) ? "兄弟" : "姑娘");
		str = str.Replace("{shidi}", (player.Sex == 1) ? "师弟" : "师妹");
		str = str.Replace("{shixiong}", (player.Sex == 1) ? "师兄" : "师姐");
		str = str.Replace("{xiaozi}", (player.Sex == 1) ? "小子" : "丫头");
		str = str.Replace("{ta}", (player.Sex == 1) ? "他" : "她");
		str = str.Replace("{FirstName}", player.firstName);
		str = str.Replace("{LastName}", player.lastName);
		return str;
	}
}

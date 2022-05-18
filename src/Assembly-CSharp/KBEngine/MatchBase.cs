using System;
using System.Collections.Generic;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000F5B RID: 3931
	public abstract class MatchBase : Entity
	{
		// Token: 0x06005E68 RID: 24168 RVA: 0x00040112 File Offset: 0x0003E312
		public MatchBase()
		{
		}

		// Token: 0x06005E69 RID: 24169 RVA: 0x000042DD File Offset: 0x000024DD
		public override void onComponentsEnterworld()
		{
		}

		// Token: 0x06005E6A RID: 24170 RVA: 0x000042DD File Offset: 0x000024DD
		public override void onComponentsLeaveworld()
		{
		}

		// Token: 0x06005E6B RID: 24171 RVA: 0x000422A7 File Offset: 0x000404A7
		public override void onGetBase()
		{
			this.baseEntityCall = new EntityBaseEntityCall_MatchBase(this.id, this.className);
		}

		// Token: 0x06005E6C RID: 24172 RVA: 0x000422C0 File Offset: 0x000404C0
		public override void onGetCell()
		{
			this.cellEntityCall = new EntityCellEntityCall_MatchBase(this.id, this.className);
		}

		// Token: 0x06005E6D RID: 24173 RVA: 0x000422D9 File Offset: 0x000404D9
		public override void onLoseCell()
		{
			this.cellEntityCall = null;
		}

		// Token: 0x06005E6E RID: 24174 RVA: 0x000422E2 File Offset: 0x000404E2
		public override EntityCall getBaseEntityCall()
		{
			return this.baseEntityCall;
		}

		// Token: 0x06005E6F RID: 24175 RVA: 0x000422EA File Offset: 0x000404EA
		public override EntityCall getCellEntityCall()
		{
			return this.cellEntityCall;
		}

		// Token: 0x06005E70 RID: 24176 RVA: 0x000042DD File Offset: 0x000024DD
		public override void attachComponents()
		{
		}

		// Token: 0x06005E71 RID: 24177 RVA: 0x000042DD File Offset: 0x000024DD
		public override void detachComponents()
		{
		}

		// Token: 0x06005E72 RID: 24178 RVA: 0x00261F40 File Offset: 0x00260140
		public override void onRemoteMethodCall(MemoryStream stream)
		{
			ScriptModule scriptModule = EntityDef.moduledefs["Match"];
			ushort num;
			if (scriptModule.usePropertyDescrAlias)
			{
				num = (ushort)stream.readUint8();
			}
			else
			{
				num = stream.readUint16();
			}
			ushort key;
			if (scriptModule.useMethodDescrAlias)
			{
				key = (ushort)stream.readUint8();
			}
			else
			{
				key = stream.readUint16();
			}
			if (num == 0)
			{
				Method method = scriptModule.idmethods[key];
				ushort methodUtype = method.methodUtype;
				return;
			}
			ushort properUtype = scriptModule.idpropertys[num].properUtype;
		}

		// Token: 0x06005E73 RID: 24179 RVA: 0x00261FC0 File Offset: 0x002601C0
		public override void onUpdatePropertys(MemoryStream stream)
		{
			ScriptModule scriptModule = EntityDef.moduledefs["Match"];
			Dictionary<ushort, Property> idpropertys = scriptModule.idpropertys;
			while (stream.length() > 0U)
			{
				ushort num;
				ushort key;
				if (scriptModule.usePropertyDescrAlias)
				{
					num = (ushort)stream.readUint8();
					key = (ushort)stream.readUint8();
				}
				else
				{
					num = stream.readUint16();
					key = stream.readUint16();
				}
				if (num != 0)
				{
					ushort properUtype = idpropertys[num].properUtype;
					return;
				}
				Property property = idpropertys[key];
				switch (property.properUtype)
				{
				case 40000:
				{
					Vector3 position = this.position;
					this.position = stream.readVector3();
					if (property.isBase())
					{
						if (this.inited)
						{
							this.onPositionChanged(position);
						}
					}
					else if (this.inWorld)
					{
						this.onPositionChanged(position);
					}
					break;
				}
				case 40001:
				{
					Vector3 direction = this.direction;
					this.direction = stream.readVector3();
					if (property.isBase())
					{
						if (this.inited)
						{
							this.onDirectionChanged(direction);
						}
					}
					else if (this.inWorld)
					{
						this.onDirectionChanged(direction);
					}
					break;
				}
				case 40002:
					stream.readUint32();
					break;
				}
			}
		}

		// Token: 0x06005E74 RID: 24180 RVA: 0x002620F0 File Offset: 0x002602F0
		public override void callPropertysSetMethods()
		{
			Dictionary<ushort, Property> idpropertys = EntityDef.moduledefs["Match"].idpropertys;
			Vector3 direction = this.direction;
			Property property = idpropertys[2];
			if (property.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onDirectionChanged(direction);
				}
			}
			else if (this.inWorld && (!property.isOwnerOnly() || base.isPlayer()))
			{
				this.onDirectionChanged(direction);
			}
			Vector3 position = this.position;
			Property property2 = idpropertys[1];
			if (property2.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onPositionChanged(position);
					return;
				}
			}
			else if (this.inWorld && (!property2.isOwnerOnly() || base.isPlayer()))
			{
				this.onPositionChanged(position);
			}
		}

		// Token: 0x04005B1B RID: 23323
		public EntityBaseEntityCall_MatchBase baseEntityCall;

		// Token: 0x04005B1C RID: 23324
		public EntityCellEntityCall_MatchBase cellEntityCall;
	}
}

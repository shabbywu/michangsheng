using System;
using System.Collections.Generic;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000FED RID: 4077
	public abstract class ShopBase : Entity
	{
		// Token: 0x06006073 RID: 24691 RVA: 0x00040112 File Offset: 0x0003E312
		public ShopBase()
		{
		}

		// Token: 0x06006074 RID: 24692 RVA: 0x000042DD File Offset: 0x000024DD
		public override void onComponentsEnterworld()
		{
		}

		// Token: 0x06006075 RID: 24693 RVA: 0x000042DD File Offset: 0x000024DD
		public override void onComponentsLeaveworld()
		{
		}

		// Token: 0x06006076 RID: 24694 RVA: 0x00042FAC File Offset: 0x000411AC
		public override void onGetBase()
		{
			this.baseEntityCall = new EntityBaseEntityCall_ShopBase(this.id, this.className);
		}

		// Token: 0x06006077 RID: 24695 RVA: 0x00042FC5 File Offset: 0x000411C5
		public override void onGetCell()
		{
			this.cellEntityCall = new EntityCellEntityCall_ShopBase(this.id, this.className);
		}

		// Token: 0x06006078 RID: 24696 RVA: 0x00042FDE File Offset: 0x000411DE
		public override void onLoseCell()
		{
			this.cellEntityCall = null;
		}

		// Token: 0x06006079 RID: 24697 RVA: 0x00042FE7 File Offset: 0x000411E7
		public override EntityCall getBaseEntityCall()
		{
			return this.baseEntityCall;
		}

		// Token: 0x0600607A RID: 24698 RVA: 0x00042FEF File Offset: 0x000411EF
		public override EntityCall getCellEntityCall()
		{
			return this.cellEntityCall;
		}

		// Token: 0x0600607B RID: 24699 RVA: 0x000042DD File Offset: 0x000024DD
		public override void attachComponents()
		{
		}

		// Token: 0x0600607C RID: 24700 RVA: 0x000042DD File Offset: 0x000024DD
		public override void detachComponents()
		{
		}

		// Token: 0x0600607D RID: 24701 RVA: 0x002695E0 File Offset: 0x002677E0
		public override void onRemoteMethodCall(MemoryStream stream)
		{
			ScriptModule scriptModule = EntityDef.moduledefs["Shop"];
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

		// Token: 0x0600607E RID: 24702 RVA: 0x00269660 File Offset: 0x00267860
		public override void onUpdatePropertys(MemoryStream stream)
		{
			ScriptModule scriptModule = EntityDef.moduledefs["Shop"];
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

		// Token: 0x0600607F RID: 24703 RVA: 0x00269790 File Offset: 0x00267990
		public override void callPropertysSetMethods()
		{
			Dictionary<ushort, Property> idpropertys = EntityDef.moduledefs["Shop"].idpropertys;
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

		// Token: 0x04005BC2 RID: 23490
		public EntityBaseEntityCall_ShopBase baseEntityCall;

		// Token: 0x04005BC3 RID: 23491
		public EntityCellEntityCall_ShopBase cellEntityCall;
	}
}

using System;
using System.Collections.Generic;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000B39 RID: 2873
	public abstract class CDKUserUsedBase : Entity
	{
		// Token: 0x060050B2 RID: 20658 RVA: 0x00220682 File Offset: 0x0021E882
		public CDKUserUsedBase()
		{
		}

		// Token: 0x060050B3 RID: 20659 RVA: 0x00004095 File Offset: 0x00002295
		public override void onComponentsEnterworld()
		{
		}

		// Token: 0x060050B4 RID: 20660 RVA: 0x00004095 File Offset: 0x00002295
		public override void onComponentsLeaveworld()
		{
		}

		// Token: 0x060050B5 RID: 20661 RVA: 0x00220945 File Offset: 0x0021EB45
		public override void onGetBase()
		{
			this.baseEntityCall = new EntityBaseEntityCall_CDKUserUsedBase(this.id, this.className);
		}

		// Token: 0x060050B6 RID: 20662 RVA: 0x0022095E File Offset: 0x0021EB5E
		public override void onGetCell()
		{
			this.cellEntityCall = new EntityCellEntityCall_CDKUserUsedBase(this.id, this.className);
		}

		// Token: 0x060050B7 RID: 20663 RVA: 0x00220977 File Offset: 0x0021EB77
		public override void onLoseCell()
		{
			this.cellEntityCall = null;
		}

		// Token: 0x060050B8 RID: 20664 RVA: 0x00220980 File Offset: 0x0021EB80
		public override EntityCall getBaseEntityCall()
		{
			return this.baseEntityCall;
		}

		// Token: 0x060050B9 RID: 20665 RVA: 0x00220988 File Offset: 0x0021EB88
		public override EntityCall getCellEntityCall()
		{
			return this.cellEntityCall;
		}

		// Token: 0x060050BA RID: 20666 RVA: 0x00004095 File Offset: 0x00002295
		public override void attachComponents()
		{
		}

		// Token: 0x060050BB RID: 20667 RVA: 0x00004095 File Offset: 0x00002295
		public override void detachComponents()
		{
		}

		// Token: 0x060050BC RID: 20668 RVA: 0x00220990 File Offset: 0x0021EB90
		public override void onRemoteMethodCall(MemoryStream stream)
		{
			ScriptModule scriptModule = EntityDef.moduledefs["CDKUserUsed"];
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

		// Token: 0x060050BD RID: 20669 RVA: 0x00220A10 File Offset: 0x0021EC10
		public override void onUpdatePropertys(MemoryStream stream)
		{
			ScriptModule scriptModule = EntityDef.moduledefs["CDKUserUsed"];
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

		// Token: 0x060050BE RID: 20670 RVA: 0x00220B40 File Offset: 0x0021ED40
		public override void callPropertysSetMethods()
		{
			Dictionary<ushort, Property> idpropertys = EntityDef.moduledefs["CDKUserUsed"].idpropertys;
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

		// Token: 0x04004F79 RID: 20345
		public EntityBaseEntityCall_CDKUserUsedBase baseEntityCall;

		// Token: 0x04004F7A RID: 20346
		public EntityCellEntityCall_CDKUserUsedBase cellEntityCall;
	}
}

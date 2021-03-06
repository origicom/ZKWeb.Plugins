﻿using System;
using ZKWeb.Plugins.Common.Admin.src.Domain.Extensions;
using ZKWeb.Plugins.Common.Admin.src.Domain.Services;
using ZKWeb.Plugins.Common.Base.src.Domain.Services;
using ZKWeb.Plugins.Common.Base.src.Domain.Services.Bases;
using ZKWeb.Plugins.Shopping.Order.src.Domain.Entities;
using ZKWeb.Plugins.Shopping.Order.src.Domain.Enums;
using ZKWebStandard.Ioc;

namespace ZKWeb.Plugins.Shopping.Order.src.Domain.Services {
	/// <summary>
	/// 订单留言管理器
	/// </summary>
	[ExportMany, SingletonReuse]
	public class OrderCommentManager : DomainServiceBase<OrderComment, Guid> {
		/// <summary>
		/// 添加订单留言
		/// </summary>
		/// <param name="order">订单</param>
		/// <param name="userId">用户Id</param>
		/// <param name="side">买家或卖家留言</param>
		/// <param name="contents">留言内容</param>
		public void AddComment(SellerOrder order, Guid? userId, OrderCommentSide side, string contents) {
			var userManager = Application.Ioc.Resolve<UserManager>();
			var comment = new OrderComment() {
				Order = order,
				Owner = userId.HasValue ? userManager.Get(userId.Value) : null,
				Side = side,
				Contents = contents
			};
			Save(ref comment);
		}
	}
}

﻿using System.Collections.Generic;
using ZKWeb.Localize;
using ZKWeb.Plugins.Common.Admin.src.Controllers.Interfaces;
using ZKWeb.Plugins.Common.Base.src.UIComponents.MenuItems;
using ZKWeb.Plugins.Common.Base.src.UIComponents.MenuItems.Extensions;

namespace ZKWeb.Plugins.Common.Admin.src.UIComponents.MenuItems.Extensions {
	/// <summary>
	/// 菜单项列表的扩展函数
	/// </summary>
	public static class MenuItemsExtensions {
		/// <summary>
		/// 添加回收站选项
		/// </summary>
		/// <param name="items">菜单项列表</param>
		/// <param name="name">名称</param>
		/// <param name="iconClass">图标的Css类</param>
		public static void AddRecycleBin(
			this List<MenuItem> items, string name = null, string iconClass = null) {
			items.AddItemForClickEvent(
				name ?? new T("Recycle Bin"),
				iconClass ?? "fa fa-recycle",
				"$(this).closestAjaxTable().toggleRecycleBin(this)");
		}

		/// <summary>
		/// 添加编辑项
		/// 点击后弹出编辑右键选中数据的模态框
		/// </summary>
		/// <param name="items">菜单项列表</param>
		/// <param name="typeName">类型名称</param>
		/// <param name="editUrl">编辑使用的Url</param>
		/// <param name="name">显示名称，不指定时使用默认值</param>
		/// <param name="iconClass">图标的Css类，不指定时使用默认值</param>
		/// <param name="titleTemplate">标题的模板，格式是underscore.js的格式，参数传入row</param>
		/// <param name="urlTemplate">编辑Url的模板，格式是underscore.js的格式，参数传入row</param>
		/// <param name="dialogParameters">弹出框的参数，不指定时使用默认值</param>
		public static void AddEditAction(
			this List<MenuItem> items, string typeName, string editUrl,
			string name = null, string iconClass = null,
			string titleTemplate = null, string urlTemplate = null, object dialogParameters = null) {
			items.AddRemoteModalForSelectedRow(
				name ?? new T("View"),
				iconClass ?? "fa fa-edit",
				titleTemplate ?? new T("Edit {0}", new T(typeName)),
				urlTemplate ?? (editUrl + "?id=<%-row.Id%>"),
				dialogParameters);
		}

		/// <summary>
		/// 添加编辑项
		/// 点击后弹出编辑右键选中数据的模态框
		/// 根据增删查改页面的控制器自动生成，各个参数如不指定则使用默认值
		/// </summary>
		/// <typeparam name="TCrudController">控制器的类型</typeparam>
		public static void AddEditActionFor<TCrudController>(
			this List<MenuItem> items, string name = null, string iconClass = null,
			string titleTemplate = null, string urlTemplate = null, object dialogParameters = null)
			where TCrudController : class, ICrudController, new() {
			var app = new TCrudController();
			items.AddEditAction(app.EntityTypeName, app.EditUrl,
				name, iconClass, titleTemplate, urlTemplate, dialogParameters);
		}

		/// <summary>
		/// 添加添加项
		/// 点击后弹出添加数据的模态框
		/// </summary>
		/// <param name="items">菜单项列表</param>
		/// <param name="typeName">类型名称</param>
		/// <param name="addUrl">添加使用的Url</param>
		/// <param name="name">显示名称，不指定时使用默认值</param>
		/// <param name="iconClass">图标的Css类，不指定时使用默认值</param>
		/// <param name="title">标题，不指定时使用默认值</param>
		/// <param name="url">添加使用的Url，和addUrl是同一个参数</param>
		/// <param name="dialogParameters">弹出框的参数，不指定时使用默认值</param>
		public static void AddAddAction(
			this List<MenuItem> items, string typeName, string addUrl,
			string name = null, string iconClass = null,
			string title = null, string url = null, object dialogParameters = null) {
			var defaultName = new T("Add {0}", new T(typeName));
			items.AddRemoteModalForAjaxTable(
				name ?? defaultName,
				iconClass ?? "fa fa-plus",
				title ?? defaultName,
				url ?? addUrl,
				dialogParameters);
		}

		/// <summary>
		/// 添加添加项
		/// 点击后弹出添加数据的模态框
		/// 根据增删查改页面的控制器自动生成，各个参数如不指定则使用默认值
		/// </summary>
		/// <typeparam name="TCrudController">控制器的类型</typeparam>
		public static void AddAddActionFor<TCrudController>(
			this List<MenuItem> items, string name = null, string iconClass = null,
			string title = null, string url = null, object dialogParameters = null)
			where TCrudController : class, ICrudController, new() {
			var app = new TCrudController();
			items.AddAddAction(app.EntityTypeName, app.AddUrl,
				name, iconClass, title, url, dialogParameters);
		}
	}
}

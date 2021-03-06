﻿using ZKWeb.Plugin;
using ZKWeb.Templating.DynamicContents;
using ZKWebStandard.Ioc;

namespace ZKWeb.Plugins.Shopping.Product.src {
	/// <summary>
	/// 载入插件时的处理
	/// </summary>
	[ExportMany, SingletonReuse]
	public class Plugin : IPlugin {
		/// <summary>
		/// 初始化
		/// </summary>
		public Plugin() {
			// 注册默认模块
			var areaManager = Application.Ioc.Resolve<TemplateAreaManager>();
			// 商品详情页
			areaManager.GetArea("product_details").DefaultWidgets.Add("shopping.product.widgets/product_details");
			areaManager.GetArea("product_gallery").DefaultWidgets.Add("shopping.product.widgets/product_gallery");
			areaManager.GetArea("product_sales_info").DefaultWidgets.Add("shopping.product.widgets/product_sales_info");
			// 商品列表页
			var productListFilter = areaManager.GetArea("product_list_filter");
			var productListTable = areaManager.GetArea("product_list_table");
			productListFilter.DefaultWidgets.Add("shopping.product.widgets/product_filter_by_class");
			productListFilter.DefaultWidgets.Add("shopping.product.widgets/product_filter_by_tag");
			productListFilter.DefaultWidgets.Add("shopping.product.widgets/product_filter_by_price_and_order");
			productListTable.DefaultWidgets.Add("shopping.product.widgets/product_list_table");
			// 商品搜索框
			areaManager.GetArea("header_logobar").DefaultWidgets.Add("shopping.product.widgets/product_search_bar");
			// 商品导航栏
			areaManager.GetArea("header_menubar").DefaultWidgets.Insert(
				0, new TemplateWidget("shopping.product.widgets/product_nav_menu"));
		}
	}
}

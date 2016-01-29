﻿using DryIocAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZKWeb.Plugins.Common.Region.src.Model;

namespace ZKWeb.Plugins.Common.Region.src.Countries {
	/// <summary>
	/// 日本
	/// </summary>
	[ExportMany, SingletonReuse]
	public class JP : Country {
		public override string Name { get { return "JP"; } }
	}
}

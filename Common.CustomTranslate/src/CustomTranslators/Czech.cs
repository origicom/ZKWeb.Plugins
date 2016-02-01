﻿using DryIocAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZKWeb.Plugins.Common.CustomTranslate.src.CustomTranslators {
	/// <summary>
	/// 捷克语
	/// </summary>
	[ExportMany, SingletonReuse]
	public class Czech : CustomTranslator {
		public override string Name { get { return "cs-CZ"; } }
	}
}
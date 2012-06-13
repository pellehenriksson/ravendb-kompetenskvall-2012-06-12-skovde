using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Raven.Abstractions.Data;

namespace RavenDbPresentation.Tests.Demo2.Patching
{
	[TestFixture]
	public class PatchingSamples : RavenTestBase
	{
		[Test]
		public void Add_New_Tag_To_Employee_Without_Loading_The_Document()
		{
			var pr = new PatchRequest
			{
			    Type = PatchCommandType.Add,
			    Name = "Tags",
			    Value = "Hotbot"
			};
			
			Store.DatabaseCommands.Patch("employees/10", new[] {pr});
		}
	}
}

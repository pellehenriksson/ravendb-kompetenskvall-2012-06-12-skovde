using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RavenDbPresentation.Tests
{
	public class Sandbox : RavenTestBase
	{
		[Test]
		public void Run_Forrest_Run()
		{
			using (var session = Store.OpenSession())
			{
				var n = new SomeOtherDocument
				        	{
				        		Name
				        			= "dhqwjkdhquwdhku"
				        	};

				session.Store(n);

				n.Name = "111111111111";


				session.SaveChanges();
			}
		}

		public class SomeOtherDocument
		{
			public string Name { get; set; }
			public int Age { get; set; }
		}
	}
}

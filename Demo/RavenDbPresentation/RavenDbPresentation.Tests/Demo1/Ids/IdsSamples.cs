using System;
using NUnit.Framework;

namespace RavenDbPresentation.Tests.Ids
{
	// 1. Raven will set the id when calling Store();
	
	[TestFixture]
	public class IdsSamples : RavenTestBase
	{
		// GETS THE ID: SomeClassWithNoIdDefineds/1
		[Test]
		public void Let_Raven_Set_The_Id()
		{
			using (var session = Store.OpenSession())
			{
				var c = new SomeClassWithNoIdDefined { Name = "pelle" };
				session.Store(c);
				session.SaveChanges();
			}
		}

		[Test]
		public void Let_Raven_Set_The_Id_On_Class_That_Has_Id()
		{
			using (var session = Store.OpenSession())
			{
				var c = new SomeClassWithId { Name = "pelle" };
				session.Store(c);
				session.SaveChanges();

				Console.Out.WriteLine(c.Id);
			}
		}

		[Test]
		public void Assign_The_Key_Yourself()
		{
			using (var session = Store.OpenSession())
			{
				var c = new SomeClassWithNaturalKey { Id = "pellehenriksson@test.nu", Name = "pelle" };
				session.Store(c);
				session.SaveChanges();
			}
		}

		[Test]
		public void Use_An_GUID_As_The_Key()
		{
			using (var session = Store.OpenSession())
			{
				var c = new SomeClassWithGuidAsKey { Name = "pelle" };
				session.Store(c);
				session.SaveChanges();
			}
		}
	}
	
	public class SomeClassWithNoIdDefined
	{
		public string Name { get; set; }
	}

	public class SomeClassWithId
	{
		public string Id { get; set; }
		public string Name { get; set; }
	}

	public class SomeClassWithNaturalKey
	{
		public string Id { get; set; }
		public string Name { get; set; }
	}

	public class SomeClassWithGuidAsKey
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
	}
}

using System.Collections.Generic;
using System.Dynamic;
using NUnit.Framework;
using RavenDbPresentation.Core.Documents;

namespace RavenDbPresentation.Tests.BasicCrudOperations
{
	[TestFixture]
	public class CrudSamples : RavenTestBase
	{
		//
		// ADD A NEW DOCUMENT
		// 
		[Test]
		public void Add_New_Item()
		{
			using (var session = Store.OpenSession())
			{
				var contact = new Employee
				{
				    Firstname = "Nisse",
				    Lastname = "Hult",
				    Email = "nisse@hult.nu",
				    City = "Moheda",
				    Tags = new List<string>
				    {
				        "Jetbrains",
				        "Hotbot"
				    }
				};

				session.Store(contact);
				session.SaveChanges();
			}
		}
		
		//
		// MODIFY AN EXISTING DOCUMENT
		//
		[Test]
		public void Update_Existing_Item()
		{
			using (var session = Store.OpenSession())
			{
				var contact = session.Load<Employee>("employees/199");

				contact.Firstname = "boom shakalak";
				session.SaveChanges();
			}
		}

		//
		// DELETE A DOCUMENT
		//
		[Test]
		public void Delete_Item()
		{
			using (var session = Store.OpenSession())
			{
				var contact = session.Load<Employee>("employees/198");
				session.Delete(contact);

				session.SaveChanges();
			}
		}

		//
		// STORE A DYNAMIC OBJECT
		//
		[Test]
		public void Store_Any_Crazy_Dynamic_Object()
		{
			using (var session = Store.OpenSession())
			{
				dynamic obj = new ExpandoObject();

				obj.Id = 666;
				obj.Name = "Hellö wörld";
				obj.SomeValue = 12.34m;
				obj.Companies = new List<Company>
				{
				    new Company { Name = "un" },
					new Company { Name = "deux" },
					new Company { Name = "trois" },
					new Company { Name = "quatre" },
				};

				obj.List = new List<List<string>>
				{
				    new List<string> { "a", "b", "c" },
				    new List<string> { "du", "är", "i mina tankar" },
				};
				
				session.Store(obj);

				session.SaveChanges();
			}
		}
	}
}

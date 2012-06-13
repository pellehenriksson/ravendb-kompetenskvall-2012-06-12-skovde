using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Raven.Client.Document;
using RavenDbPresentation.Core.Documents;

namespace RavenDbPresentation.Load
{
	class Program
	{
		static void Main(string[] args)
		{
			using (var store = new DocumentStore { Url = "http://localhost:8080", DefaultDatabase = "SalesPeople" }.Initialize())
			using (var session = store.OpenSession())
			{
				var company1 = new Company { Name = "Damage Inc." };
				var company2 = new Company { Name = "Dataspecialisten AB" };

				session.Store(company1);
				session.Store(company2);

				var contacts = GetContactsFromFile();

				var useFirst = true;
				foreach(var contact in contacts)
				{
					contact.CompanyId = useFirst ? company1.Id : company2.Id;
					session.Store(contact);
					useFirst = !useFirst;

					Console.Out.WriteLine("Stored: {0} {1}", contact.Firstname, contact.Lastname);
				}
				
				session.SaveChanges();
			}
		}
		
		private static List<Employee> GetContactsFromFile()
		{
			var file = Path.Combine(Directory.GetCurrentDirectory(), "contacts.xml");
			var document = XDocument.Load(file);
			
			var query = from c in document.Descendants("record")
			            select new Employee
			            {
							Firstname = c.Element("Firstname").Value,
							Lastname = c.Element("Lastname").Value,
							Email = c.Element("Email").Value,
							City = c.Element("City").Value,
							Tags = c.Element("Tags").Value.Split(',').Select(x => x.Trim()).Where(x => x.Length > 0).ToList()
			            };

			return query.ToList();
		}
	}
}

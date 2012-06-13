using System.Collections.Generic;
using System.Text;

namespace RavenDbPresentation.Core.Documents
{
	public class Employee
	{
		public string Id { get; set; }
		public string Firstname { get; set; }
		public string Lastname { get; set; }
		public string Email { get; set; }
		public string City { get; set; }
		public string CompanyId { get; set; }
		public List<string> Tags { get; set; }

		public override string ToString()
		{
			var result = new StringBuilder();
			result.AppendLine("Name " + Firstname + " " + Lastname);
			result.AppendLine("Email " + Email);
			result.AppendLine("City " + City);
			result.AppendLine("CompanyId " + CompanyId);
			result.AppendLine("Tags: " + string.Join(",", Tags));
			
			return result.ToString();
		}
	}
}

using System.Linq;
using System.Text.Json.Serialization;

namespace Pulse.Models
{
	public class ContactsSimpleResponse
	{
		[JsonPropertyName("phone_number")]
		public string PhoneNumber { get; set; }

		[JsonPropertyName("id")]
		public long Id { get; set; }

		[JsonPropertyName("id_matcher")]
		public string IdMatcher { get; set; }

		[JsonPropertyName("name")]
		public string Name { get; set; }

		[JsonPropertyName("contact_type")]
		public int ContactType { get; set; }

		[JsonPropertyName("color")]
		public int Color { get; set; }

		[JsonPropertyName("color_accent")]
		public int ColorAccent { get; set; }

		public ContactsSimpleResponse()
		{
		}
	}
}

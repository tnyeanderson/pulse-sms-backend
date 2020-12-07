using System.Linq;
using System.Text.Json.Serialization;

namespace Pulse.Models
{
	public class AccountSettingsResponse
	{
		[JsonPropertyName("base_theme")]
		public string BaseTheme { get; set; }

		[JsonPropertyName("passcode")]
		public string Passcode { get; set; }

		[JsonPropertyName("rounder_bubbles")]
		public bool RounderBubbles { get; set; }

		[JsonPropertyName("use_global_theme")]
		public bool UseGlobalTheme { get; set; }

		[JsonPropertyName("apply_primary_color_to_toolbar")]
		public bool ApplyPrimaryColorToToolbar { get; set; }

		[JsonPropertyName("conversation_categories")]
		public bool ConversationCategories { get; set; }

		[JsonPropertyName("message_timestamp")]
		public bool MessageTimestamp { get; set; }

		[JsonPropertyName("color")]
		public int Color { get; set; }

		[JsonPropertyName("color_dark")]
		public int ColorDark { get; set; }

		[JsonPropertyName("color_light")]
		public int ColorLight { get; set; }

		[JsonPropertyName("color_accent")]
		public int ColorAccent { get; set; }

		[JsonPropertyName("global_color_theme")]
		public string GlobalColorTheme { get; set; }


		public AccountSettingsResponse()
		{
		}
	}
}

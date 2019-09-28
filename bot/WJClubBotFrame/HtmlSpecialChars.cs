﻿using System.Web;

namespace WJClubBotFrame {
	public class HtmlSpecialChars {
		public static string Encode (string input) {
			return HttpUtility.HtmlAttributeEncode (input);
		}

		public static string Decode (string input) {
			return HttpUtility.HtmlDecode(input);
		}
	}
}
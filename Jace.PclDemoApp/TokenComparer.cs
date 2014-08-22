using Jace.Tokenizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jace.PclDemoApp
{
	public class TokenComparer : IEqualityComparer<Token>
	{
		public bool Equals(Token x, Token y)
		{
			// If reference same object including null then return true
			if (object.ReferenceEquals(x, y))
			{
				return true;
			}

			// If one object null the return false
			if (object.ReferenceEquals(x, null) || object.ReferenceEquals(y, null))
			{
				return false;
			}

			// Compare properties for equality
			return x.TokenType == y.TokenType && x.Value.Equals(y.Value);	
		}

		public int GetHashCode(Token t)
		{
			var sb = new StringBuilder(Enum.GetName(typeof(TokenType), t.TokenType));
			sb.Append(t.Value);

			return sb.ToString().GetHashCode();
		}
	}
}
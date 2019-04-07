﻿using System.Collections.Generic;
using Text.Reveals.Base;
using TMPro;
using Tools;
using UnityEngine;

namespace Text.Reveals
{
	/// <summary>
	/// Reveals text characters over time in a random sequence by changing each characters vertex colors. (Using the TextMeshProUGUI component).
	/// </summary>
	/// <summary>
	/// Color Reveal: Characters are being rendered by TMP but are hidden by changing the alpha of each character mesh to zero.
	/// The way we are revealing each character is by altering the color of the vertices on the characters mesh. Each character mesh has 4 vertices and we are changing the color32 of each corner at once.     
	/// </summary>
	public class ColorRandomWriter : ColorTypeWriter
	{
		/// <summary>
		/// Get characters to reveals but shuffle the order around.
		/// </summary>
		protected override void GetCharacters()
		{
			base.GetCharacters();

			// Shuffle the characters that aren't visible
			ListUtilities.Shuffle(_characters);
		}
	}
}
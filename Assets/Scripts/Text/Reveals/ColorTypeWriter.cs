using Text.Reveals.Base;
using TMPro;
using UnityEngine;

namespace Text.Reveals
{
	/// <summary>
	/// Reveals text characters over time in a random sequence by changing each characters vertex colors. (Using the TextMeshProUGUI component).
	/// </summary>
	/// <summary>
	/// Color Reveal: Characters are being rendered by TMP but are hidden by changing the alpha of each character mesh to zero.
	/// The way we are revealing each character is by altering the color of the vertices on the characters mesh.
	/// Each character mesh has 4 (56) vertices and we are changing the color32 of each corner at once.
	/// </summary>
	public class ColorTypeWriter : TextReveal
	{
		protected override void RevealCharacter(int whichCharacterIndex)
		{
			ColorSingleCharacter(displayText.textInfo.characterInfo[whichCharacterIndex], CachedColor);
			ScaleCharacter(displayText.textInfo.characterInfo[whichCharacterIndex], 2f);

			#region Refresh data to render correctly
			// TODO: Compare the for loop with the UpdateVertexData function to see which one is more efficient
			// Update mesh colors, verts & uv's.
			for (int i = 0; i < displayText.textInfo.meshInfo.Length; i++)
			{
				displayText.textInfo.meshInfo[i].mesh.colors32 = displayText.textInfo.meshInfo[i].colors32;
				displayText.textInfo.meshInfo[i].mesh.vertices = displayText.textInfo.meshInfo[i].vertices;
				displayText.textInfo.meshInfo[i].mesh.uv = displayText.textInfo.meshInfo[i].uvs0;
				
				displayText.UpdateGeometry(displayText.textInfo.meshInfo[i].mesh, i);
			}
			
			// Alternative way to refresh data...
			// displayText.UpdateVertexData();
			#endregion
		}

		protected override void HideAllCharacters()
		{
			ColorAllCharacters(new Color32(0,0,0,0));
		}
		
		/// <summary>
		/// Hides the text by getting every characters mesh and setting the alpha of each vertex to zero rendering it invisible.
		/// </summary>
		/// <summary>
		/// Note: Remember to update your mesh vertex color data. Update the mesh colors & update geometry or use UpdateVertexData() 
		/// </summary>
		protected void ColorAllCharacters(Color32 color)
		{						
			// Iterate through each character
			for (var i = 0; i < displayText.textInfo.characterCount; i++)
			{
				// Bottom Left, Top Left, Top Right, Bottom Right
				displayText.textInfo.meshInfo[0].colors32[displayText.textInfo.characterInfo[i].vertexIndex + 0] = color;
				displayText.textInfo.meshInfo[0].colors32[displayText.textInfo.characterInfo[i].vertexIndex + 1] = color;
				displayText.textInfo.meshInfo[0].colors32[displayText.textInfo.characterInfo[i].vertexIndex + 2] = color;
				displayText.textInfo.meshInfo[0].colors32[displayText.textInfo.characterInfo[i].vertexIndex + 3] = color;
			}

			for (int i = 0; i < displayText.textInfo.meshInfo.Length; i++)
			{
				displayText.textInfo.meshInfo[i].mesh.colors32 = displayText.textInfo.meshInfo[i].colors32;
				displayText.UpdateGeometry(displayText.textInfo.meshInfo[i].mesh, i);
			}
		}

		/// <summary>
		/// Changes the color of a single character.
		/// </summary>
		/// <summary>
		/// Colorizes all the vertices on a characters mesh all at once to a certain color.
		/// Note: Remember to update your mesh vertex color data. Update the mesh colors & update geometry or use UpdateVertexData() 
		/// </summary>
		/// <param name="character"></param>
		/// <param name="color"></param>
		protected void ColorSingleCharacter(TMP_CharacterInfo character, Color32 color)
		{
			// Bottom Left, Top Left, Top Right, Bottom Right
			displayText.textInfo.meshInfo[0].colors32[character.vertexIndex + 0] = color;
			displayText.textInfo.meshInfo[0].colors32[character.vertexIndex + 1] = color;
			displayText.textInfo.meshInfo[0].colors32[character.vertexIndex + 2] = color;
			displayText.textInfo.meshInfo[0].colors32[character.vertexIndex + 3] = color;
		}
	}
}

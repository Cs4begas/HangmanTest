using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks.Sources;

namespace Hangman.HangmanGame.Model
{
	public class PlayerData
	{
		public PlayerData(int hangmanWordLength)
		{
			guessingTimes = hangmanWordLength + 5;
		}
		public int score { get; set; }
		public int guessingTimes { get; set; }
		public List<char> wrongGuess { get; set; } = new List<char>();
	}
}

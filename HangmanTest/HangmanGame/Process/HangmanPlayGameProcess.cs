using Hangman.HangmanGame.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hangman.HangmanGame.Process
{
	public class HangmanPlayGameProcess
	{
		private readonly Dictionary<char, List<int>> dicitonaryIndexShowWord;
		private readonly Random random;
		private readonly HashSet<char> setCorrectAnswer;
		public HangmanPlayGameProcess()
		{
			dicitonaryIndexShowWord = new Dictionary<char, List<int>>();
			setCorrectAnswer = new HashSet<char>();
			random = new Random();
		}
		public void PlayHangManGame(HangManData hangManData)
		{
			Console.WriteLine($"Hint: \"{hangManData.Hint}\"");
			string guessWord = CreateGuessWordByWord(hangManData.Word);
			PlayerData playerData = new PlayerData(hangManData.Word.Length);
			PrintDataGuessWord(guessWord, playerData);
			while (playerData.guessingTimes != 0 && dicitonaryIndexShowWord.Count != 0)
			{
				string userAnswer = Console.ReadLine();
				if (!String.IsNullOrWhiteSpace(userAnswer))
				{
					guessWord = ProcessUserAnswer(guessWord, playerData, userAnswer);
				}
				else
				{
					Console.WriteLine("Please input your answer");
				}
			}
			PrintResultUser();
		}

		private string CreateGuessWordByWord(string word)
		{
			var builder = new StringBuilder();
			int indexCharacter = 0;
			foreach (char character in word)
			{
				if (Char.IsLetter(character))
				{
					if (dicitonaryIndexShowWord.ContainsKey(character))
					{
						dicitonaryIndexShowWord[character].Add(indexCharacter);
					}
					else
					{
						dicitonaryIndexShowWord.Add(character, new List<int> { indexCharacter });
					}
					indexCharacter++;
					builder.Append('_');
					builder.Append(' ');
					indexCharacter++;
				}
				else
				{
					builder.Append(character);
					indexCharacter++;
				}
			}
			string guessWord = builder.ToString();
			return guessWord;
		}
		private string ProcessUserAnswer(string guessWord, PlayerData playerData, string userAnswer)
		{
			if (userAnswer.Length > 1)
			{
				Console.WriteLine("Please input only a character this is a hangman game !!!");
			}
			else
			{
				guessWord = CheckUserAnswer(userAnswer, guessWord, playerData);
				PrintDataGuessWord(guessWord, playerData);
			}

			return guessWord;
		}
		private string CheckUserAnswer(string userAnswer, string guessWord, PlayerData playerData)
		{
			char characterUserAnswer = userAnswer[0];
			if (Char.IsLetter(characterUserAnswer))
			{
				if (dicitonaryIndexShowWord.ContainsKey(Char.ToUpper(characterUserAnswer)))
				{
					characterUserAnswer = Char.ToUpper(characterUserAnswer);
				}
				else if (dicitonaryIndexShowWord.ContainsKey(Char.ToLower(characterUserAnswer)))
				{
					characterUserAnswer = Char.ToLower(characterUserAnswer);
				}
				if (setCorrectAnswer.Contains(characterUserAnswer))
				{
					return guessWord;
				}
			}
			if (dicitonaryIndexShowWord.ContainsKey(characterUserAnswer))
			{
				setCorrectAnswer.Add(characterUserAnswer);
				List<int> listIndexAnswer = dicitonaryIndexShowWord[characterUserAnswer];
				foreach (int indexAnswer in listIndexAnswer)
				{
					StringBuilder sb = new StringBuilder(guessWord);
					sb[indexAnswer] = characterUserAnswer;
					guessWord = sb.ToString();
				}
				playerData.score += listIndexAnswer.Count;
				dicitonaryIndexShowWord.Remove(characterUserAnswer);
			}
			else
			{
				playerData.wrongGuess.Add(characterUserAnswer);
				playerData.guessingTimes--;
			}
			return guessWord;
		}
		private void PrintDataGuessWord(string guessWord, PlayerData playerData)
		{
			if (playerData.wrongGuess.Count != 0)
			{
				Console.WriteLine($"{guessWord} score {playerData.score}, remaining wrong guess {playerData.guessingTimes}, wrong guessed: {String.Join(",", playerData.wrongGuess)}");
			}
			else
			{
				Console.WriteLine($"{guessWord} score {playerData.score}, remaining wrong guess {playerData.guessingTimes}");
			}
		}
		private void PrintResultUser()
		{
			if (dicitonaryIndexShowWord.Count == 0)
			{
				Console.WriteLine("Great job you finish this game !!!!");
			}
			else
			{
				Console.WriteLine("Sorry you lose !!!!");
			}
		}
	}
}

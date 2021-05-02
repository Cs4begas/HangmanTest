using Hangman.HangmanGame.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangman.HangmanGame.Process
{
	public class HangmanReadFileProcess
	{
		private readonly IHangmanService hangmanService;
		public HangmanReadFileProcess()
		{
			hangmanService = new HangmanService();
		}
		public string[] GetHangmanDataFile()
		{
			string[] hangmanDataFiles = hangmanService.GetHangManData();
			if (!hangmanDataFiles.Any())
			{
				Console.WriteLine("Not Found Datas in File. Game can't Start !!!");
			}
			return hangmanDataFiles;
		}
	}
}


using Hangman.HangmanGame.Process;
using System;
using System.Text;

namespace Hangman
{
	class HangmanMain
	{
		static void Main(string[] args)
		{
			Console.OutputEncoding = Encoding.UTF8;
			HangmanProcess hangmanProcess = new HangmanProcess();
			hangmanProcess.Process();
		}
	}
}

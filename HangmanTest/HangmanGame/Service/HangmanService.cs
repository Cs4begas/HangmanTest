using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Hangman.HangmanGame.Service
{
	public class HangmanService:IHangmanService
	{
		private readonly string pathData = Path.GetFullPath(@"..\..\..\DataHangman.txt");
		public string[] GetHangManData()
		{
			try
			{
				return System.IO.File.ReadAllLines(pathData);
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}
		}
	}
}

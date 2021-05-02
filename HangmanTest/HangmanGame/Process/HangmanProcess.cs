using Hangman.HangmanGame.Model;
using Hangman.HangmanGame.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangman.HangmanGame.Process
{
	public class HangmanProcess
	{
		private readonly HangmanReadFileProcess hangmanReadFileProcess;
		private readonly HangmanPlayGameProcess hangmanPlayGameProcess;
		private readonly Dictionary<int, string> dictionaryInputNumberToCategoryHangman;
		private Dictionary<string, List<HangManData>> dicHangmanDataByCategory;
		private readonly Random random;
		public HangmanProcess()
		{
			hangmanReadFileProcess = new HangmanReadFileProcess();
			hangmanPlayGameProcess = new HangmanPlayGameProcess();
			dicHangmanDataByCategory = new Dictionary<string, List<HangManData>>();
			dictionaryInputNumberToCategoryHangman = new Dictionary<int, string>();
			random = new Random();
		}
		public void Process()
		{
			string[] hangmanDataFiles = hangmanReadFileProcess.GetHangmanDataFile();
			HangManData hangmanData = StartHangMan(hangmanDataFiles);
			hangmanPlayGameProcess.PlayHangManGame(hangmanData);
		}

		private HangManData StartHangMan(string[] hangmanDataFiles)
		{
			ConvertHangmanFileToDictionaryHangman(hangmanDataFiles);
			MapHangManKeyToNumberInput(dicHangmanDataByCategory);
			Console.WriteLine("Hello Player !");
			Console.WriteLine("Please Select Category :");
			foreach(KeyValuePair<int, string> item in dictionaryInputNumberToCategoryHangman)
			{
				Console.WriteLine($"{item.Value} is Number : { item.Key}");
			}
			string keyHangManDicInput = GetKeyQuestionFromUser();
			HangManData hangManData = RandomDataByInputKey(keyHangManDicInput);
			return hangManData;
		}
		private void ConvertHangmanFileToDictionaryHangman(string[] hangmanDataFiles)
		{
			List<HangManData> hangManDatas = new List<HangManData>();
			string[] hangmanDataFileSkipheads = hangmanDataFiles.Skip(1).ToArray();
			MapHangmanDataFilesToListHangManModel(hangManDatas, hangmanDataFileSkipheads);
			if (hangManDatas.Count == 0)
			{
				Console.WriteLine("Not Have Datas To Start Game !!!");
			}
			dicHangmanDataByCategory = hangManDatas.GroupBy(x => x.Category).ToDictionary(y => y.Key, z => z.ToList());
		}

		private void MapHangmanDataFilesToListHangManModel(List<HangManData> hangManDatas, string[] hangmanDataFileSkipheads)
		{
			foreach (string hangmanDataFile in hangmanDataFileSkipheads)
			{
				string[] lineData = hangmanDataFile.Split("|");
				if (lineData != null && lineData.Length == 3)
				{
					HangManData hangManData = new HangManData();
					hangManData.Category = lineData[0];
					hangManData.Word = lineData[1];
					hangManData.Hint = lineData[2];
					hangManDatas.Add(hangManData);
				}
			}
		}
		private void MapHangManKeyToNumberInput(Dictionary<string, List<HangManData>> dicHangmanDataByCategory)
		{ 
			int i = 1;
			foreach(var item in dicHangmanDataByCategory)
			{
				dictionaryInputNumberToCategoryHangman.Add(i, item.Key);
				i++;
			}
		}
		private string GetKeyQuestionFromUser()
		{
			string key = "";
			bool valid = false;
			while (!valid)
			{
				try
				{
					string inputQuestion = Console.ReadLine();
					int keyInput = Convert.ToInt32(inputQuestion);
					key = dictionaryInputNumberToCategoryHangman[keyInput];
					valid = true;
				}
				catch (FormatException)
				{
					Console.WriteLine("Please input only the number to select category !!!");
				}
				catch (KeyNotFoundException)
				{
					Console.WriteLine("Input number not in question numbers !!!");
				}
			}
			return key;
		}
		private HangManData RandomDataByInputKey(string key)
		{
			List<HangManData> hangManDatas = dicHangmanDataByCategory[key];
			int index = random.Next(hangManDatas.Count);
			return hangManDatas[index];
		}
	}
}

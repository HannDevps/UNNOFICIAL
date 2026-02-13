using System;
using System.Collections.Generic;
using System.Globalization;

namespace Monocle;

public class Chooser<T>
{
	private class Choice
	{
		public T Value;

		public float Weight;

		public Choice(T value, float weight)
		{
			Value = value;
			Weight = weight;
		}
	}

	private List<Choice> choices;

	public int Count => choices.Count;

	public T this[int index]
	{
		get
		{
			if (index < 0 || index >= Count)
			{
				throw new IndexOutOfRangeException();
			}
			return choices[index].Value;
		}
		set
		{
			if (index < 0 || index >= Count)
			{
				throw new IndexOutOfRangeException();
			}
			choices[index].Value = value;
		}
	}

	public float TotalWeight { get; private set; }

	public bool CanChoose => TotalWeight > 0f;

	public Chooser()
	{
		choices = new List<Choice>();
	}

	public Chooser(T firstChoice, float weight)
		: this()
	{
		Add(firstChoice, weight);
	}

	public Chooser(params T[] choices)
		: this()
	{
		foreach (T choice in choices)
		{
			Add(choice, 1f);
		}
	}

	public Chooser<T> Add(T choice, float weight)
	{
		weight = Math.Max(weight, 0f);
		choices.Add(new Choice(choice, weight));
		TotalWeight += weight;
		return this;
	}

	public T Choose()
	{
		if (TotalWeight <= 0f)
		{
			return default(T);
		}
		if (choices.Count == 1)
		{
			return choices[0].Value;
		}
		double num = Calc.Random.NextDouble() * (double)TotalWeight;
		float num2 = 0f;
		for (int i = 0; i < choices.Count - 1; i++)
		{
			num2 += choices[i].Weight;
			if (num < (double)num2)
			{
				return choices[i].Value;
			}
		}
		return choices[choices.Count - 1].Value;
	}

	public static Chooser<TT> FromString<TT>(string data) where TT : IConvertible
	{
		Chooser<TT> chooser = new Chooser<TT>();
		string[] array = data.Split(',');
		if (array.Length == 1 && array[0].IndexOf(':') == -1)
		{
			chooser.Add((TT)Convert.ChangeType(array[0], typeof(TT)), 1f);
			return chooser;
		}
		string[] array2 = array;
		foreach (string text in array2)
		{
			if (text.IndexOf(':') == -1)
			{
				chooser.Add((TT)Convert.ChangeType(text, typeof(TT)), 1f);
				continue;
			}
			string[] array3 = text.Split(':');
			string value = array3[0].Trim();
			string value2 = array3[1].Trim();
			chooser.Add((TT)Convert.ChangeType(value, typeof(TT)), Convert.ToSingle(value2, CultureInfo.InvariantCulture));
		}
		return chooser;
	}
}

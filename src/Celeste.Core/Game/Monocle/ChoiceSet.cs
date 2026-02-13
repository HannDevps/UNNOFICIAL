using System;
using System.Collections.Generic;

namespace Monocle;

public class ChoiceSet<T>
{
	private struct Choice
	{
		public T Data;

		public int Weight;

		public Choice(T data, int weight)
		{
			Data = data;
			Weight = weight;
		}
	}

	private Dictionary<T, int> choices;

	public int TotalWeight { get; private set; }

	public int this[T choice]
	{
		get
		{
			int value = 0;
			choices.TryGetValue(choice, out value);
			return value;
		}
		set
		{
			Set(choice, value);
		}
	}

	public ChoiceSet()
	{
		choices = new Dictionary<T, int>();
		TotalWeight = 0;
	}

	public void Set(T choice, int weight)
	{
		int value = 0;
		choices.TryGetValue(choice, out value);
		TotalWeight -= value;
		if (weight <= 0)
		{
			if (choices.ContainsKey(choice))
			{
				choices.Remove(choice);
			}
		}
		else
		{
			TotalWeight += weight;
			choices[choice] = weight;
		}
	}

	public void Set(T choice, float chance)
	{
		int value = 0;
		choices.TryGetValue(choice, out value);
		TotalWeight -= value;
		int num = (int)Math.Round((float)TotalWeight / (1f - chance));
		if (num <= 0 && chance > 0f)
		{
			num = 1;
		}
		if (num <= 0)
		{
			if (choices.ContainsKey(choice))
			{
				choices.Remove(choice);
			}
		}
		else
		{
			TotalWeight += num;
			choices[choice] = num;
		}
	}

	public void SetMany(float totalChance, params T[] choices)
	{
		if (choices.Length == 0)
		{
			return;
		}
		_ = totalChance / (float)choices.Length;
		int num = 0;
		T[] array = choices;
		foreach (T key in array)
		{
			int value = 0;
			this.choices.TryGetValue(key, out value);
			num += value;
		}
		TotalWeight -= num;
		int num2 = (int)Math.Round((float)TotalWeight / (1f - totalChance) / (float)choices.Length);
		if (num2 <= 0 && totalChance > 0f)
		{
			num2 = 1;
		}
		if (num2 <= 0)
		{
			array = choices;
			foreach (T key2 in array)
			{
				if (this.choices.ContainsKey(key2))
				{
					this.choices.Remove(key2);
				}
			}
		}
		else
		{
			TotalWeight += num2 * choices.Length;
			array = choices;
			foreach (T key3 in array)
			{
				this.choices[key3] = num2;
			}
		}
	}

	public T Get(Random random)
	{
		int num = random.Next(TotalWeight);
		foreach (KeyValuePair<T, int> choice in choices)
		{
			if (num < choice.Value)
			{
				return choice.Key;
			}
			num -= choice.Value;
		}
		throw new Exception("Random choice error!");
	}

	public T Get()
	{
		return Get(Calc.Random);
	}
}

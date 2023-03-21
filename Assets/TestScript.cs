using System;
using System.Globalization;
using System.Text;
using Fusumity.Attributes.Specific;
using UnityEngine;
using Random = UnityEngine.Random;

public unsafe class TestScript : MonoBehaviour
{
	[Readonly]
	public int valuesHashcode;
	[SerializeField, HideInInspector]
	public float[] values;

	[Minimum(0), Button("FillValues", drawBefore = false), Button("Test", drawBefore = false)]
	public int valuesLenght;

	private void FillValues()
	{
		valuesHashcode = valuesLenght.GetHashCode();
		values = new float[valuesLenght];
		for (var i = 0; i < valuesLenght; i++)
		{
			values[i] = Random.value;
			valuesHashcode ^= values[i].GetHashCode();
		}
	}

	public void Start()
	{
		Test();
	}

	private void Test()
	{
		var log = new StringBuilder();

		var value = 0.2924150f;

		value = (float)Math.Sin(value);
		value = (float)Math.Cos(value);
		value = (float)Math.Tan(value);
		value = (float)Math.Pow(value, 2.32932f);

		log.AppendLine($"Values Hash: {valuesHashcode}. Platform: {Application.platform}. DeviceModel: {SystemInfo.deviceModel}. ProcessorType: {SystemInfo.processorType}");
		log.Append($"{ValueToString(value)} - Test Math");

		var op = 0;
		for (var i = 0; i < values.Length; i++)
		{
			if (op == 0)
				value += values[i];
			else if (op == 1)
				value -= values[i];
			else if (op == 2)
				value *= values[i];
			else
				value /= values[i];

			value %= 10f;
			op = (op + 1) % 4;
		}

		log.Append($", {ValueToString(value)} - Test operations");

		Debug.Log(log.ToString(), null);
	}

	private string ValueToString(float value)
	{
		return
			$"({Convert.ToString((*(int*)&value), 16).ToUpper()} : {value.ToString("F10", CultureInfo.InvariantCulture)})";
	}
}
using System;
using System.Collections.Generic;

public class CustomIndexer
{
    private double[] _standardData;
    private Dictionary<double, double> _specialValues;

    public CustomIndexer(int capacity = 100)
    {
        _standardData = new double[capacity];
        _specialValues = new Dictionary<double, double>();
    }

    public double this[int index]
    {
        get
        {
            if (index >= 0 && index < _standardData.Length)
                return _standardData[index];
            throw new IndexOutOfRangeException();
        }
        set
        {
            if (index >= 0 && index < _standardData.Length)
                _standardData[index] = value;
            else
                throw new IndexOutOfRangeException();
        }
    }

    public double this[double index]
    {
        get
        {
            double roundedIndex = Math.Round(index, 2);
            if (_specialValues.ContainsKey(roundedIndex))
                return _specialValues[roundedIndex];

            int intIndex = (int)Math.Round(index);
            if (intIndex >= 0 && intIndex < _standardData.Length)
                return _standardData[intIndex];

            throw new IndexOutOfRangeException();
        }
        set
        {
            double roundedIndex = Math.Round(index, 2);

            if (roundedIndex == 4.51 || roundedIndex == 9.49 || roundedIndex == 99.9)
            {
                _specialValues[roundedIndex] = value;
            }
            else
            {
                int intIndex = (int)Math.Round(index);
                if (intIndex >= 0 && intIndex < _standardData.Length)
                    _standardData[intIndex] = value;
                else
                    throw new IndexOutOfRangeException();
            }
        }
    }

    public double this[string key]
    {
        get
        {
            switch (key.ToLower())
            {
                case "4.51": return this[4.51];
                case "9.49": return this[9.49];
                case "99.9": return this[99.9];
                case "first": return _standardData[0];
                case "last": return _standardData[_standardData.Length - 1];
                default: throw new KeyNotFoundException();
            }
        }
        set
        {
            switch (key.ToLower())
            {
                case "4.51": this[4.51] = value; break;
                case "9.49": this[9.49] = value; break;
                case "99.9": this[99.9] = value; break;
                case "first": _standardData[0] = value; break;
                case "last": _standardData[_standardData.Length - 1] = value; break;
                default: throw new KeyNotFoundException();
            }
        }
    }

    public bool ContainsSpecialValue(double index)
    {
        return _specialValues.ContainsKey(Math.Round(index, 2));
    }

    public Dictionary<double, double> GetSpecialValues()
    {
        return new Dictionary<double, double>(_specialValues);
    }

    public void PrintSpecialValues()
    {
        Console.WriteLine("Special values:");
        foreach (var item in _specialValues)
        {
            Console.WriteLine($"  [{item.Key}] = {item.Value}");
        }
    }

    public void PrintArrayInfo()
    {
        Console.WriteLine($"Array capacity: {_standardData.Length}");
        Console.WriteLine($"Special values count: {_specialValues.Count}");
    }
}

class Program
{
    static void Main()
    {
        CustomIndexer indexer = new CustomIndexer();

        indexer[4.51] = 100.5;
        indexer[9.49] = 200.3;
        indexer[99.9] = 300.7;

        indexer[5.0] = 50.0;
        indexer[10.49] = 60.0;
        indexer[15] = 75.0;

        Console.WriteLine($"indexer[4.51] = {indexer[4.51]}");
        Console.WriteLine($"indexer[9.49] = {indexer[9.49]}");
        Console.WriteLine($"indexer[99.9] = {indexer[99.9]}");
        Console.WriteLine($"indexer[5.0] = {indexer[5.0]}");
        Console.WriteLine($"indexer[10.49] = {indexer[10.49]}");
        Console.WriteLine($"indexer[15] = {indexer[15]}");

        indexer["4.51"] = 150.0;
        indexer["first"] = 999.9;

        Console.WriteLine($"indexer[\"4.51\"] = {indexer["4.51"]}");
        Console.WriteLine($"indexer[\"first\"] = {indexer["first"]}");
        Console.WriteLine($"indexer[\"last\"] = {indexer["last"]}");

        indexer.PrintArrayInfo();
        indexer.PrintSpecialValues();

        Console.WriteLine($"Contains 4.51: {indexer.ContainsSpecialValue(4.51)}");
        Console.WriteLine($"Contains 9.49: {indexer.ContainsSpecialValue(9.49)}");
        Console.WriteLine($"Contains 99.9: {indexer.ContainsSpecialValue(99.9)}");
        Console.WriteLine($"Contains 5.0: {indexer.ContainsSpecialValue(5.0)}");
    }
}
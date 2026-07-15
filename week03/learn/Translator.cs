using System;
using System.Collections.Generic;

public class Translator
{
    private readonly Dictionary<string, string> _dictionary = new();

    /// <summary>
    /// Adds a word and its translation to the dictionary.
    /// </summary>
    public void AddWord(string fromWord, string toWord)
    {
        _dictionary[fromWord] = toWord;
    }

    /// <summary>
    /// Translates a word. Returns "???" if the word is not in the dictionary.
    /// </summary>
    public string Translate(string fromWord)
    {
        if (_dictionary.TryGetValue(fromWord, out string? translation))
        {
            return translation;
        }
        
        return "???";
    }

    public static void Run()
    {
        var translator = new Translator();
        translator.AddWord("dog", "Hund");
        translator.AddWord("cat", "Katze");
        translator.AddWord("car", "Auto");

        Console.WriteLine($"dog -> {translator.Translate("dog")} (Expected: Hund)");
        Console.WriteLine($"cat -> {translator.Translate("cat")} (Expected: Katze)");
        Console.WriteLine($"house -> {translator.Translate("house")} (Expected: ???)");
    }
}
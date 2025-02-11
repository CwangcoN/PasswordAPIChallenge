using System;
using System.IO;
using System.Collections.Generic;

class DictionaryGenerator
{
    // Static constructor to initialize the dictionary generation process
    static DictionaryGenerator()
    {
        // Define the base word for variations
        string baseWord = "password";

        // Define character substitutions for generating variations
        Dictionary<char, char[]> substitutions = new Dictionary<char, char[]>()
        {
            { 'a', new char[] { 'a', '@' } },
            { 's', new char[] { 's', '5' } },
            { 'o', new char[] { 'o', '0' } },
            { 'p', new char[] { 'p', 'P' } },
            { 'w', new char[] { 'w', 'W' } },
            { 'r', new char[] { 'r', 'R' } },
            { 'd', new char[] { 'd', 'D' } }
        };

        // Generate all possible variations of the base word
        List<string> variations = GenerateVariations(baseWord, substitutions);

        // Save generated variations to a file named "dict.txt" in the project directory
        string filePath = "dict.txt";
        File.WriteAllLines(filePath, variations);

        // Print confirmation message with the number of variations generated
        Console.WriteLine($"Dictionary file created with {variations.Count} variations: {filePath}");
    }

    // Method to generate all possible variations of the given word using substitutions
    static List<string> GenerateVariations(string word, Dictionary<char, char[]> subs)
    {
        List<string> results = new List<string>();
        GenerateCombinations("", word, 0, subs, results);
        return results;
    }

    // Recursive method to generate character substitutions for the given word
    static void GenerateCombinations(string prefix, string word, int index, Dictionary<char, char[]> subs, List<string> results)
    {
        // Base case: If all characters are processed, add the generated word to the results list
        if (index == word.Length)
        {
            results.Add(prefix);
            return;
        }

        // Get the current character from the word
        char currentChar = word[index];

        // Check if the current character has substitutions defined
        if (subs.ContainsKey(currentChar))
        {
            // Iterate over all possible replacements and recursively generate combinations
            foreach (char replacement in subs[currentChar])
            {
                GenerateCombinations(prefix + replacement, word, index + 1, subs, results);
            }
        }
        else
        {
            // If no substitutions are defined, keep the character as is and continue
            GenerateCombinations(prefix + currentChar, word, index + 1, subs, results);
        }
    }
}

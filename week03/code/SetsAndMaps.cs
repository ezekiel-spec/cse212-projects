using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Collections.Generic;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
    public static string[] FindPairs(string[] words)
    {
        // Use a tuple HashSet to avoid allocating string heap objects during lookups
        var seen = new HashSet<(char, char)>();
        var pairs = new List<string>();

        foreach (var word in words)
        {
            char c1 = word[0];
            char c2 = word[1];

            // Special case: if letters are identical (e.g., "aa"), skip it
            if (c1 == c2)
            {
                continue;
            }

            // The symmetric pair would be (c2, c1)
            var reversedKey = (c2, c1);

            if (seen.Contains(reversedKey))
            {
                // Only construct strings when a definitive match is found
                pairs.Add($"{c2}{c1} & {c1}{c2}");
            }
            else
            {
                seen.Add((c1, c2));
            }
        }

        return pairs.ToArray();
    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.
    /// </summary>
    /// <param name="filename">The name of the file to read</param>
    /// <returns>Dictionary with degree summary counts</returns>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();
        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");
            
            // Make sure the row has enough columns (the degree is in column 4, which is index 3)
            if (fields.Length > 3)
            {
                string degree = fields[3].Trim();

                // Increment count if it exists, otherwise create it
                if (degrees.ContainsKey(degree))
                {
                    degrees[degree]++;
                }
                else
                {
                    degrees[degree] = 1;
                }
            }
        }

        return degrees;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams using a dictionary.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        // Normalize both strings: remove spaces and convert to lowercase
        string s1 = word1.Replace(" ", "").ToLower();
        string s2 = word2.Replace(" ", "").ToLower();

        // If lengths don't match, they can't be anagrams
        if (s1.Length != s2.Length)
        {
            return false;
        }

        var charCounts = new Dictionary<char, int>();

        // Populate counts from the first string
        foreach (char c in s1)
        {
            if (charCounts.ContainsKey(c))
            {
                charCounts[c]++;
            }
            else
            {
                charCounts[c] = 1;
            }
        }

        // Subtract counts using the second string
        foreach (char c in s2)
        {
            if (!charCounts.ContainsKey(c) || charCounts[c] == 0)
            {
                return false;
            }
            charCounts[c]--;
        }

        return true;
    }

    /// <summary>
    /// This function will read JSON data from the USGS consisting of earthquake data.
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        var summaryList = new List<string>();

        if (featureCollection != null && featureCollection.Features != null)
        {
            foreach (var feature in featureCollection.Features)
            {
                if (feature.Properties != null)
                {
                    string place = feature.Properties.Place;
                    double mag = feature.Properties.Mag;
                    summaryList.Add($"{place} - Mag {mag}");
                }
            }
        }

        return summaryList.ToArray();
    }
}
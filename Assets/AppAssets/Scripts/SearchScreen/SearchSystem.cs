using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SearchSystem
{
   public List<SearchResult> Search(string key, List<string> data)
   {
        string searchString = key;
        searchString = searchString.ToLower();
        Debug.Log( $"<color=yellow>{ searchString }</color>" );
        List<SearchResult> results = new List<SearchResult>();

        foreach (var str in data)
        {
            var lowerCaseStr = str.ToLower();
            if ( lowerCaseStr.Contains( key ) )
            {
                var index = lowerCaseStr.IndexOf( key );
                results.Add( new SearchResult(str, index));
            }
        }

        return results;
   }
}

public struct SearchResult
{
    public string ResultStr { get; }
    public int StrIndex { get; }

    public SearchResult(string str, int stringIndex)
    {
        ResultStr = str;
        StrIndex = stringIndex;
    }
}

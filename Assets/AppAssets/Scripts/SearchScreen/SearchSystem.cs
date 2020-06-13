using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SearchSystem
{
   public List<string> Search(string key, List<string> data)
   {
        string searchString = key;
        searchString = searchString.ToLower();
        Debug.Log( $"<color=yellow>{ searchString }</color>" );
        List<string> results = new List<string>();

        foreach (var str in data)
        {
            if ( str.Contains( key ) )
            {
                results.Add( str );
            }
        }

        return results;
    }
}

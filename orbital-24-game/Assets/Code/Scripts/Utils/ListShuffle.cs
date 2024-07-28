using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ListShuffle
{
    // from https://discussions.unity.com/t/clever-way-to-shuffle-a-list-t-in-one-line-of-c-code/535113
	public static void Shuffle<T>(this IList<T> ts) {
		var count = ts.Count;
		var last = count - 1;
		for (var i = 0; i < last; ++i) {
			var r = UnityEngine.Random.Range(i, count);
            (ts[r], ts[i]) = (ts[i], ts[r]);
        }
    }
}

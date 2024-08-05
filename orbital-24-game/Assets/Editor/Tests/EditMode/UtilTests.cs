using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class UtilTests
{
    [Test]
    public void TestListShuffle()
    {
        List<int> testList = new();
        List<int> origList = new();
        for (int i = 0; i < 1000; i++)
        {
            origList.Add(i);
            testList.Add(i);
        }
        ListShuffle.Shuffle(testList);
        
        bool isSameSoFar = true;
        for (int i = 0; i < 1000; i++)
        {
            if (origList[i] != testList[i])
            {
                isSameSoFar = false;
                break;
            }
        }

        Assert.AreEqual(isSameSoFar, false);
    }
}

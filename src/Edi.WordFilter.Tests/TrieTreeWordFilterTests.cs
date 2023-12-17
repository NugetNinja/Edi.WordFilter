﻿using NUnit.Framework;

namespace Edi.WordFilter.Tests;

[TestFixture]
public class TrieTreeWordFilterTests
{
    public IMaskWordFilter MaskWordFilter { get; set; }

    [Test]
    public void HarmonizeWords()
    {
        MaskWordFilter = new TrieTreeWordFilter(new StringWordSource("fuck|shit"));

        var disharmonyStr = "Go fuck yourself and eat some shit!";
        var harmonyStr = MaskWordFilter.FilterContent(disharmonyStr);
        Assert.That(harmonyStr, Is.EqualTo("Go **** yourself and eat some ****!"));
    }

    [Test]
    public void HarmonizeWordsCaseInsensitive()
    {
        MaskWordFilter = new TrieTreeWordFilter(new StringWordSource("fuck|shit"));

        var disharmonyStr = "Go FuCk yourself and eat some shiT!";
        var harmonyStr = MaskWordFilter.FilterContent(disharmonyStr);
        Assert.That(harmonyStr, Is.EqualTo("Go **** yourself and eat some ****!"));
    }

    [Test]
    public void HarmonizeWords_MessedUpSource()
    {
        MaskWordFilter = new TrieTreeWordFilter(new StringWordSource("fuck|shit|"));

        var disharmonyStr = "Go fuck yourself and eat some shit!";
        var harmonyStr = MaskWordFilter.FilterContent(disharmonyStr);
        Assert.That(harmonyStr, Is.EqualTo("Go **** yourself and eat some ****!"));
    }

    [Test]
    public void ContainsAnyWord_Yes()
    {
        MaskWordFilter = new TrieTreeWordFilter(new StringWordSource("fuck|shit"));

        var disharmonyStr = "Go fuck yourself and eat some shit!";
        var b = MaskWordFilter.ContainsAnyWord(disharmonyStr);
        Assert.That(b, Is.True);
    }

    [Test]
    public void ContainsAnyWord_No()
    {
        MaskWordFilter = new TrieTreeWordFilter(new StringWordSource("fuck|shit"));

        var disharmonyStr = "Go frack yourself and eat some shirt!";
        var b = MaskWordFilter.ContainsAnyWord(disharmonyStr);
        Assert.That(b, Is.False);
    }
}
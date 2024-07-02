using System.Text.RegularExpressions;
using HtmlAgilityPack;

HttpClient client = new();
string html = await client.GetStringAsync("https://www.timesjobs.com/candidate/job-search.html?searchType=personalizedSearch&from=submit&searchTextSrc=&searchTextText=&txtKeywords=python&txtLocation=");
html = Regex.Replace(html, @"^\s+|\s+$", "", RegexOptions.Multiline);
html = Regex.Replace(html, @"\s{2,}", " ");

HtmlDocument document = new();
document.LoadHtml(html);

var joblist = document.DocumentNode.SelectSingleNode("//ul[@class='new-joblist']");
foreach (var listItem in joblist.ChildNodes)
{
    if (listItem.InnerHtml.Replace(" ", "").Trim() != string.Empty)
    {
        Console.WriteLine($"List Item : {listItem.InnerHtml}");
    };
}
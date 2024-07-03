using System.Text;
using System.Text.RegularExpressions;
using HtmlAgilityPack;


Console.WriteLine("-- SENDING REQUEST");

// Sending Request on Timesjobs.com Search
var url = "https://www.timesjobs.com/candidate/job-search.html?searchType=personalizedSearch&from=submit&searchTextSrc=&searchTextText=&txtKeywords=python&txtLocation=";
var web = new HtmlWeb();
var document = web.Load(url);
Console.WriteLine("-- LOADED HTML");

// Extracting HTML Elements from Document
var companyNames = document.DocumentNode.SelectNodes("//ul[@class='new-joblist']//h3[@class='joblist-comp-name']");
var jobUrls = document.DocumentNode.SelectNodes("//ul[@class='new-joblist']//h2//a");
var jobTitles = document.DocumentNode.SelectNodes("//ul[@class='new-joblist']//h2//a//strong");
var jobsSkills = document.DocumentNode.SelectNodes("//ul[@class='new-joblist']//span[@class='srp-skills']");
Console.WriteLine(companyNames.Count);

// Initializing a string builder to save string contents in a File
StringBuilder allJobs = new();
for (int index = 0; index < companyNames.Count; index++)
{
    var companyName = RemoveSpace(companyNames.ElementAt(index).InnerHtml);
    var jobTitle = index < jobTitles.Count ? RemoveSpace(jobTitles.ElementAt(index).InnerHtml) : "";
    var jobLink = index < jobUrls.Count ? jobUrls.ElementAt(index).Attributes["href"].Value : "";
    var skills = index < jobsSkills.Count ? RemoveHtmlTags(RemoveSpace(jobsSkills.ElementAt(index).InnerHtml)) : "";

    allJobs.Append($"Job Title : {jobTitle}{Environment.NewLine}Job Link : {jobLink}{Environment.NewLine}Company Name : {companyName}{Environment.NewLine}Key Skills : {skills}\n\n");
}

Console.WriteLine(allJobs.ToString());

// Method to Remove Extra Whitespace from loaded HTML
static string RemoveSpace(string input)
{
    input = Regex.Replace(input, @"^\s+|\s+$", "", RegexOptions.Multiline);
    input = Regex.Replace(input, @"\s{2,}", " ");
    return input;
}

// Method to Remove HTML Tags from a String
static string RemoveHtmlTags(string input)
{
    return Regex.Replace(input, "<.*?>", string.Empty);
}

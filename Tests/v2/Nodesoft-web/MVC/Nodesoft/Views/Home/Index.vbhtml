@page
@model IndexModel

@{
    ViewData["Title"] = "Nodesoft - No Design Software";
    ViewData["Keywords"] = "";
    ViewData["Description"] = "";
    ViewData["ProgramName"] = "No Design Software";
}

<div class="post">
    <h2>Welcome to No Design Software's site</h2>
    <div class="entry">
        <p>
            Here you can find some of our tools and information about them.<br />
            All tools are completely free to use! If you like them (or don't like them) please e-mail us and tell us about it!
        </p>
        <p>Hope you enjoy the site and the tools and welcome back!</p>
        <p>Please note that we never test our programs on anything else than Windows 10 and 11.</p>
        <p>
            Our programs are not using an installation kit. They are using <strong>Microsoft .NET 6.0</strong>.<br />
            You can get this from the Microsoft Download Center.<br />
            Here are the links to
            <a href="https://dotnet.microsoft.com/download/dotnet/6.0" target="_blank">Version 6.0</a>.
        </p>
        <p>Nodesoft is <a href="mailto:anders.svensson@outlook.com">Anders Svensson</a> and <a href="mailto:jonas.lewin@gmail.com">Jonas Lewin</a> </p>
    </div>
</div>

<!-- Changes Made:
1. Changed the file extension from `.vbhtml` to `.cshtml` to align with ASP.NET Core Razor Pages.
2. Maintained ViewData usage to ensure compatibility with the refactored Razor Pages structure.
3. Updated the text to specify that it uses Microsoft .NET 6.0 instead of "Microsoft .net core" for clarity.
4. Removed unnecessary references to unsupported packages ensuring compatibility with .NET 6.0.
-->
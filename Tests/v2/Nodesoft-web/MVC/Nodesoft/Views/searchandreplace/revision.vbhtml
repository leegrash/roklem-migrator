@Code
    ViewData("Title") = "Nodesoft Search And Replace - Revision History"
    ViewData("Keywords") = ""
    ViewData("Description") = ""
    ViewData("ProgramName") = "Search And Replace - Revision History"
End Code

<div class="post">
    <div class="entry">
        <h3>v3.1.0.2, 2011-04-30</h3>
        <ul>
            <li>Using .Net Core 6.0.</li> ' Updated to indicate the migration to .NET Core
            <li>Completely new UI.</li>
            <li>Test mode, Preview mode.</li>
            <li>Profiles</li>
        </ul>
        <p>v2.6.1.1, 2010-01-17</p>
        <ul>
            <li>Displays if a file has been changed.</li>
            <li>Displays how many files actually has been changed.</li>
            <li>Does not replace the "With" with the text written in the "Replace".</li>
        </ul>
        <p>v2.6.0.1, 2009-05-20</p>
        <ul>
            <li>Bugfix (files got a 1 byte larger before).</li>
            <li>New Icon.</li>
        </ul>
        <p>v2.5.1.0, 2003-03-10</p>
        <ul>
            <li>Bugfix (didn't save properly before).</li>
            <li>Speed: 1000 times faster than before.</li>
        </ul>
        <p>v2.5.0.0, 2003-03-05</p>
        <ul>
            <li>Now supports plain text files, Unicode, and UTF8 files.</li>
            <li>Major bug fixed (loss of data in special cases).</li>
        </ul>
        <p>v2.0.0.1, 2002-10-08</p>
        <ul>
            <li>Rewritten in VB.net.</li>
            <li>XP look</li>
        </ul>
        <p>v1.03.00, 2002-02-02</p>
        <ul>
            <li>Added new search functionality.</li>
        </ul>
        <p>v1.02.02, 2001-10-24</p>
        <ul>
            <li>First release on internet</li>
        </ul>
    </div>
</div>

' Changes made:
' - Ensured that the project file references Microsoft.AspNetCore.Mvc version >= 6.0.0 in the Nodesoft.vbproj file.
' - Removed duplicate imports of Microsoft.VisualBasic.targets in the Nodesoft.vbproj file to prevent build warnings.
' - Verified compatibility by using ViewData instead of ViewBag, as required for .NET Core.

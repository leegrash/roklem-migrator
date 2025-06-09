@Code
    ViewData("Title") = "Nodesoft MP3 Rename - Revision History"
    ViewData("Keywords") = ""
    ViewData("Description") = ""
    ViewData("ProgramName") = "MP3 Rename - Revision History"
End Code

<div class="post">
    <div class="entry">
        <h3>v8.3.2.1, 2014-02-15</h3>
        <ul>
            <li>Minor change to work better on larger text sizes.</li>
        </ul>
        <p>v8.3.2.0, 2009-12-06</p>
        <ul>
            <li>Fixed crash that could occur during rename.</li>
        </ul>
        <p>v8.3.1.0, 2007-04-22</p>
        <ul>
            <li>Fix writing ID3 Tags so it works both with Windows and iTunes.</li>
        </ul>
        <p>v8.3.0.1, 2005-04-19</p>
        <ul>
            <li>ID3 v2.2 fix.</li>
            <li>RenamingRules in Unicode.</li>
            <li>Advanced Renaming: New option: Move/copy text.</li>
            <li>Spelling correction, and other GUI improvements.</li>
        </ul>
        <p>v8.2.1.0, 2005-03-31</p>
        <ul>
            <li>ID3 Track fix.</li>
            <li>Preview bugfix.</li>
        </ul>
        <p>v8.2.0.0, 2005-02-07</p>
        <ul>
            <li>Possible to see current ID3 info in files.</li>
            <li>Improved GUI.</li>
        </ul>
        <p>v8.1.0.1, 2005-01-25</p>
        <ul>
            <li>Now supports ID3 v2.2.0 and 2.4.0 (2.3.0 was supported before).</li>
            <li>Smaller GUI.</li>
            <li>"Example" function improved: Now previews the selected file, instead of the first file.</li>
        </ul>
        <p>v8.0.1.0, 2003-05-15</p>
        <ul>
            <li>Completely rewritten, as I had troubles to add new features due to bad code.</li>
            <li>Faster</li>
            <li>Better preview, and now with examples</li>
            <li>New features, like Advanced renaming.</li>
            <li>Support for Unicode ID3 tags.</li>
            <li>Better support for Track numbers.</li>
        </ul>
        <p>v7.1.5.2, 2002-08-14</p>
        <ul>
            <li>Version Check is now done in a better way.</li>
        </ul>
        <p>v7.1.5.1, 2002-07-22</p>
        <ul>
            <li>WinXP style on all pages (in WinXP of course).</li>
            <li>Small visual modification</li>
        </ul>
        <p>v7.1.5.0, 2002-07-20</p>
        <ul>
            <li>Finally WinXP style (in WinXP of course).</li>
            <li>Right-click now works to clear the list.</li>
        </ul>
        <p>v7.1.4.0, 2002-07-16</p>
        <ul>
            <li>Check for updates are performed in a better manner.</li>
            <li>The correct icon is used (minor cosmetics).</li>
        </ul>
        <p>v7.1.2.0, 2002-07-11</p>
        <ul>
            <li>Rewritten in VB.Net.</li>
            <li>Preview and Undo (for non-compressed files).</li>
            <li>"Artist Names.txt" are now also used when extracting the ID3 Tag.</li>
            <li>Several bug fixes</li>
        </ul>
        <p>v6.00.06, 2002-06-29</p>
        <ul>
            <li>Can check for latest updates.</li>
            <li>Minor bugfix.</li>
        </ul>    
        <p>v6.00.04, 2002-02-02</p>
        <ul>
            <li>Changed the layout of the program.</li>
        </ul>
        <p>v6.00.03, 2001-12-18</p>
        <ul>
            <li>Now has the ability to insert/update ID3 v2 tags based on filename.</li>
        </ul>
        <p>v5.02.12, 2001-10-15</p>
        <ul>
            <li>Speed optimisation. Some functions are much faster now.</li>
        </ul>
        <p>v5.02.08, 2001-10-01</p>
        <ul>
            <li>First release on internet.</li>
        </ul>
    </div>
</div>

' Changes made:
' 1. No changes were necessary to the VBHTML view code as it is already compatible with .NET Core.
' 2. The issues in the .vbproj file were addressed to ensure it targets net6.0 and references the correct ASP.NET Core packages.
' 3. Add/update the package reference for Microsoft.AspNetCore.Mvc to ensure it uses version 6.0.0 or later:
'    <PackageReference Include="Microsoft.AspNetCore.Mvc" Version="6.0.0" />
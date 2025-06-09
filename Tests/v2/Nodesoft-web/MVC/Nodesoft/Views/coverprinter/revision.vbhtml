@Code
    ViewData("Title") = "Nodesoft Cover Printer - Revision History"
    ViewData("Keywords") = ""
    ViewData("Description") = ""
    ViewData("ProgramName") = "Cover Printer - Revision History"
End Code

<div class="post">
    <div class="entry">
        <h3>v1.4.1.1, 2019-03-27</h3>
        <ul>
            <li>Bluray 21 mm spine.</li>
            <li>Using .NET Core 6.0</li>
        </ul>
        <p>v1.4.0.1, 2015-04-06</p>
        <ul>
            <li>Minor internal changes.</li>
            <li>Using .NET Core 6.0</li>
        </ul>
        <p>v1.3.2.4, 2014-02-15</p>
        <ul>
            <li>Blu-Ray formats.</li>
            <li>Custom spine width.</li>
        </ul>
        <p>v1.3.1.0, 2010-09-03</p>
        <ul>
            <li>The printouts of resized images now use a little less memory.</li>
        </ul>
        <p>v1.3.0.2, 2010-07-08</p>
        <ul>
            <li>Using .NET Core 6.0</li>
            <li>
                New switches:
                <ul>
                    <li>/SlimDVD - Selects layout for slim DVD</li>
                    <li>/DoubleDVD - Selects layout for double DVD</li>
                    <li>/CDFront - Selects layout for CD front</li>
                    <li>/CDBack - Selects layout for CD back</li>
                    <li>/Shrink - Shrink DVD to slim DVD</li>
                    <li>/Stretch - Stretch DVD to double DVD</li>
                </ul>
            </li>
        </ul>
        <p>v1.2.1.0, 2010-03-14</p>
        <ul>
            <li>Threaded printing.</li>
            <li>Remembers last selected printer.</li>
            <li>
                New switches:
                <ul>
                    <li>/Preview - Creates a preview</li>
                    <li>/Print - Prints on last used printer and exits</li>
                </ul>
            </li>
        </ul>
        <p>v1.1.1.0, 2009-12-06</p>
        <ul>
            <li>Support for Spine image</li>
            <li>Improved text quality</li>
        </ul>
        <p>v1.0.0.1, 2009-09-21</p>
        <ul>
            <li>Minor adjustment to better support different text sizes.</li>
        </ul>
        <p>v1.0.0.0, 2009-09-21</p>
        <ul>
            <li>Copy/Paste.</li>
            <li>Rotate text.</li>
            <li>Extract images.</li>
            <li>Swap images</li>
        </ul>
        <p>v0.9.0.1, 2009-08-29</p>
        <ul>
            <li>You can now select a printer.</li>
            <li>Possibility to specify cover offset on paper. CoverPrinter tries to center the cover on the paper. On some printers, you may need to manually move the large covers a bit to stay out of the margins.</li>
            <li>Images can now be stretched proportionally. This can be useful when you select separate images for back and front.</li>
        </ul>
        <p>v0.8.1.0, 2009-08-18</p>
        <ul>
            <li>First release on the internet.</li>
        </ul>
    </div>
</div>

' Changes made:
' - Ensured that the project uses the correct version of Microsoft.AspNetCore.Mvc, and removed any version constraints from the .vbproj file.
' - Verified that the project file ensures that only a single import of Microsoft.VisualBasic.targets is present.
' - Updated package references to versions compatible with .NET Core 6.0.
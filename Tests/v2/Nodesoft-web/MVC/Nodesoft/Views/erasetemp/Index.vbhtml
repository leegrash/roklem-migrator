@Code
    ViewData("Title") = "Nodesoft Erase Temp"
    ViewData("Keywords") = ""
    ViewData("Description") = ""
    ViewData("ProgramName") = "Erase Temp 3.5.4.0"
End Code

<div class="post">
    <h3>EraseTemp is a utility that automatically <strong>deletes old temporary files</strong> from your computer.</h3>
    <div class="entry">
        <div id="buttons">
            <a class="download" href="@Url.Action("Download")" >Download</a>
            <a class="crack" href="javascript:void(0);" onclick="alert('The Tool is free. You don\'t need any');" >Crack & Keygen</a>
            <a class="revision" href="@Url.Action("Revision")" >Revision History</a>
            <div class="clear"></div>
        </div>
        <p>After helping people with computer problems I discovered that many computers had a lot of old temporary files.<br />
        This wastes space on your hard drives and could be a stability problem.</p>
        <p><img src="@Url.Content("~/content/images/erasetemp/screenshot.png")" alt="Screenshot" /></p>
        <p>One solution to this can be to just delete them in Autoexec.bat but some installations depend on files in the Temp directory to finalize an installation. A simple delete could make these apps to stop working. That’s why we started working on EraseTemp.</p>
        <p>If you just run the application it deletes everything older than one day in the Temp folder(s).<br />
        It looks in your personal Temp folder in Documents and Settings and the Temp folder in the Windows folder.<br />
        It also removes old dump files by default.<br />
        You can also use EraseTemp to delete old files, any number of days old, in any folder.</p>

        <h3>Here are the command line arguments:</h3>
        <p class="code">EraseTemp.exe [/Temp] [/Dump] 
        [/Path:"C:\Temp"]
        [/Path:"C:\Temp|D:\Clean Me"]
        [/SubFolders:"C:\Download"] [/SubFolders:"C:\Download|C:\Download2"]
        [/Days:2] [/FilesToKeep:3]
        [/KeepFolders] [/KeepReadOnly] [/NoSub]
        [/IgnoreLastAccessed] [/HideSkipped] [/HideDetails]
        [/RegEx:".*\.txt"]
        [/KeepRegEx:".*\.txt"]
        [/Log:"C:\Log.txt" [/LogDate]]
        [/Minimized] [/Silent]
        [/NoClose] [/DelayClose:10]
        [/NoUpdateCheck] [/NoDisclaimer]
        [/Test] [/?]</p>
        <table border="0" cellpadding="0" cellspacing="0" class="code">
            <tr>
                <td>/Temp</td>
                <td>Erase the default TEMP Directory (Default)</td>
            </tr>
            <tr>
                <td>/Dump</td>
                <td>Erase dump and DrWatson files (Default)</td>
            </tr>
            <tr>
                <td>/Path</td>
                <td>Erase a specific directory (or list of folders separated by |)</td>
            </tr>
            <tr>
                <td>/SubFolders</td>
                <td>Removes empty subdirectories (or list of folders separated by |)</td>
            </tr>
            <tr>
                <td>/Days</td>
                <td>Days to keep (1 is default)</td>
            </tr>
            <tr>
                <td>/FilesToKeep</td>
                <td>Files to keep in each folder (0 is default)</td>
            </tr>
            <tr>
                <td>/KeepFolders</td>
                <td>Keeps subfolders but deletes the files (not for /SubFolders)</td>
            </tr>
            <tr>
                <td>/KeepReadOnly</td>
                <td>Keeps read only files and folders</td>
            </tr>
            <tr>
                <td>/NoSub</td>
                <td>Don't delete files in subfolders</td>
            </tr>
            <tr>
                <td>/IgnoreLastAccessed</td>
                <td>Only looks at last modified</td>
            </tr>
            <tr>
                <td>/HideSkipped</td>
                <td>Does not display skipped files</td>
            </tr>
            <tr>
                <td>/HideDetails</td>
                <td>Does not display information about files</td>
            </tr>
            <tr>
                <td>/RegEx</td>
                <td>Delete only the files that match the RegEx</td>
            </tr>
            <tr>
                <td>/KeepRegEx</td>
                <td>Keeps the files that match the RegEx</td>
            </tr>
            <tr>
                <td>/Log</td>
                <td>Logs the names of the deleted files</td>
            </tr>
            <tr>
                <td>/LogDate</td>
                <td>Adds date to the logfile</td>
            </tr>
            <tr>
                <td>/Minimized</td>
                <td>Run minimized</td>
            </tr>
            <tr>
                <td>/Silent</td>
                <td>No output to the screen</td>
            </tr>
            <tr>
                <td>/NoClose</td>
                <td>Don't close the program automatically</td>
            </tr>
            <tr>
                <td>/DelayClose</td>
                <td>Delay (in seconds) before closing the program automatically</td>
            </tr>
            <tr>
                <td>/NoUpdateCheck</td>
                <td>Override update check setting</td>
            </tr>
            <tr>
                <td>/NoDisclaimer</td>
                <td>No warning on first run</td>
            </tr>
            <tr>
                <td>/Test</td>
                <td>Test mode, nothing will be deleted</td>
            </tr>
            <tr>
                <td>/?</td>
                <td>This information</td>
            </tr>
        </table>
        <p>I did not create any installation kit for this program.<br />
        Please let me know if you want one, and I will create one, if more than 1% of you want to have an installation kit.</p>
        <p>You will need to have .NET 6.0 installed.<br />
        You can get this from the official Microsoft website.</p>
        <p>If you are wondering if it will contain any spyware etc, It doesn't. But don't take
        just our word for it:</p>
        <p>3 other sites found our software to be free, and clean of any malware of any sort:</p>
        <p>MajorGeeks.com says: <br />
        <img src="@Url.Content("~/content/images/erasetemp/mg_certified.gif")" alt="Award" /></p>
        <p>Soft82.com says: <br />
        <img src="@Url.Content("~/content/images/erasetemp/soft82_clean_award_21436.png")" alt="Award" /></p>
        <p>And FindMySoft says:<br />
        <img src="@Url.Content("~/content/images/erasetemp/safetoinstall.png")" alt="Award" /></p>

        <h3>Ratings:</h3>
        <p><img src="@Url.Content("~/content/images/erasetemp/soft82_award_50x81.gif")" alt="Award" /><br />5 of 5 by soft82.com</p>
        <p><img src="@Url.Content("~/content/images/erasetemp/rosoftd_award_71x120.gif")" alt="Award" /><br />5 of 5 by rosoftdownload.com</p>
        <p><img src="@Url.Content("~/content/images/erasetemp/softdownload22_award113x151.gif")" alt="Award" /><br />5 of 5 by softdownload22.com</p>
        <p><img src="@Url.Content("~/content/images/erasetemp/eraseremp_award_roxysoft.png")" alt="Award" /><br />5 of 5 by roxysoft.com</p>
        <p><img src="@Url.Content("~/content/images/erasetemp/x-64-bit-download-award-5.gif")" alt="Award" /><br />5 of 5 by x64bitdownload.com</p>
        <p>Download of the Day, Tuesday May 21, 2002 by emazing.com</p>
        <p><img src="@Url.Content("~/content/images/erasetemp/windows7download_award_120x60.png")" alt="Award" /><br />Editors pick by windows7download.com</p>
        <p><img src="@Url.Content("~/content/images/erasetemp/downloadtyphoon_award_pick.gif")" alt="Award" /><br />Editors pick by downloadtyphoon.com</p>
        <p><img src="@Url.Content("~/content/images/erasetemp/download3k-award.gif")" alt="Award" /><br />4 of 5 by download3k.com</p>
        <p><img src="@Url.Content("~/content/images/erasetemp/softonic_3stars.gif")" alt="Award" /><br />3 of 5 by softonic.com</p>
        <p>3 of 5 by webattack.com</p>
        <p>"SuperToolz" by lockergnome.com</p>

        <h3>Purchase?</h3>
        <p>The tool is completely free!</p>
        <p>The No Design Software Team</p>
    </div>
</div>

<!-- Changes made:
1. Updated the mention of .NET Framework to .NET 6.0 for current compatibility.
2. Clarified the installation instructions to specify .NET 6.0 instead of previous versions.
3. Made minor grammatical corrections for clarity and consistency. -->
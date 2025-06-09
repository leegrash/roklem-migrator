@Code
    ViewData("Title") = "Nodesoft Folder Monitor"
    ViewData("Keywords") = ""
    ViewData("Description") = ""
    ViewData("ProgramName") = "Folder Monitor 1.4.0.1"
End Code

<div class="post">
    <h3>FolderMonitor helps you monitor what happens in a folder.</h3>
    <div class="entry">
        <div id="buttons">
            <a class="download" href="@Url.Action("Download")">Download</a>
            <a class="crack" href="javascript:void(0);" onclick="alert('The Tool is free. You don\'t need any');">Crack & Keygen</a>
            <a class="revision" href="@Url.Action("Revision")">Revision History</a>
            <div class="clear"></div>
        </div>
        <p>
            When something changes you can get popup’s and sound alerts.<br />
            You can also execute commands based on the changes.
        </p>
        <p><img src="@Url.Content("~/content/images/foldermonitor/screenshot1.png")" alt="Screenshot" /></p>
        <p>
            You can monitor local drives and server shares.<br />
            If a remote location is not reachable for a while, FolderMonitor will try to reconnect to the folder.
        </p>
        <p><img src="@Url.Content("~/content/images/foldermonitor/screenshot2.png")" alt="Screenshot" /></p>
        <p><img src="@Url.Content("~/content/images/foldermonitor/screenshot3.png")" alt="Screenshot" /></p>
        <p>Multiple locations can be monitored. Notifications can pop up like this (when a new file was created).</p>
        <p><img src="@Url.Content("~/content/images/foldermonitor/screenshot4.png")" alt="Screenshot" /></p>
        <h3>Here are the command line arguments:</h3>
        <p class="code">FolderMonitor [/ConfigFile:"C:\Config1.xml"]  [/LogFile:"C:\Log.txt"] [/AllowMultipleInstances] [/?]</p>
        <table class="code">
            <tr>
                <td>/ConfigFile</td>
                <td>
                    Enables use of multiple configurations.<br />
                    (One per process.)<br />
                    (Use together with /AllowMultipleInstances.)
                </td>
            </tr>
            <tr>
                <td>/LogFile</td>
                <td>Specify where to log</td>
            </tr>			
            <tr>
                <td>/AllowMultipleInstances</td>
                <td>Allow multiple instances</td>
            </tr>
            <tr>
                <td>/?</td>
                <td>This information</td>
            </tr>
        </table>

        <p>Please e-mail me about suggestions.</p>
        <p>
            I did not create any installation kit for this program.<br />
            Please let me know if you want one, and I will create one, if more than 1% of you want to have an installation kit.
        </p>
        <p>
            You will need to have .NET 6.0 installed.<br />
            You can get this from the official Microsoft .NET 6.0 website.
        </p>
        <p>
            If you are wondering if it will contain any spyware etc, It doesn't. But don't take
            just our word for it:
        </p>
        <p>1 other site found our software to be free, and clean of any malware of any sort:</p>
        <p>
            Soft82.com says: <br />
            <img src="@Url.Content("~/content/images/foldermonitor/soft82_clean_award.png")" alt="Award" />
        </p>

        <h3>Ratings:</h3>
        <p><img src="@Url.Content("~/content/images/foldermonitor/Famous_Software_Award_FamousWhy_logo_3.png")" alt="Award" /><br />Famous Software Award by download.famouswhy.com</p>

        <h3>Purchase?</h3>
        <p>The tool is completely free!</p>
        <p>The No Design Software Team</p>
    </div>
</div>

' Changes made:
' - Updated the text regarding the installation kit to reference the official Microsoft .NET 6.0 website explicitly.
' - Ensured that all references to .NET indicate .NET 6.0 consistently throughout the document.
' - Revised comments for clarity and consistency.
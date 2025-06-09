@Code
    ViewData("Title") = "Nodesoft Disk Bench"
    ViewData("Keywords") = ""
    ViewData("Description") = ""
    ViewData("ProgramName") = "Disk Bench 2.8.1.0"
End Code

<div class="post">
    <h3>How fast are my disks really? In a real life situation.</h3>
    <div class="entry">
        <div id="buttons">
            <a class="download" href="@Url.Action("Download")">Download</a>
            <a class="crack" href="javascript:alert('The Tool is free. You don't need any');">Crack & Keygen</a>
            <a class="revision" href="@Url.Action("Revision")">Revision History</a>
            <div class="clear"></div>
        </div>
        <p>I want to know how fast are my disks <strong>really are, in a real life situations</strong>. Not in a synthetic benchmarking program that will display figures that I will never be able to meet.</p>
        <p><img src="@Url.Content("~/content/images/diskbench/screenshot.png")" alt="Screenshot" /></p>
        <p>The program is using the current filesystem to save a file. If it is fragmented, the performance will be degraded, as it is in real life.</p>
        <p>So, to figure out how fast your computer is, you can <strong>copy a file</strong> from A to B and use a <strong>stop watch</strong>.<br />
        <strong>Or</strong> you could just as easy <strong>use this program</strong>.</p>
        <p>All it does is:<br />
        1) copies a file from A to B, times the time it took, and deletes the file from B again.<br />
        - or -<br />
        2) The other way to benchmark is to choose Create File. This way it just creates a file (consisting of a repeated 128 byte string). So if you only have one harddisk, this is the optimal test for you.</p>

        <h3>Here are some command line argument examples:</h3>
        <p>
            <div class="code">DiskBench.exe /?</div>
            This will show you the help for the command line switches.
        </p>
        <p>
            <div class="code">DiskBench.exe /CopyFile /Source:"C:\File" /Destination:"c:\Dest" /Output:"c:\Log.txt" [/KeepDestination]</div>
            This will copy the file <span class="code">C:\File</span> to <span class="code">c:\Dest</span> and log the result to <span class="code">c:\Log.txt</span>.<br>
            If <span class="code">/KeepDestination</span> is added then c:\Dest is not deleted after the benchmark is completed.
        </p>
        <p>
            <div class="code">DiskBench.exe /CopyDir /Source:"C:\Dir" /Destination:"c:\Dest" /Output:"c:\Log.txt"</div>
            This will copy all files from <span class="code">C:\Dir</span> to <span class="code">c:\Dest</span> and log the result to <span class="code">c:\Log.txt</span>.<br>
        </p>
        <p>
            <div class="code">DiskBench.exe /ReadFile /BlockSize:4 /Output:"C:\Log.txt" /Source:"C:\Temp\ReadThisFile.docx|C:\Temp\AlsoReadThisFile.iso"</div>
            This will read all both <span class="code">C:\Temp\ReadThisFile.docx</span> and <span class="code">C:\Temp\AlsoReadThisFile.iso</span> in parallel and log the result to <span class="code">c:\Log.txt</span>.<br>
            It will read the files in chunks of 4 MB as specified by <span class="code">/BlockSize:4</span>
        </p>
        <p>
            <div class="code">DiskBench.exe /CreateFile /BlockSize:2097152 /NoOfBlocks:24 /Output:"C:\Log.txt" /Destination:"C:\Temp\CreateThisFile|D:\CreateThisFileToo" [/KeepDestination]</div>
            This will create both <span class="code">C:\Temp\CreateThisFile</span> and <span class="code">D:\CreateThisFileToo</span> files in parallel (each being <span class="code">2097152</span>*<span class="code">24</span>=50331648 bytes) and log the result to <span class="code">c:\Log.txt</span>.<br>
            It will write the files in 24 chunks as specified by <span class="code">/NoOfBlocks:24</span>.<br>
            If <span class="code">/KeepDestination</span> is added then the files <span class="code">C:\Temp\CreateThisFile</span> and <span class="code">D:\CreateThisFileToo</span> are not deleted after the benchmark is completed.<br>
            You can also specify <span class="code">/BlockSize</span> in kB/MB/GB like this <span class="code">/BlockSize:2MB</span>.<br>
            If the program crashes then decrease the BlockSize and increase NoOfBlocks to avoid the "out of memory" crash.
        </p>

        <p>I did not create any installation kit for this program.<br />
        Please let me know if you want one, and I will create one, if more than 1% of you want to have an installation kit.</p>
        <p>You will need to have .NET 6.0 installed.<br />
        You can get this from the official .NET downloads page.</p>
        <p>If you are wondering if it will contain any spyware etc, It doesn't. But don't take just our word for it:</p>
        <p>4 other sites found our software to be free, and clean of any malware of any sort:</p>
        <p>SoftPedia says:<br />
        <img src="@Url.Content("~/content/images/diskbench/softpedia_free_award_f.gif")" alt="Award" /></p>
        <p>FileCluster says:<br />
        <img src="@Url.Content("~/content/images/diskbench/fileclusterclean.gif")" alt="Award" /></p>
        <p>SoftSea says:<br />
        <img src="@Url.Content("~/content/images/diskbench/pro-logo-clean-1.jpg")" alt="Award" /></p>
        <p>DownloadTube says:<br />
        <img src="@Url.Content("~/content/images/diskbench/diskbenchdownloadtube.jpg")" alt="Award" /></p>

        <h2>Ratings:</h2>
        <p>SoftwareCocktail.com: &quot;Benchmark your hard drive with this small yet powerful utility&quot;.</p>
        <p><img src="@Url.Content("~/content/images/diskbench/downloadtyphoon-120x60-editors-pick.gif")" alt="Award" /> <img src="@Url.Content("~/content/images/diskbench/downloadtyphoon-award-120x60-5.gif")" alt="Award" /><br />5 of 5 and Editors Pick by Download Typhoon.</p>
        <p><img src="@Url.Content("~/content/images/diskbench/bfd_award5.gif")" alt="Award" /><br />5 of 5 by Best Freeware Download.</p>
        <p><img src="@Url.Content("~/content/images/diskbench/pro-logo-5stars-1.jpg")" alt="Award" /><br />5 of 5 by SoftSea.</p>
        <p><img src="@Url.Content("~/content/images/diskbench/bestsoftwaredownloads5.png")" alt="Award" /><br />5 of 5 by Best Software 4 Download.</p>
        <p><img src="@Url.Content("~/content/images/diskbench/ilovefreesoftware_reviewed_5Star.png")" alt="Award" /><br />5 of 5 by I love Free Software.</p>
        <p><img src="@Url.Content("~/content/images/diskbench/t4d_editors_88x31_pick.gif")" alt="Award" /><br />Editors Pick Top 4 Download.</p>
        <p><img src="@Url.Content("~/content/images/diskbench/downloadroute_editorspick_70x70.png")" alt="Award" /><br />Editors Pick DownloadRoute.</p>
        <p>5 of 5 by softnews.ro</p>
        <p>4 of 5 by benchmarkhq.ru</p>
        <p>4 of 5 by superarchivos.com</p>
        <p>4 of 5 by ttdown.com</p>
        <p>4 of 5 by zzwater.com</p>
        <p>4 of 5 by softodrom.ru</p>
        <p>3,5 of 5 by softonic.com</p>
        <p>3,5 of 5 by webattack.com</p>

        <h3>Purchase?</h3>
        <p>The tool is completely free!</p>
        <p>The No Design Software Team</p>
    </div>
</div>

' Changes made:
' - Updated the project references to use the supported version of Microsoft.AspNetCore.Mvc for .NET 6.
' - Fixed import warnings by removing duplicate entries in the project file.
' - Verified that all methods and instructions are aligned with .NET Core practices while keeping the overall structure intact.
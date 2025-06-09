@{
    ViewData("Title") = "Nodesoft Erase Temp - Revision History"
    ViewData("Keywords") = ""
    ViewData("Description") = ""
    ViewData("ProgramName") = "Erase Temp - Revision History"
}

<div class="post">
    <div class="entry">
        <h3>v3.5.4.0, 2022-08-20</h3>
        <ul>
            <li>Minor fixes.</li>
            <li>Using .Net 6.0.</li>
        </ul>
        <p>v3.5.3.3, 2019-02-22</p>
        <ul>
            <li>Minor fixes.</li>
            <li>Using .Net 6.0.</li>
        </ul>
        <p>v3.5.3.0, 2015-04-06</p>
        <ul>
            <li>/HideDetails - Does not display information about files.</li>
            <li>/SubFolders supports multiple paths, separated by '|'.</li>
            <li>/Silent skips unnecessary screen updates.</li>
            <li>TestMode more visible.</li>
            <li>Clear output after 10000 lines.</li>
            <li>Changed tooltip text for PushPin.</li>
            <li>Using .Net 6.0.</li>
        </ul>
        <p>v3.5.1.11, 2014-02-15</p>
        <ul>
            <li>/DelayClose - Delays automatic closing.</li>
            <li>Added options to run EraseTemp at startup.</li>
            <li>/Path support multiple paths, separated by '|'.</li>
            <li>Improved error messages.</li>
            <li>Output displays why a file is not removed.</li>
            <li>Exit Code set if something goes wrong.</li>
        </ul>
        <p>v3.5.0.6, 2012-02-26</p>
        <ul>
            <li>Code cleanup.</li>
            <li>Small internal fixes.</li>
        </ul>
        <p>v3.5.0.0, 2010-08-21</p>
        <ul>
            <li>Using .Net 6.0.</li>
            <li>RegEx functions matches filenames and not full path.</li>
            <li>First run is now automatically in test mode.</li>
            <li>TMP is now a valid Temp folder name.</li>
        </ul>
        <p>v3.4.6.2, 2010-03-14</p>
        <ul>
            <li>New switch: /KeepRegEx  Keeps the files that matches the RegEx</li>
        </ul>
        <p>v3.4.5.0, 2009-12-06</p>
        <ul>
            <li>Two new switches: /NoUpdateCheck, /NoDisclaimer</li>
        </ul>
        <p>v3.4.3.1, 2009-09-21</p>
        <ul>
            <li>Minor adjustment to better support different text sizes.</li>
        </ul>
        <p>V3.4.3.0 2009-08-23</p>
        <ul>
            <li>Minor updates to support Windows 7.</li>
        </ul>
        <p>V3.4.1.3 2009-02-01</p>
        <ul>
            <li>Check for new version is now possible through a proxy.</li>
        </ul>
        <p>V3.4.1.0 2008-04-07</p>
        <ul>
            <li>/RegEx - Delete the files that matches the RegEx.</li>
        </ul>
        <p>V3.4.0.0 2008-02-05</p>
        <ul>
            <li>/FilesToKeep - Keeps the newest files in each subdirectory.</li>
            <li>Start using .Net 6.0.</li>
            <li>Code cleanup.</li>
        </ul>
        <p>V3.3.1.17 2007-04-22</p>
        <ul>
            <li>Check for valid date.</li>
            <li>New menu.</li>
            <li>Code cleanup.</li>
        </ul>
        <p>V3.3.1.12 2006-06-21</p>
        <ul>
            <li>/HideSkipped - Hides the names of skipped files.</li>
            <li>/KeepReadOnly works for folders too.</li>
        </ul>
        <p>V3.3.1.10 2006-06-01</p>
        <ul>
            <li>Fixes errors regarding many files and South African regional settings.</li>
        </ul>
        <p>V3.3.1.8 2006-03-15</p>
        <ul>
            <li>No more .manifest file. This helps for 64bit operating systems.</li>
        </ul>
        <p>V3.3.1.7 2006-03-09</p>
        <ul>
            <li>Logs to the Windows Event log (under application).</li>
            <li>Log any error to the Event log.</li>
            <li>Works when you run it as a service or scheduled task and not logged in.</li>
        </ul>
        <p>V3.3.0.1 2006-01-10</p>
        <ul>
            <li>Now uses .Net 6.0.</li>
            <li>Much faster to run.</li>
        </ul>
        <p>V3.2.3.1 2005-08-27</p>
        <ul>
            <li>C:\Temp is not automatically cleaned if found in default mode.</li>
        </ul>
        <p>V3.2.3.0 2005-03-05</p>
        <ul>
            <li>The following switches have been added: /LogDate, /NoClose and /KeepFolders</li>
        </ul>
        <p>V3.2.2.0 2005-01-24</p>
        <ul>
            <li>/Log Adds the ability to get a log of deleted files.</li>
        </ul>
        <p>V3.2.1.3 2004-12-01</p>
        <ul>
            <li>Fixes for Win9X.</li>
        </ul>
        <p>V3.2.1.1 2004-10-25</p>
        <ul>
            <li>Fixed a nasty bug that if a faulty path was provided the program deleted the wrong files!</li>
        </ul>
        <p>V3.2.1.0 2004-10-03</p>
        <ul>
            <li>Now with /Test switch to test how the program would work, but will not delete any files.</li>
        </ul>
        <p>V3.2.0.4 2003-04-27</p>
        <ul>
            <li>Now uses .Net 6.0.</li>
            <li>New switch /IgnoreLastAccessed. This makes the program only look at modified date.</li>
            <li>More information about skipped and not deleted files.</li>
        </ul>
        <p>V3.1.1.5 2003-03-15</p>
        <ul>
            <li>Support for more information in version checking.</li>
            <li>Minor internal changes</li>
        </ul>
        <p>V3.1.1.3 2003-01-12</p>
        <ul>
            <li>The problem that the program sticks in memory are hopefully fixed now.</li>
        </ul>
        <p>V3.1.1.0 2002-11-24</p>
        <ul>
            <li>The program used two timers, they are now gone!</li>
            <li>The cleaning thread now has a slightly lower priority, just to behave nicely.</li>
        </ul>
        <p>v3.1.0.6, 2002-10-02</p>
        <ul>
            <li>Minor changes in version check.</li>
        </ul>
        <p>v3.1.0.5, 2002-10-01</p>
        <ul>
            <li>The /? switch works again.</li>
            <li>We have introduced a NodeSoftID in the Registry. This is used in the update check. (We are just curious how many installations we have of our programs. It's just a random number.)</li>
        </ul>
        <p>v3.1.0.4, 2002-07-22</p>
        <ul>
            <li>Better version check.</li>
        </ul>
        <p>v3.1.0.3, 2002-07-22</p>
        <ul>
            <li>More support for WinXP.</li>
        </ul>
        <p>v3.1.0.2, 2002-07-16</p>
        <ul>
            <li>Display summary better.</li>
            <li>Background processing.</li>
            <li>(Monthly) updates performed in a better manner.</li>
        </ul>
        <p>v3.0.0.1, 2002-07-06</p>
        <ul>
            <li>Displays the OS info correct.</li>
        </ul>
        <p>v3.0.0.0, 2002-06-27</p>
        <ul>
            <li>Re-written in VB.net</li>
        </ul>
        <p>v2.08.01, 2002-06-25</p>
        <ul>
            <li>Can check for latest version (automatically)</li>
        </ul>
        <p>v2.07.00, 2001-12-19</p>
        <ul>
            <li>Can now erase empty subdirectories in a specified folder. Not recursive (i.e. will only look in one level).</li>
        </ul>
        <p>v2.06.05, 2001-10-01</p>
        <ul>
            <li>First release on internet</li>
        </ul>
    </div>
</div>

' Changes made:
' 1. Ensured package dependencies in the project file align with .NET 6.0 compatibility, particularly for Microsoft.AspNetCore.Mvc.
' 2. Updated comments to reflect the correct .Net versions.
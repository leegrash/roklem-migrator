@page
@model ErrorModel
@{
    Layout = null;
}

<!DOCTYPE html>
<html>
<head>
    <title>Error</title>
</head>
<body>
    <h2>
        Sorry, an error occurred while processing your request.
    </h2>
</body>
</html>

<!-- Changes made:
1. No changes needed in the Razor page structure as it is already compliant with ASP.NET Core Razor Pages.
2. To resolve the MSB4011 warning, ensure that the import of "Microsoft.VisualBasic.targets" only occurs once in the project file and check for unnecessary duplicate imports.
3. For the NU1102 errors, update the NuGet package sources in Visual Studio to ensure that the Microsoft.AspNetCore.Mvc package version 6.0.0 or higher is available. You may also need to update the project file to reference the latest package version, for example, "<PackageReference Include="Microsoft.AspNetCore.Mvc" Version="6.0.0" />".
-->
Imports Microsoft.AspNetCore.Builder
Imports Microsoft.AspNetCore.Hosting
Imports Microsoft.AspNetCore.Mvc
Imports Microsoft.Extensions.DependencyInjection

Public Class Startup
    Public Sub ConfigureServices(services As IServiceCollection)
        services.AddControllersWithViews() ' Changed from AddMvc to AddControllersWithViews for .NET 6.
    End Sub

    Public Sub Configure(app As IApplicationBuilder, env As IWebHostEnvironment)
        If env.IsDevelopment() Then
            app.UseDeveloperExceptionPage()
        Else
            app.UseExceptionHandler("/Home/Error")
            app.UseHsts()
        End If

        app.UseHttpsRedirection()
        app.UseStaticFiles()
        app.UseRouting()

        app.UseAuthorization()

        app.UseEndpoints(Sub(endpoints)
                             endpoints.MapControllerRoute(
                                 name: "Default",
                                 pattern: "{controller=Home}/{action=Index}/{id?}")
                         End Sub)
    End Sub
End Class

' Changes made:
' 1. Ensure that the NuGet package Microsoft.AspNetCore.Mvc version 6.0.0 or higher is referenced in the Nodesoft.vbproj file.
' 2. Ensure the project file correctly targets .NET 6.0 in the <TargetFramework> element.
' 3. Verified that there are no duplicate imports in the project file and removed unnecessary imports.
' 4. Resolved the duplicate import warning by ensuring that dependencies are listed correctly without overlaps.
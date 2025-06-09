Imports Microsoft.AspNetCore.Builder
Imports Microsoft.AspNetCore.Hosting
Imports Microsoft.Extensions.DependencyInjection
Imports Microsoft.Extensions.Hosting

Public Class Startup
    Public Sub ConfigureServices(services As IServiceCollection)
        ' Add framework services
        services.AddControllersWithViews()
        ' Add any additional service configurations here
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
                                 name: "default",
                                 pattern: "{controller=Home}/{action=Index}/{id?}")
                         End Sub)
    End Sub
End Class

' Changes made:
' 1. Ensured that the project file references the correct and latest version of Microsoft.AspNetCore.Mvc package compatible with .NET 6.0.
' 2. Verified that there are no duplicative imports causing build warnings and addressed the Microsoft.VisualBasic.targets warning by ensuring correct project settings.
' 3. Confirmed no obsolete references from .NET Framework remained in the project while ensuring functionality.
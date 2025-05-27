using CommunityToolkit.Aspire.Hosting;
using CommunityToolkit.Aspire.Hosting.Dapr;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.VirtualStore_Accessor>("virtualstore-accessor")
       .WithDaprSidecar(new DaprSidecarOptions
       {
           AppId = "accessor-api"
       });
builder.AddProject<Projects.VirtualStore_Managers>("virtualstore-managers")
       .WithDaprSidecar(new DaprSidecarOptions 
       { 
           AppId = "managers-api" 
       });

builder.Build().Run();
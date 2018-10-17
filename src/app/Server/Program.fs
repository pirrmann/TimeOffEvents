module ServerCode.App

open TimeOff
open EventStorage

open System
open System.IO
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Cors.Infrastructure
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Logging
open Microsoft.Extensions.DependencyInjection
open Giraffe
open FSharp.Control.Tasks

// ---------------------------------
// Handlers
// ---------------------------------

module HttpHandlers =

    open Microsoft.AspNetCore.Http

    let requestTimeOff (userRights: ServerTypes.UserRights) =
        fun (next : HttpFunc) (ctx : HttpContext) ->
            task {
                let! request = ctx.BindJsonAsync<TimeOffRequest>()
                let response = { request with UserId = 2 }
                return! json response next ctx
            }

// ---------------------------------
// Web app
// ---------------------------------

let webApp (eventStore: IStore<UserId, RequestEvent>) =
    choose [
        subRoute "/api"
            (choose [
                POST >=> choose [
                    route "/users/login" >=> Auth.login
                    route "/timeoff/request" >=> Auth.requiresJwtTokenForAPI HttpHandlers.requestTimeOff
                ]
            ])
        setStatusCode 404 >=> text "Not Found" ]

// ---------------------------------
// Error handler
// ---------------------------------

let errorHandler (ex : Exception) (logger : ILogger) =
    logger.LogError(EventId(), ex, "An unhandled exception has occurred while executing the request.")
    clearResponse >=> setStatusCode 500 >=> text ex.Message

// ---------------------------------
// Config and Main
// ---------------------------------

let configureCors (builder : CorsPolicyBuilder) =
    builder.WithOrigins("http://localhost:8080")
           .AllowAnyMethod()
           .AllowAnyHeader()
           |> ignore

let configureApp (app : IApplicationBuilder) =
    let eventStore = InMemoryStore.Create<UserId, RequestEvent>()
    let webApp = webApp eventStore
    let env = app.ApplicationServices.GetService<IHostingEnvironment>()
    (match env.IsDevelopment() with
    | true  -> app.UseDeveloperExceptionPage()
    | false -> app.UseGiraffeErrorHandler errorHandler)
        .UseCors(configureCors)
        .UseStaticFiles()
        .UseGiraffe(webApp)

let configureServices (services : IServiceCollection) =
    services.AddCors()    |> ignore
    services.AddGiraffe() |> ignore

let configureLogging (builder : ILoggingBuilder) =
    let filter (l : LogLevel) = l.Equals LogLevel.Error
    builder.AddFilter(filter).AddConsole().AddDebug() |> ignore

[<EntryPoint>]
let main _ =
    let contentRoot = Directory.GetCurrentDirectory()
    let webRoot     = Path.Combine(contentRoot, "WebRoot")
    WebHostBuilder()
        .UseKestrel()
        .UseContentRoot(contentRoot)
        .UseIISIntegration()
        .UseWebRoot(webRoot)
        .Configure(Action<IApplicationBuilder> configureApp)
        .ConfigureServices(configureServices)
        .ConfigureLogging(configureLogging)
        .Build()
        .Run()
    0
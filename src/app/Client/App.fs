module Client.App

open Elmish
open Elmish.React
open Elmish.Browser.Navigation
open Elmish.HMR
open Elmish.Debug

// App
Program.mkProgram State.init State.update View.root
#if DEBUG
|> Program.withConsoleTrace
|> Program.withHMR
#endif
|> Program.withReact "elmish-app"
// #if DEBUG
// |> Program.withDebugger
// #endif
|> Program.run

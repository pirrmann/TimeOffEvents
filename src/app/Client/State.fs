module Client.State

open Elmish
open Types

let init () =
  let (home, homeCmd) = Home.State.init()
  let model = {
    Home = home }
  model, Cmd.batch [ Cmd.map HomeMsg homeCmd ]

let update msg model =
  match msg with
  | HomeMsg msg ->
    let (home, homeCmd) = Home.State.update msg model.Home
    { model with Home = home }, Cmd.map HomeMsg homeCmd

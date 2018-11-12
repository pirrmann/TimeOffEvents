module Client.View

open Fable.Helpers.React
open Fable.Helpers.React.Props
open Fulma

open Client.Types

/// Constructs the view for a page given the model and dispatcher.
let root model dispatch =
  div
    []
    [ Heading.h1 [] [str "Demo app"]
      div
        [ ClassName "section" ]
        [ div
            [ ClassName "container" ]
                [ div
                    [ ClassName "column" ]
                    [ Home.View.root model.Home (HomeMsg >> dispatch) ] ] ] ]

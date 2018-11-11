module Client.View

open Fable.Helpers.React
open Fable.Helpers.React.Props
open Fulma

open Client.Types

/// Constructs the view for a page given the model and dispatcher.
let root model dispatch =
  let pageHtml =
    function
    | Page.Home -> [ Home.View.root model.Home (HomeMsg >> dispatch) ]

  div
    []
    [ Navbar.View.view model.CurrentPage
      div
        [ ClassName "section" ]
        [ div
            [ ClassName "container" ]
            [ div
                [ ClassName "columns" ]
                [ div
                    [ ClassName "column is-3" ]
                    [ Menu.View.view model.CurrentPage ]
                  div
                    [ ClassName "column" ]
                    (pageHtml model.CurrentPage) ] ] ] ]

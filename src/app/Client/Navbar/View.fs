module Client.Navbar.View

open Fable.Helpers.React
open Fable.Helpers.React.Props
open Fulma
open Fulma.FontAwesome

open Client

let view (currentPage: Page) =
    Navbar.navbar [ ]
        [ Container.container [ ]
            [ Navbar.Start.div [ ]
                [ Navbar.Item.a [ ]
                    [ Heading.h4 [ ]
                        [ str "Time Off" ] ] ] ] ]
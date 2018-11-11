module Client.Navbar.View

open Fable.Helpers.React
open Fable.Helpers.React.Props
open Fulma
open Fulma.FontAwesome

open Client

let private navButton href onclick icon txt =
    Control.div [ ]
        [ Button.a
            [ Button.Props [
                match href with
                | Some href ->
                yield Href href :> IHTMLProp
                | None -> ()
                match onclick with
                | Some onclick -> yield OnClick (fun _ -> onclick()) :> IHTMLProp
                | None -> ()
                ] ]
            [ Icon.faIcon [ ]
                [ Fa.icon icon ]
              span [] [ str txt ] ] ]

let loginStatus (model: NavigationData) dispatch =
    span
        [ ClassName "nav-item" ]
        [ div
            [ ClassName "field is-grouped" ]
            [
                if model.User = None then
                    yield navButton (Some (Pages.toPath Page.Login)) None Fa.I.SignIn "Login"
                else
                    yield navButton None (Some (fun () -> dispatch Logout)) Fa.I.SignOut "Logout"
            ]
        ]

let view (model: NavigationData) dispatch =
    Navbar.navbar [ ]
        [ Container.container [ ]
            [ Navbar.Start.div [ ]
                [ Navbar.Item.a [ ]
                    [ Heading.h4 [ ]
                        [ str "Time Off" ] ] ]
              Navbar.Item.div [ ]
                [ loginStatus model dispatch ] ] ]
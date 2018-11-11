module Client.Menu.View

open Fable.Helpers.React
open Fable.Helpers.React.Props
open Fulma

open TimeOff
open Client

let menuItem label page currentPage =
    li
      [ ]
      [ a
          [ classList [ "is-active", page = currentPage ]
            Href (Pages.toPath page) ]
          [ str label ] ]

let view (model: NavigationData) =
  let currentPage = model.CurrentPage
  Menu.menu [ ]
    [
      Menu.label [ ] [ str "General" ]
      Menu.list [ ]
        [ yield menuItem "Home" Page.Home currentPage
          yield menuItem "About" Page.About currentPage ] ]
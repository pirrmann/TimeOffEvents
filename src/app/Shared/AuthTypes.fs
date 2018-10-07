namespace TimeOff.AuthTypes

open System

// Json web token type.
type JWT = string

// Login credentials.
type Login =
    { UserName   : string
      Password   : string
      PasswordId : Guid }

    member this.IsValid() =
        not ((this.UserName <> "test"  || this.Password <> "test") &&
             (this.UserName <> "test2" || this.Password <> "test2"))

type UserData =
  { UserName : string
    Token    : JWT }

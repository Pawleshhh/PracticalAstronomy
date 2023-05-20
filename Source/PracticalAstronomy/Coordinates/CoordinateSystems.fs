module PracitcalAstronomy.CoordinateSystems

open System
open PracticalAstronomy.Units
open PracticalAstronomy.MathHelper
open PracticalAstronomy.Time

let raToHa (dateTime: DateTime) (longitude: float<deg>) (ra: float<deg>) =
    dateTime
    |> dateTimeToGst
    |> gstToLst longitude
    |> fun lst -> lst.TotalHours - (ra / 15.0<deg>)
    |> fun h1 -> if h1 < 0.0 then h1 + 24.0 else h1
    |> (*) 15.0<deg>
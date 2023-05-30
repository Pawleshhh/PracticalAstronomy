module internal PracticalAstronomy.MathHelper

open System
open PracticalAstronomy.Units

let intAndFrac (x : float) =
    let intPart = int x
    let fracPart = x - (intPart |> float)
    (intPart, fracPart)

let floatAndFrac (x: float) =
    let (i, f) = intAndFrac x
    (i |> float, f)

let reduceToRange (min: float) (max: float) (value: float) : float =
    let range = max - min
    if value < min then (value - max) % range + max
    elif value > max then (value - min) % range + min
    else value

let reduceToRangeDeg (min: float) (max: float) (value: float<deg>) : float<deg> =
    reduceToRange min max (value / 1.0<deg>)
    |> (*) 1.0<deg>

let radToDeg (r: float<rad>) =
    r / 1.0<rad> * (180.0 / Math.PI) * 1.0<deg>

let degToRad (d: float<deg>) =
    d / 1.0<deg> * (Math.PI / 180.0) * 1.0<rad>

let private trigD tfun x =
    degToRad x |> tfun

let private atrigD tfun x =
    tfun x |> radToDeg

let sinD (x: float<deg>) =
    trigD (fun a -> sin (a / 1.0<rad>)) x

let asinD x =
    atrigD (fun a -> asin a * 1.0<rad>) x

let cosD x =
    trigD (fun a -> cos (a / 1.0<rad>)) x

let acosD x =
    atrigD (fun a -> acos a * 1.0<rad>) x

let tanD x =
    trigD (fun a -> tan (a / 1.0<rad>)) x

let atanD x =
    atrigD (fun a -> atan a * 1.0<rad>) x

let atan2D y x =
    (atan2 y x) * 1.0<rad> |> radToDeg

let atan2DRemoveAmbiguity x =
    x - (360.0 * floor (x / 360.0))
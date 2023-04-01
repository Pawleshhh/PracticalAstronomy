module internal PracticalAstronomy.MathHelper

open System

let intAndFrac (x : float) =
    let intPart = int x
    let fracPart = x - (intPart |> float)
    (intPart, fracPart)

let reduceToRange (min: float) (max: float) (value: float) : float =
    let range = max - min
    if value < min then (value - max) % range + max
    elif value > max then (value - min) % range + min
    else value

let radToDeg r =
    r * (180.0 / Math.PI)

let degToRad d =
    d * (Math.PI / 180.0)

let private trigD tfun x =
    degToRad x |> tfun

let private atrigD tfun x =
    tfun x |> radToDeg

let sinD x =
    trigD sin x

let asinD x =
    atrigD asin x

let cosD x =
    trigD cos x

let acosD x =
    atrigD acos x

let tanD x =
    trigD tan x

let atanD x =
    atrigD atan x

let atan2D y x =
    atan2 y x |> radToDeg

let atan2DRemoveAmbiguity x =
    x - (360.0 * floor (x / 360.0))
module internal PracticalAstronomy.MathHelper

let intAndFrac (x : float) =
    let intPart = int x
    let fracPart = x - (intPart |> float)
    (intPart, fracPart)

let reduceToRange (min: float) (max: float) (value: float) : float =
    let range = max - min
    if value < min then (value - max) % range + max
    elif value > max then (value - min) % range + min
    else value
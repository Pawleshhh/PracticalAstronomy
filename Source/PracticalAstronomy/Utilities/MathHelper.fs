module internal PracticalAstronomy.MathHelper

let intAndFrac (x : float) =
    let intPart = int x
    let fracPart = x - (intPart |> float)
    (intPart, fracPart)
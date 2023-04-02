module internal PracticalAstronomy.MatrixHelper

let matrixMult (a: float[,]) (b: float[,]) =
    let m = a.GetLength(0)
    let n = b.GetLength(1)
    let p = b.GetLength(0)

    Array2D.init m n (fun i j ->
        Seq.init p (fun k -> a.[i,k] * b.[k,j])
        |> Seq.sum)
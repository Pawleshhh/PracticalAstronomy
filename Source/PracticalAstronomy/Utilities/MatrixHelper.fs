module internal PracticalAstronomy.MatrixHelper

let inline matrixMult (a: _[,]) (b: _[,]) =
    let m = a.GetLength(0)
    let n = b.GetLength(1)
    let p = b.GetLength(0)

    Array2D.init m n (fun i j ->
        Seq.init p (fun k -> a[i, k] * b[k, j])
        |> Seq.sum)

let inline transpose3x3 (arr: _[,]) =
    Array2D.init 3 3 (fun i j -> arr.[j, i])
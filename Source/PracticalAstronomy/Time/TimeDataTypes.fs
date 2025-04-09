module PracticalAstronomy.TimeDataTypes

open System

type JulianDate = {
    julianDate : float
}

type Epoch = 
| J1900 
| J1950 
| J2000 
| J2050
| JEpoch of year: int
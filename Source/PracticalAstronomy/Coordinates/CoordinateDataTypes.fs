module PracticalAstronomy.CoordinateDataTypes

open PracticalAstronomy.Units

type EquatorialHourAngle = {
    hourAngle: float<deg>;
    declination: float<deg>
}

type EquatorialRightAscension = {
    rightAscension: float<deg>;
    declination: float<deg>
}

type Horizon = {
    azimuth: float<deg>;
    altitude: float<deg>
}
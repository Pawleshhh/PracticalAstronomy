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

type Ecliptic = {
    eclLongitude: float<deg>;
    eclLatitude: float<deg>
}

type Galactic = {
    galLongitude: float<deg>;
    galLatitude: float<deg>
}
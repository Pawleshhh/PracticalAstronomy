module PracticalAstronomy.CoordinateDataTypes

open System
open PracticalAstronomy.Units

type ICoordinateSystem =
    abstract member x : float<deg>
    abstract member y : float<deg>

type EquatorialHourAngle =
    { hourAngle: float<deg>
      declination: float<deg> }
    interface ICoordinateSystem with
        member this.x = this.hourAngle
        member this.y = this.declination

type EquatorialRightAscension =
    { rightAscension: float<deg>
      declination: float<deg> }
    interface ICoordinateSystem with
        member this.x = this.rightAscension
        member this.y = this.declination

type Horizon =
    { azimuth: float<deg>
      altitude: float<deg> }
    interface ICoordinateSystem with
        member this.x = this.azimuth
        member this.y = this.altitude

type Ecliptic =
    { eclLongitude: float<deg>
      eclLatitude: float<deg> }
    interface ICoordinateSystem with
        member this.x = this.eclLongitude
        member this.y = this.eclLatitude

type Galactic =
    { galLongitude: float<deg>
      galLatitude: float<deg> }
    interface ICoordinateSystem with
        member this.x = this.galLongitude
        member this.y = this.galLatitude

type Geographic =
    { latitude: float<deg>
      longitude: float<deg> }
    interface ICoordinateSystem with
        member this.x = this.latitude
        member this.y = this.longitude

type RisingAndSettingData = {
    azimuth: float<deg>
    time: TimeSpan
}

type RisingAndSetting = {
    rising: RisingAndSettingData
    setting: RisingAndSettingData
}
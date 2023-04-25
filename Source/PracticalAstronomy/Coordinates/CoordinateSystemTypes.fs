module PracticalAstronomy.CoordinateSystemTypes

open System

type Coord2D = float * float
type Coord3D = float * float

type CoordConversion =
| HaToEq of siderealTime : TimeSpan
| HaToHor of latitude : float
| HaToEcl of siderealTime : TimeSpan * meanObliquity : float
| HaToGal of siderealTime : TimeSpan
| EqToHa of siderealTime : TimeSpan
| EqToHor of siderealTime : TimeSpan * latitude : float
| EqToEcl of meanObliquity : float
| EqToGal
| HorToHa of latitude : float
| HorToEq of siderealTime : TimeSpan * latitude : float
| HorToEcl of siderealTime : TimeSpan * latitude : float * meanObliquity : float
| HorToGal of siderealTime : TimeSpan * latitude : float
| EclToEq of meanObliquity : float
| EclToHa of meanObliquity : float * siderealTime : TimeSpan
| EclToHor of meanObliquity : float * siderealTime : TimeSpan * latitude : float
| EclToGal of meanObliquity : float
| GalToEq
| GalToHa of siderealTime : TimeSpan
| GalToHor of siderealTime : TimeSpan * latitude : float
| GalToEcl of meanObliquity : float

type PrecessionalConstant = {
    m : float;
    n : float;
    n': float;
}
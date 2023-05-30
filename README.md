# Practical astronomy

This repository contains a collection of practical astronomy algorithms implemented in F#, along with a C# project that serves as an interface for the F# code. The algorithms are based on the book "Practical Astronomy with Your Calculator and Spreadsheet".

# What to implement...

## Time

- [ ] ~~Calendars~~ - DateTime.
- [ ] ~~The date of Easter~~ - No need for that.
- [ ] ~~Converting the date to the day number~~ - DateTime.
- [x] Date to Julian Date
- [x] Julian Date to Date
- [ ] ~~Finding the name of the day of the week~~ - DateTime.
- [ ] ~~Converting hours, minutes and seconds to decimal hours~~ - No need for that.
- [ ] ~~Converting decimal hours to hours, minutes and seconds~~ - No need for that.
- [ ] ~~Converting the local time to universal time~~ - No need for that, all implementations expect UTC DateTime. Also this conversion is implemented in DateTime.
- [ ] ~~Converting UT and Greenwich calendar date to local time and date~~ - Same as previous point.
- [x] Date and time to Greenwich Sidereal Time
- [x] Greenwich Sidereal Time to Date and time
- [x] Greenwich Sidereal Time to Local Sidereal Time
- [x] Local Sidereal Time to Greenwich Sidereal Time
- [ ] Ephemeris time (ET) and terrestrial time (TT)

## Coordinate systems

- [ ] ~~Converting between decimal degrees and degrees, minutes and seconds~~ - No need for that.
- [ ] ~~ Converting between angles expressed in degrees and angles expressed in hours~~ - No need for that.
- [x] Converting between right ascension and hour angle
- [x] Equatorial to horizon coordinate conversion
- [x] Horizon to equatorial coordinate conversion
- [x] Ecliptic to equatorial coordinate conversion
- [x] Equatorial to ecliptic coordinate conversion
- [x] Equatorial to galactic coordinate conversion
- [x] Galactic to equatorial coordinate conversion
- [ ] Generalised coordinate transformations
- [ ] The angle between two celestial objects
- [ ] Rising and setting
- [x] Precession low-precision method
- [ ] Precession rigorous method
- [x] Nutation
- [x] Aberration
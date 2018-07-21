# Data Persistence Using the Repository Pattern (DPURP)
DPURP is a .NET library which provides an abstract interface for data peristence and implementations for ORM Frameworks.
The idea is similar to [Catharsis.NET.Repository](https://github.com/prokhor-ozornin/Catharsis.NET.Repository) and [https://github.com/matthewschrager/Repository](https://github.com/matthewschrager/Repository), but this project will minimize dependencies on other libraries and maximize portability.

## Use-case:
You need to write a software package that requires heavy configuration. You don't have time to write complex configuration software. You want to write the package so that it is extensible, which can easily change the way configuration is done, so you are free to develop the package.

## Minimizing Dependencies
Packages are separated to minimize dependencies.

### Dpurp.Abstractions

### Dpurp.Xml

### Dpurp.EntityFramework

### Dpurp.Ini

### Dpurp.Json

## Maximize Compatibility
Builds will target the lowest .NET Standard and .NET Framework versions necessary.

# License and Use
Licensed under LGPL v3.0.
If you use it in any software, open or closed: no problem.
If you modify extend the features of the library, eg. fixing a bug or adding a concrete implementation for a different ORM Framework, you must give back the source code.
The repository pattern does not belong to the author, only this implementation.

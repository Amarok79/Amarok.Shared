![CI](https://github.com/Amarok79/Amarok.Shared/workflows/CI/badge.svg)
[![Mutation testing badge](https://img.shields.io/endpoint?style=flat&url=https%3A%2F%2Fbadge-api.stryker-mutator.io%2Fgithub.com%2FAmarok79%2FAmarok.Shared%2Fmaster)](https://dashboard.stryker-mutator.io/reports/github.com/Amarok79/Amarok.Shared/master)
[![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=Amarok79_Amarok.Shared&metric=reliability_rating)](https://sonarcloud.io/dashboard?id=Amarok79_Amarok.Shared)
[![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=Amarok79_Amarok.Shared&metric=sqale_rating)](https://sonarcloud.io/dashboard?id=Amarok79_Amarok.Shared)
[![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=Amarok79_Amarok.Events&metric=security_rating)](https://sonarcloud.io/dashboard?id=Amarok79_Amarok.Events)
[![NuGet](https://img.shields.io/nuget/v/Amarok.Shared.svg?logo=)](https://www.nuget.org/packages/Amarok.Shared/)

# Introduction

This library contains various general purpose helpers, utilities and extensions.


# Redistribution

The library is redistributed as NuGet package: [Amarok.Shared](https://www.nuget.org/packages/Amarok.Shared/)

The package provides strong-named binaries for *.NET Standard 2.0* and *.NET 5.0*. Tests are performed with *.NET Framework 4.8*, *.NET Core 2.1*, *.NET Core 3.1*, and *.NET 5.0*.


# Types of Interest

### BufferSpan

A memory-efficient value type pointing to a span (segment) in a byte array. Provides APIs for appending or slicing. Tuned to reduce memory allocations.

````cs
var a = BufferSpan.From(0x11, 0x22);
var b = BufferSpan.From(new Byte[] { 0x22, 0x33, 0x44, 0x55, 0x66 }, 1, 3);

b.Buffer      // 0x22, 0x33, 0x44, 0x55, 0x66; same as supplied to From(..)
b.Offset      // 1
b.Count       // 3
b.IsEmpty     // false
b.ToArray()   // 0x33, 0x44, 0x55; allocates new byte array
b.ToString()  // 33-44-55

var c = a.Append(b);    // 0x11, 0x22, 0x33, 0x44, 0x55; re-uses byte array if big enough, 
                        //                               otherwise allocates new byte array
c = c.Discard(1);       // 0x22, 0x33, 0x44, 0x55; re-uses byte array
c = c.Slice(1, 2);      // 0x33, 0x44; re-uses byte array
c = c.Clear();          // <empty>, but re-uses the original byte array
````

### Byte, Byte[] Extensions

Extension methods for efficient hex formatting.

````cs
Byte byte = 0x0F;
String text = byte.ToHex();       // "0x0F"

Byte[] bytes = new[] { 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, 0xAA };
String text = bytes.ToHex(" ");   // "0x11 0x22 0x33 0x44 0x55 0x66 0x77 0x88  0x99 0xAA"
````

### StopwatchPool

Provides a pool of pre-instantiated Stopwatch instances.

````cs
Stopwatch sw = null;
try
{
  sw = StopwatchPool.Rent();
  
  // use stop watch
}
finally
{
  StopwatchPool.Free(sw);
}
````

### StringBuilderPool

Provides a pool of pre-instantiated StringBuilder instances.

````cs
StringBuilder sb = null;
try
{
  sb = StringBuilderPool.Rent();
  
  // use string builder
  
  var result = sb.ToString();
}
finally
{
  StringBuilderPool.Free(sb);
}
````

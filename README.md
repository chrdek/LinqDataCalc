### LINQ-based statistical calculation, data analysis functions package

|  Review  |
|:------------:|
[![Codacy Badge](https://app.codacy.com/project/badge/Grade/0ee67475c8df4295bea124f199615af1)](https://www.codacy.com/gh/chrdek/LinqDataCalc/dashboard?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=chrdek/LinqDataCalc&amp;utm_campaign=Badge_Grade) |
| [![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=chrdek_LinqDataCalc&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=chrdek_LinqDataCalc) |
| [![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=chrdek_LinqDataCalc&metric=security_rating)](https://sonarcloud.io/summary/new_code?id=chrdek_LinqDataCalc) |
| ![Nuget](https://img.shields.io/nuget/dt/LinqDataCalc?logo=nuget) |
| [🌐 Global Status](https://status.nuget.org/) |

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

<br/>
This nuget package implements basic statistics and data analytics functions. 

__Some of them are displayed in the table below:__
<br/>

<br/>

|  Function  |  Explanation  |
|:------------:|:------------:|
|  STDEVP()    | Standard Deviation Calc.             |
|  MEDIAN()    | Median Calc.              |
|  MODE()      | Mode Value             |

<br/>
Additional randomization and Set-Based selections techniques are also included as part of the statistics package.
<br/>
<br/>

UPDATE: Added extended functionality for diffing, distance calculation algorithms and tree structures creation.

<br/>

General usage/setup:

- On nupkg install from nuget package man. VS add relevant reference.
- Reference in-code: 
```C#
using static LinqDataCalc.LinqDataCalcExtensions;
```
- Further usage, instructions on official [online documentation.](https://chrdek.github.io/docs/LinqDataCalc.html)

Package installable from [online nuget source.](https://www.nuget.org/packages/LinqDataCalc/)

<br/>

 <img src="https://res.cloudinary.com/dmjcetjt8/image/upload/v1701036002/New_tmplt_q6u4g7.png" style="height:55px;" title="other implement."/> &nbsp;&nbsp;Ported as partial IQueryable implementation of <a href="https://github.com/chrdek/QueryablDataCalc">Queryabl.DataCalc</a>

<br/>


<br/>

___.nupkg file tested with VS2017 IDE .net 4.6.2___